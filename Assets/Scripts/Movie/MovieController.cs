using UnityEngine;
using System.Collections;
using UnityEngine.UI ; 
using TeaLiqueur.MovieController ; 

//[RequireComponent (typeof(RadianPageCtrl))]
public class MovieController : MonoBehaviour{


	public static float secondsForWaitVideoLoad = 3f;

	[Header("Config")]
	public string videoPath = null ; 
	public CTRL_MODE PlayMode = CTRL_MODE.MANUAL ;
	public CTRL_MODE StopMode = CTRL_MODE.AUTO ;
	public bool useHandHeld = false ; 


	[Header("GameObjects")]
	public UIMediaPlayerCtrl video;
	public Button playBtn ; 
	public Button nextBtn ;


	/// <summary>
	/// 使用 videopanel 上面的原始設定，並呼叫他去載入。
	/// </summary>
	public void Init (){
		_init () ;
	}

	public void Init (string path , CTRL_MODE p = CTRL_MODE.MANUAL, CTRL_MODE s = CTRL_MODE.AUTO){
		this.videoPath = path ;
		this.PlayMode = p ; 
		this.StopMode = s ; 




		_init () ; 
	}
		
	public void _init (){


		playBtn.gameObject.SetActive(false);
		nextBtn.gameObject.SetActive(false);


		video.gameObject.SetActive(true);			
		video.OnReady += OnReady ; 
		video.OnVideoFirstFrameReady += OnFirstFrame ; 
		video.OnEnd += videoOnEnd ; 


		if(useHandHeld){
			waitForLoad(0f) ; 
		}else{
			

			if (videoPath != null)
				video.Load(videoPath);
			else 
				video.Load();

			#if UNITY_EDITOR
			video.OnReady();   // assumed movie end
			#endif

			#if UNITY_IPHONE
			waitForLoad(secondsForWaitVideoLoad) ; 
			#endif
		}
		

	}




	public void waitForLoad (float waitTime){
		StartCoroutine(_waitForLoad(waitTime)) ; 
	}
	public IEnumerator _waitForLoad (float waitTime){
		yield return new WaitForSeconds(waitTime);
		if( video.GetCurrentState() == UIMediaPlayerCtrl.MEDIAPLAYER_STATE.NOT_READY){
			handHeldHelp() ; 

			yield return new WaitForSeconds(.5f);
			if (video.OnEnd != null) {
				video.OnEnd() ; 	
				video.OnEnd = null ; 
			}

			// Radian
			if (nextBtn != null && nextBtn.IsActive())
				nextBtn.onClick.Invoke();
				
		}
	}
		

	public void videoOnEnd(){
		video.OnEnd = null ; 
//		video.Pause();
		if (StopMode == CTRL_MODE.MANUAL)
			nextBtn.gameObject.SetActive(true);
		else if (StopMode == CTRL_MODE.AUTO){
			Debug.Log ("MovieController Auto Next") ;

			nextBtn.onClick.Invoke() ;
		}else if (StopMode == CTRL_MODE.WAIT_1 || StopMode == CTRL_MODE.WAIT_3)
			nextBtn.onClick.Invoke() ; // wait for implement
	}

	public void OnReady (){  // here
		video.GetComponent<RawImage>().color = Color.white ;
		video.Play();

		#if UNITY_EDITOR
		video.OnVideoFirstFrameReady();   // assumed movie end
		#endif
	}

	IEnumerator unityEditorFlow (){
		yield return new WaitForSeconds(1.5f);
		playBtn.onClick.Invoke();
	}

	public void OnFirstFrame (){
		playBtn.gameObject.SetActive(true);
		if (PlayMode ==  CTRL_MODE.AUTO){
			Debug.Log ("MovieController Auto Play") ;
			#if UNITY_EDITOR
			StartCoroutine(unityEditorFlow());
			#elif UNITY_IPHONE

			playBtn.onClick.Invoke();
			#endif

		}else if (PlayMode == CTRL_MODE.MANUAL){
			video.Pause();
		}
	}

	public void handHeldHelp(){

//		var path =  MAIN.videoBlue ? "blue.mp4" : videoPath != null ? videoPath : video.m_strFileName ;
//		Debug.Log( "Radian Using HandHeld, Path : " + path) ;
//		Handheld.PlayFullScreenMovie( path , Color.black,FullScreenMovieControlMode.Minimal);

	}

	
	void OnDisable () {	
		video.UnLoad();
		Debug.Log( name +  " Disabled") ;
		
	}



	public void Play (){
		playBtn.gameObject.SetActive(false);
		video.Play();
		#if UNITY_EDITOR
		video.OnEnd();   // assumed movie end

//		video.OnNearEndEvent.Invoke();
//		video.OnEndEvent.Invoke();
		#endif

	}

	public void NextPage (){
		RadianPageCtrl pp = GetComponent<RadianPageCtrl>() ; 
		if (pp != null)
			pp.GotoNext();
		else {
			GetComponent<Page>().goToNextPage();
		}
	}
}

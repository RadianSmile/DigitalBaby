using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System ; 
using UnityEngine.Events ; 

public class TIME : MonoBehaviour {

	public float Mission1 = 4500f ;
	public float Mission2 = 900f ;

	public static TIME instance ; 
	 

	public Text timerLabel;
	public float duration ;
	[SerializeField] 
	private float maxDuration  ;

	public UnityEvent OnTimeUp ; 
	private float startTime ;

	public float GameStartTimePoint ;


	public bool activate = false  ; 

	public void Awake (){
		instance = this ; 
	}
		
	public void Mission1Start(){
		setMaxDuration(Mission1) ; 
		activate = true ; 
		Reset();
	}


	public void TaskStart() {
		GameStartTimePoint = Time.time;

		setMaxDuration(Mission1) ; 

		OnTimeUp.RemoveAllListeners() ;
		OnTimeUp.AddListener (()=>{
			Debug.LogWarning("time up invoked ");
//			MAIN.instantce.GobackToMainRequest();
//			MAIN.instantce.UpdateSectionStatus(TeaLiqueur.SectionStatus.MAP);
		});

		activate = true ; 
		Reset();
	}

	public void MapStart (){
		GameStartTimePoint = Time.time ;

//		setMaxDuration(MapTimeSeconds ) ; 

		OnTimeUp.RemoveAllListeners() ;
		OnTimeUp.AddListener(()=>{
//			MAIN.instantce.GotoEnd() ;	
		}) ; 
		activate = true ; 
		Reset();
	}

	public void ShortenTime (){
		setMaxDuration(1f); 
		Reset();
	}

	public void OnTaskTimeEnd (){

//		switch(MAIN.instantce.sectionStatus){
//
//		case TeaLiqueur.SectionStatus.PATH : 
//
//			break ; 
//
//		case TeaLiqueur.SectionStatus.MAP : 
//			
//			break ; 
//		}

	}


	private  void Reset (){
		duration = 0f ;
	
	}
	private void setMaxDuration (float maxDuration){
		this.maxDuration = maxDuration ;
	}

	public void Hide (){
		timerLabel.enabled = false ; 
	}
	public void Show (){
		timerLabel.enabled = true  ; 
	}
		
	void Update () {

		// this.gameObject.GetComponent<Text> ().text = String.Format("{0:HH:mm:ss}", DateTime.Now);; 
		// http://answers.unity3d.com/questions/905990/how-can-i-make-a-timer-with-the-new-ui-system.html

		if (!activate  ) {
			timerLabel.text = string.Format ("{0:00} : {1:00} ",0,0); // min , secs 	
			return ;
		};

		float remain  =  maxDuration + GameStartTimePoint - Time.time; 

		if ( (remain - 1) < 0 ){
			OnTimeUp.Invoke();

			Debug.LogWarning("Time up");
			activate = false ; 
//			timerLabel.enabled = false ; 
		}else {
			timerLabel.text = string.Format ("{0:00} : {1:00} ", Mathf.Floor( remain / 60 ), Mathf.Floor(remain % 60)); // min , secs 	
		}
	}
}

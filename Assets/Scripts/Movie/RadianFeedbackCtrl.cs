using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using TeaLiqueur.MovieController ; 

public class RadianFeedbackCtrl : MonoBehaviour {

//	public int bad_TaskId ;

	[SerializeField]
	public MovieController preVideo ; 
	public string optionPanelPath ; 
	public RadianFeedbackOptionCtrl optionPanel ; 
	public RadianPage postVideoPage ; 
	public Image postVideoImage  ;

	public RadianPage location ; 

	[System.Serializable]
	public struct OptionVideo  {
		public string option;
		public string videoName;
	}
	public OptionVideo[] videos ;
	public Dictionary<string,string> optionToVideo = new  Dictionary<string,string>  () ; 


	void Start (){ // exacuate only once in game
		foreach (OptionVideo o in  videos ) optionToVideo.Add( o.option , o.videoName ) ; //o.videoName
	}
		
	public void Init (string preVideoPath  , string optionPanelPath , string locationString) {

		GetComponentInParent<RouteControllerAbstract>() ;
		this.optionPanelPath = optionPanelPath ;

		preVideo.nextBtn.onClick.AddListener(()=>{
			preVideo.GetComponent<RadianPage>().Close(()=>{
				ShowOptionPanel();
			});
		}); 
		preVideo.StopMode = CTRL_MODE.AUTO ; 
		preVideo.Init(preVideoPath);

		location.GetComponent<Text>().text = "( " + locationString + " )" ; 
		location.gameObject.SetActive(false);

		Debug.Log(name +" "+ GetType().Name +  "Inited");
	}
		




	public void ShowOptionPanel (){


		GameObject optionPanelGameObject = Instantiate<GameObject>( Resources.Load<GameObject>(optionPanelPath) )  ; 
		_configHierachy(optionPanelGameObject);

		// set size and pos

		this.optionPanel = optionPanelGameObject.GetComponent<RadianFeedbackOptionCtrl>(); 
		this.optionPanel.gameObject.SetActive(true);
		this.optionPanel.Init(this);
		this.optionPanel.GetComponent<RadianPage>().Show();


	}

	private void _configHierachy (GameObject g){
		g.transform.SetParent(this.transform);
		g.transform.SetSiblingIndex(1);

		RectTransform r = (RectTransform) g.transform ; 
		r.offsetMax = Vector2.one ; 
		r.offsetMin = Vector2.zero ;
		r.localScale = Vector3.one ;
		r.localPosition = Vector3.zero;

	}

	public void PlayOptionVideo (string option , List<string> imageList){


		GetComponent<RadianPageCtrl>().Init();
//		MAIN.instantce.PathResponse( GetComponent<RadianPageCtrl>().r.TaskId,option) ; 
		optionPanel.gameObject.GetComponent<RadianPage>().Close(null,true);


		postVideoPage.gameObject.SetActive(true);
		postVideoPage.Show() ; 

		StartCoroutine(Carousel(imageList)); // will go to the end ; 

	}



	IEnumerator Carousel (List<string> imageList){
		int l = imageList.Count ; 
		int i = 0 ; 

		do {
			switchOptionImage(imageList[i++]);
			yield return new WaitForSeconds(1f);
		} while (i < l ) ; 

		postVideoImage.color = Color.black ;

		location.gameObject.SetActive(true);
		location.Show() ; 
//		yield return new WaitForSeconds(3f);
//		GetComponent<RadianPageCtrl>().GotoNext() ;

	}

	bool switchOptionImage ( string path){

		postVideoImage.overrideSprite = Resources.Load<Sprite>(path) ; 
		postVideoImage.SetNativeSize();
		return postVideoImage.overrideSprite != null ;
	}
}

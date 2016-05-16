using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using TeaLiqueur.MovieController ; 

public class FeedBackCtrl : MonoBehaviour {

	public int bad_TaskId ;

	public MovieController preVideo ; 
	public Page optionPanel ; 
	public MovieController optionVideo ; 
	public GameObject location ; 

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

	void OnEnable(){
		location.SetActive(false);
		Debug.Log(name +" "+ GetType().Name +  " OnEnabled");

		preVideo.Init();
		preVideo.video.OnEnd += showOptionPanel ; 
		preVideo.StopMode = CTRL_MODE.AUTO ; 


		// Rn. temp close 
		//		optionVideo.GetComponent<MovieController>().video.OnNearEnd += ()=>location.SetActive(true) ;

		//		optionVideo.video.OnReady += showOptionVideo ;
	}



	public void PlayOptionVideo (string option){




//		MAIN.instantce.PathResponse( bad_TaskId,option) ; 

		preVideo.GetComponent<Page>().Close();
//		optionPanel.Close();
		optionVideo.gameObject.SetActive(true);
		optionVideo.Init(optionToVideo[option],CTRL_MODE.AUTO,CTRL_MODE.AUTO);


		Debug.Log("Radian : selected " + option +" : " + optionToVideo[option]  );
		optionVideo.GetComponent<Page>().Show();

//		optionVideo.init(optionToVideo[option]) ; 
	}


	public void showOptionPanel (){
		preVideo.nextBtn.gameObject.SetActive(false);
		optionPanel.Show();
	}


}

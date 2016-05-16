using UnityEngine;
using UnityEngine.UI;
using System.Collections;
//using  ; 
public class Target_Mission2 : MonoBehaviour {

	// Use this for initialization
	public string resourcePath ;

	[HideInInspector] 
	public Ctrl_Mission2 ctrl ; 

	void Init (){

		var trackers = GetComponentsInChildren<Vuforia.RadianTrackableEventHandler>() ; 
		var g =  GameObject.FindGameObjectWithTag("mission2") ; 
		ctrl = g.GetComponent<Ctrl_Mission2>() ;


		foreach(var tracker in trackers){

			var _tracker = tracker ;
			_tracker.onFound.AddListener( () => {
				_tracker.responseObject.SetActive(true);
				Debug.Log(resourcePath + _tracker.name + ".jpg");
				_tracker.GetComponentInChildren<Image>().overrideSprite = Resources.Load<Sprite>(resourcePath + _tracker.name ) ;
				_tracker.GetComponentInChildren<Image>().SetNativeSize();
			});


			_tracker.onWaitEnough.AddListener(()=>{
				var name = tracker.name ; 
				ctrl.Found(name) ; 
			});
		}



	}




	void Start (){
		Init();
	}

}

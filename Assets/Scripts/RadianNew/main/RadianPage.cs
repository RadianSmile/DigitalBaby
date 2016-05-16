using UnityEngine;
using System.Collections;
using System ; 

[RequireComponent (typeof(EDU_UI))]

/// <summary>
/// 這是用來做動畫的喔
/// </summary>
public class RadianPage : MonoBehaviour {


	[Header("Transition")]
	public EDU_UI.TRANSITION showTransition = EDU_UI.TRANSITION.FADE_FROM_BLACK; 
	public float showTransitionDur = 1f ;
	public EDU_UI.TRANSITION hideTransition = EDU_UI.TRANSITION.FADETO_BLACK ;
	public float hideTransitionDur = 1f ;


	public void Show (){Show (null);}

	public void Show (Action callBack){
		Debug.Log(gameObject.name +  "Showing");
		GetComponent<EDU_UI>().Transit(showTransition, showTransitionDur , callBack ) ;			
	}

	public void Close (){ Close (null);}

	public void Close (Action callback , bool autoClose = false){
		Debug.Log(gameObject.name +  " Closing");

		GetComponent<EDU_UI>().Transit(hideTransition,hideTransitionDur, ()=>{
			if (callback != null)
				callback();

			if (!autoClose)
				gameObject.SetActive(false);
		});
	}

	public void Transit (EDU_UI.TRANSITION transition , float duration , Action callback ){
		GetComponent<EDU_UI>().Transit(transition,duration,callback);
	}


}

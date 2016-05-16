using UnityEngine;
using System.Collections;
using Vuforia ; 

public class tCtrl : MonoBehaviour {


	[Header("Config")]
	public string tagName ;

	[Header("Debug")]
	[SerializeField]
	protected GameObject delegater ; 
	[SerializeField]
	protected bool inited = false; 

	protected virtual void Start (){
//		Ctr_Main ctrl = GameObject.FindGameObjectWithTag(tagName).GetComponent<Ctr_Main>() ; 
//
//		RadianTrackableEventHandler[] ts = GetComponentsInChildren<RadianTrackableEventHandler>() ; 
//
//		foreach(RadianTrackableEventHandler t in ts){
//			GameObject tt = t.gameObject ; 
//			t.waitForSeconds = 1f ;
//			t.onWaitEnough.AddListener( ()=>{
//				ctrl.Found(tt);
//				Debug.Log("Radian debug : RadianTrackableEventHandler callback :" + tt.name);
//			});
//		}
//
//		inited = ts.Length > 0  ;
	}

}

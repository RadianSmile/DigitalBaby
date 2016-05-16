using UnityEngine;
using UnityEngine.Events;
using System.Collections;

public class TimeCounter : MonoBehaviour {

	bool interupt = false ;
	public float trackWaitTime  = -1f; 
	public bool startOnEnabledOnce = false;  
	public string message ; 

	void OnEnable ()  {
		if (startOnEnabledOnce)
			Count() ; 
	}
	void OnDisable (){
		Interupt() ;

	}

	[SerializeField]
	public float duration ; 

	public UnityEvent onTimeUp ; 

	public void Interupt (){
		interupt = true ; 
	}
	public void Count (){
		Count(trackWaitTime);
	}


	public void Count (float time){
		Debug.Log(message + " wait " + time + " seconds to execuate");
		interupt = false ;
		trackWaitTime = time ; 
		if (trackWaitTime > 0){
			StartCoroutine( _count( trackWaitTime ));
		}else {
			if (startOnEnabledOnce) this.enabled = false ; 
			Debug.Log(message + " execuated! ");
			onTimeUp.Invoke() ;
		}

	}

	IEnumerator _count ( float trackWaitTime){
		
		duration = 0f ; 
		while ( duration <= trackWaitTime ){
			yield return new WaitForSeconds(.05f);
			if (interupt) break ;
			duration += 0.05f ; 
		}
		if (duration > trackWaitTime){
			if (startOnEnabledOnce) this.enabled = false ; 
			Debug.Log(message + " execuated! ");
			onTimeUp.Invoke() ;
		}
		interupt = false ;


	}


}

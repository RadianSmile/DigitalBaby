using UnityEngine;
using UnityEngine.Events;
using System.Collections;

public class Target : MonoBehaviour {
	public float trackWaitTime  = -1f; 
	public UnityEvent onFound ; 
	public UnityEvent onWaitEnough ; 


	public void Tracked (){
		if (trackWaitTime > 0)
			checkFoundThread( trackWaitTime );
		else 
			onFound.Invoke() ;
		
	}

	public void checkFoundThread (float trackWaitTime){
		StartCoroutine(checkTarget(GetComponent<ImageTargetTrackableEventHandler>() ,trackWaitTime ));
	}

	IEnumerator checkTarget ( ImageTargetTrackableEventHandler target ,float trackWaitTime){
		float duration = 0f ; 
		while ( duration <= trackWaitTime ){
			yield return new WaitForSeconds(.05f);
			Debug.Log(duration);
			if (!target.isBeingTracked)  break ;
			duration += 0.05f ; 
		}
		if (duration > trackWaitTime){
			onWaitEnough.Invoke() ;
		}
	}

}

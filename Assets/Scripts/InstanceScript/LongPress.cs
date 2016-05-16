using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using System.Collections;

public class LongPress : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IPointerExitHandler    {
	[ Tooltip( "How long must pointer be down on this object to trigger a long press" ) ]
	public float durationThreshold = 1.0f;

	public UnityEvent onLongPress = new UnityEvent( );

	private bool isPointerDown = false;
	private bool longPressTriggered = false;
	private float timePressStarted;


	private void Update( ) {
//		Debug.Log(Time.time - timePressStarted);
		if ( isPointerDown && !longPressTriggered ) {
			if ( Time.time - timePressStarted > durationThreshold ) {
				longPressTriggered = true;
				onLongPress.Invoke( );
				Debug.Log(name + " LongPress execuated! ");
			}
		}
	}

	public void OnPointerDown( PointerEventData eventData ) {
		Debug.Log(name + " LongPress Start, execuate in " + durationThreshold + " seconds.") ; 
		timePressStarted = Time.time;
		isPointerDown = true;
		longPressTriggered = false;
	}

	public void OnPointerUp( PointerEventData eventData ) {
		Debug.Log(name + " LongPress Stop, not execuate.") ; 
		isPointerDown = false;
	}


	public void OnPointerExit( PointerEventData eventData ) {
		isPointerDown = false;
	}
}
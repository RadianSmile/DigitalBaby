using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems ; 
using System.Collections;

public class TouchableLine : MonoBehaviour {

	public RectTransform line ; 
	float deltaX ;
	public int answerMin ;
	public int answerMax ;

	public UnityEvent onFinished ; 

	[SerializeField] 
	float distance = 20 ;



	public void changeSize ( BaseEventData e){

		deltaX = (Input.mousePosition.x - line.position.x ) / Screen.width * 1024 ;
		deltaX = deltaX > 0 ? deltaX : 0 ; 

		Debug.Log("deltaX : " + deltaX);
//		line.sizeDelta = new Vector2( deltaX ,line.sizeDelta.y ) ;

		line.sizeDelta = new Vector2(deltaX , line.sizeDelta.y) ; 
	}

	public void justify (BaseEventData e){

		float err = deltaX % distance ;
		deltaX = err < distance / 2 ? deltaX - err : deltaX - err + distance ;

		LeanTween.cancel(line.gameObject);
		LeanTween.value(line.gameObject,line.sizeDelta.x,deltaX,.5f).setOnUpdate((float val)=>{
			line.sizeDelta = new Vector2(val , line.sizeDelta.y) ; 
		});


	}

	public void Answer (){
//		if (deltaX > answerMin && deltaX < answerMax){
//			onFinished.Invoke();
//			MAIN.instantce.AudioPlayRight();
//		}else 
//			MAIN.instantce.AudioPlayWrong();
	}


}

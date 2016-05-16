using UnityEngine;
using System.Collections;

public class LightBall : MonoBehaviour {

	// Use this for initialization

	public float duration ; 

	void Start () {
//		scaleDown() ; 
		alphaDown() ; 
	} 


	private void scaleDown (){
		LeanTween.scale(transform as RectTransform ,Vector3.one * Random.Range(.8f,.9f) , Random.Range(duration,duration + 2)).setOnComplete(scaleUp);
	}
	private void scaleUp (){
		LeanTween.scale(transform as RectTransform ,Vector3.one * Random.Range(.9f,1f) , Random.Range(duration,duration + 2)).setOnComplete(scaleDown);
	}

	private void alphaDown (){
		LeanTween.alpha(transform as RectTransform , Random.Range(.8f,.9f) , Random.Range(.9f,1f)).setOnComplete(alphaUp);
	}
	private void alphaUp (){
		LeanTween.alpha(transform as RectTransform ,1f, Random.Range(.9f,1f)).setOnComplete(alphaDown);
	}
}

using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TestInstantiate : MonoBehaviour {

	// Use this for initialization
	public GameObject parent ; 
	public GameObject endViewPrefab ; 

	void Start () {
	}

	public void instantiate (){
		GameObject g =  Instantiate(endViewPrefab) ;
		Debug.Log("Instantiated Page");
		g.transform.parent = parent.transform ; 
		RectTransform r = (RectTransform) g.transform ; 

		r.offsetMax = Vector2.one ; 
		r.offsetMin = Vector2.zero ;
		r.localScale = Vector3.one ;
		r.localPosition = Vector3.zero;

		Debug.Log( GameObject.FindWithTag("Test").name ); 
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}

using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UVTest : MonoBehaviour {

	// Use this for initialization
	public float x ;
	public float y ;
	public float w ;
	public float h ;

	public float xMin ;
	public float yMin ;
	public float xMax ;
	public float yMax ;
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		var r = new Rect() ;
		r.xMin = xMin ; 
		r.xMax = xMax ;
		r.yMin = yMin ;
		r.yMax = yMax ;

		GetComponent<RawImage>().uvRect = r ;

//		GetComponent<RawImage>().uvRect = new Rect(x,y,w,h);
//		GetComponent<RawImage>().uvRect.x = x ;
//		GetComponent<RawImage>().uvRect.y = y ;
//		GetComponent<RawImage>().uvRect.xMax = xMax ;
//		GetComponent<RawImage>().uvRect.yMax = yMax ;
//		GetComponent<RawImage>().uvRect.xMin = xMin ;
//		GetComponent<RawImage>().uvRect.yMin = yMin ;
//		GetComponent<RawImage>().uvRect.width = w ;
//		GetComponent<RawImage>().uvRect.height = h ;

	}
}

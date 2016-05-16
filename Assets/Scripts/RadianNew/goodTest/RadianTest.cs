using UnityEngine;
using System.Collections;

public class RadianTest : MonoBehaviour {

	static Texture2D renderedTexture ; 
	public Material m ; 
	// Use this for initialization
	void Awake () {
		renderedTexture = new Texture2D(Screen.width , Screen.height);
		m.mainTexture = renderedTexture;
	}
	
	// Update is called once per frame
	void OnPostRender (){
		renderedTexture.ReadPixels( new Rect(0,0,Screen.width , Screen.height ),0,0 ); // this is too expansive
		renderedTexture.Apply() ;
	}
}

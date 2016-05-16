using UnityEngine;
using System.Collections;
using UnityEngine.UI ;

[RequireComponent(typeof(AspectRatioFitter))]
[RequireComponent(typeof( RawImage))]
public class TestRenderTexture : MonoBehaviour {

	// Use this for initialization
	public RenderTexture r ; 
	public AspectRatioFitter a ; 

	void Start(){
		r = GetComponent<RawImage>().texture as RenderTexture ;
		a = GetComponent<AspectRatioFitter>() ; 
	}

	void Update (){
		a.aspectRatio = r.width / r.height ; 
	}
}

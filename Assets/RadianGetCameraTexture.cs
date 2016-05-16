using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class RadianGetCameraTexture : MonoBehaviour {

	// Use this for initialization

	string deviceName ; 
	public RawImage r ;
	AspectRatioFitter a ; 
	public WebCamTexture w ;
	public Texture2D currentPhoto ; 

	void Start () {
		r = GetComponent<RawImage>() ;
		deviceName = WebCamTexture.devices[0].name;

		Debug.Log(deviceName) ;
		w = new WebCamTexture(deviceName,1920,1080) ; 
		w.Play();

		r.texture = w  ; 
		a = GetComponent<AspectRatioFitter>();
		a.aspectRatio = (float)w.width / (float)w.height;
		r.SetNativeSize();
	}


	bool printed = false ; 
	void Update (){
		
		a.aspectRatio = (float)w.width / (float)w.height ;
		if (a.aspectRatio > 1 && !printed){
			Debug.Log(w.width + " " + w.height + " "+  (float)w.width / (float)w.height) ;
			printed = true ; 
		}
	}

	public Texture2D getTextrue (){
		Texture2D t = new Texture2D (w.width,w.height) ; 
		t.SetPixels32(w.GetPixels32()) ;
		t.Apply() ;
		return t ; 
	}

	public void Resume (){
		w.Play();
	}

	public void Pause (){
		currentPhoto = getTextrue() ; 
		w.Pause();
	}



}

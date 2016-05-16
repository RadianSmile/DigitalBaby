using UnityEngine;
using System.Collections;
using Vuforia ; 
using UnityEngine.UI;

[RequireComponent(typeof(RawImage))]
public class RadianUseVuforiaBackground : MonoBehaviour {

//	public Vector3 offset ; 
//	BackgroundPlaneBehaviour b ; 
	RawImage r ; 
	AspectRatioFitter a ; 


	private bool printed = false  ; 
	private int count = 0  ; 
	void Start (){
		r = GetComponent<RawImage>() ;
		a = GetComponent<AspectRatioFitter>();
		r.texture = VuforiaRenderer.Instance.VideoBackgroundTexture ; 
		r.SetNativeSize();
	}

	public void OnDrag (){
//		Debug.Log("DRAG");
//		gameObject.transㄐform.position = Input.mousePosition;
	}

	void Update (){
		r.texture = VuforiaRenderer.Instance.VideoBackgroundTexture ; 
		count++ ;
		float xMax = 0;
		float yMax = 0 ;

		if (VuforiaRenderer.Instance.VideoBackgroundTexture != null ){
			float ratio = (float)VuforiaRenderer.Instance.VideoBackgroundTexture.width / (float)VuforiaRenderer.Instance.VideoBackgroundTexture.height ;

	
			if(ratio > 0 && ratio < 100 ){
				a.aspectRatio = (float)VuforiaRenderer.Instance.VideoBackgroundTexture.width / (float)VuforiaRenderer.Instance.VideoBackgroundTexture.height ;
			}else {
				
				xMax = (float)VuforiaRenderer.Instance.GetVideoTextureInfo().imageSize.x / (float)VuforiaRenderer.Instance.GetVideoTextureInfo().textureSize.x ; 
				yMax = (float)VuforiaRenderer.Instance.GetVideoTextureInfo().imageSize.y / (float)VuforiaRenderer.Instance.GetVideoTextureInfo().textureSize.y ; 



				Rect rr = new Rect () ;
				rr.xMax = xMax ;
				rr.yMax = yMax ;

				ratio = ((float)VuforiaRenderer.Instance.GetVideoTextureInfo().textureSize.x * xMax )/ ((float)VuforiaRenderer.Instance.GetVideoTextureInfo().textureSize.y * yMax) ;

				if(ratio > 0 && ratio < 100 ){
					a.aspectRatio = ratio ;
					r.uvRect = rr ; 
				}

			}

			if(!printed && count > 60 * 10){
				Debug.Log("a.aspectRatio " + a.aspectRatio + " xMax "+ xMax + " yMax " + yMax );

				Debug.Log("VuforiaRenderer.Instance.GetVideoTextureInfo().imageSize : x " +VuforiaRenderer.Instance.GetVideoTextureInfo().imageSize.x);
				Debug.Log("VuforiaRenderer.Instance.GetVideoTextureInfo().imageSize : y " +VuforiaRenderer.Instance.GetVideoTextureInfo().imageSize.y);
				Debug.Log("VuforiaRenderer.Instance.GetVideoTextureInfo().textureSize : x " + VuforiaRenderer.Instance.GetVideoTextureInfo().textureSize.x);
				Debug.Log("VuforiaRenderer.Instance.GetVideoTextureInfo().textureSize : y " + VuforiaRenderer.Instance.GetVideoTextureInfo().textureSize.y);
				Debug.Log("VuforiaRenderer.Instance.GetVideoBackgroundConfig().size : x "+ VuforiaRenderer.Instance.GetVideoBackgroundConfig().size.x);
				Debug.Log("VuforiaRenderer.Instance.GetVideoBackgroundConfig().size : y"+ VuforiaRenderer.Instance.GetVideoBackgroundConfig().size.y);

				Debug.Log(a.aspectRatio + " " + VuforiaRenderer.Instance.VideoBackgroundTexture.width + " " + VuforiaRenderer.Instance.VideoBackgroundTexture.height);	
				Debug.Log(a.aspectRatio + " " + VuforiaRenderer.Instance.GetVideoTextureInfo().textureSize.x + " " + VuforiaRenderer.Instance.GetVideoTextureInfo().textureSize.y);	
				printed = true ;
			}

		}else {
			Debug.Log("Radian iphone : VuforiaRenderer.Instance.VideoBackgroundTexture is null !!");
		}

		r.SetNativeSize();			
	}




}

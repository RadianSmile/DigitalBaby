using UnityEngine;
using System.Collections;
using UnityEngine.UI;

[RequireComponent (typeof(CameraToPng))]

public class Ctr_Cam : MonoBehaviour {

	// Use this for initialization
	private CameraImageAccess cameraImage ; 
	public RawImage preview ; 

	void Start () {
		cameraImage = GetComponent<CameraImageAccess>();
	}
	
	// Update is called once per frame

	public void takePicture (){
		Debug.Log ("clicked");
		Texture2D texture2D = cameraImage.TakePicture();
		preview.texture = texture2D as Texture ; 
		preview.texture.wrapMode = TextureWrapMode.Clamp ; 
		preview.color = new Color (1f,1f,1f,1f);
		preview.GetComponent<Page>().Show();

	}
}


using UnityEngine;
using System.Collections;
using System.IO;
using Vuforia;

public class CameraToPng : MonoBehaviour {

	// Use this for initialization

	WebCamDevice[] camDevice ;

	public GameObject textureGameObject = null ; 
	

//	private  texture2D ; 
	

	public Texture2D getCameraTexture (){
		Texture2D texture2D = textureGameObject.GetComponent<Renderer>().material.mainTexture as Texture2D ; 
		texture2D = Instantiate(texture2D);
		int h = texture2D.height ; 
		int w = texture2D.width ; 
		for (int y = 0; y < Mathf.Floor(h/2f) ; y++) {
			for (int x = 0; x < w ; x++) {
				Color color = texture2D.GetPixel(x,y);
				texture2D.SetPixel(x, y , texture2D.GetPixel(x,(h-1)-y) );
				texture2D.SetPixel(x, (h-1)-y , color);
			}
		}
		texture2D.Apply();
		Debug.Log(name + " OnCapture Triggered");
		return texture2D ; 
		//			texture2D.EncodeToPNG() ; 
		//			SaveTextureToFile(texture2D,"test.png");


	}


	public void SaveTextureToFile( Texture2D texture , string fileName)
	{
		byte[] bytes=texture.EncodeToPNG();
		FileStream file = File.Open(Application.dataPath + "/"+fileName,FileMode.Create);
		BinaryWriter binary= new BinaryWriter(file);
		binary.Write(bytes);
		file.Close();
	}
}

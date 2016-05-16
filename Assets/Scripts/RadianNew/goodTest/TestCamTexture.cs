using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using  System.IO;

public class TestCamTexture : MonoBehaviour {

	// Use this for initialization

	public MeshRenderer backgroundPlane ; 
	public RawImage rawImage ; 
	void Start () {

//		rawImage.texture = backgroundPlane.mainTexture ; 
	}
	
	// Update is called once per frame
	void Update () {
		rawImage.texture = backgroundPlane.material.mainTexture;
	}

	public void Save (){
		Texture2D texture2d = rawImage.texture as Texture2D ; 

		SaveTextureToFile(texture2d,"test.png");
	}

	//http://answers.unity3d.com/questions/245600/saving-a-png-image-to-hdd-in-standalone-build.html
	public void  SaveTextureToFile (  Texture2D texture , string fileName){
		byte[] bytes =texture.EncodeToPNG();
		FileStream file = File.Open(Application.dataPath + "/"+fileName,FileMode.Create);

		Debug.Log (Application.dataPath + "/"+fileName);
		BinaryWriter binary= new BinaryWriter(file);
		binary.Write(bytes);
		file.Close();
	}
}





	
	

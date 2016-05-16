using UnityEngine;
using UnityEngine.UI ;
using System.Collections;

public class Main : MonoBehaviour{

	// Use this for initialization
	void Start () {
		InitCam();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
		

	private WebCamTexture webCamTexture;
	public RawImage imgCam;
	
	
	
	public void Show(bool bShow)
	{
		gameObject.SetActive(bShow);
		
		if(webCamTexture==null)
			InitCam();
		
		if(bShow)
			webCamTexture.Play();
		else
			webCamTexture.Stop();

		GetComponent<EDU_UI>().fadeIn();
	}
	
	
	
	void InitCam()
	{
		webCamTexture=new WebCamTexture(640,480);
		
		Debug.Log("Camera devices:");
		
		WebCamDevice[] devices=WebCamTexture.devices;
		
		int i = 0;
		while (i < devices.Length)
		{
			Debug.Log(devices[i].name);
			i++;
		}
		
		imgCam.texture=webCamTexture;
		
		//flip preview on iOS
		#if UNITY_IOS
		imgCam.gameObject.GetComponent<RectTransform>().localScale=new Vector3(1,1,1);
		Debug.Log ("ios - flipping camera preview");
		#endif
	}
}

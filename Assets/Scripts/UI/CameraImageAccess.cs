using UnityEngine;
using System.Collections;
using Vuforia ; 
public class CameraImageAccess : MonoBehaviour
{

	private Image.PIXEL_FORMAT m_PixelFormat = Image.PIXEL_FORMAT.RGB888; // this work in IOS 
	private bool m_RegisteredFormat = false;
//	private bool m_LogInfo = true;
	public Texture2D texture2D ; 
	public CameraDevice cam ; 

	private VuforiaBehaviour qcarBehaviour ;
	void Awake()
	{
		qcarBehaviour = GameObject.FindGameObjectWithTag("Vuforia").GetComponent<VuforiaBehaviour>(); 

		texture2D = new Texture2D (Screen.width , Screen.height,TextureFormat.RGBA32,true); // RGBA 32 work for RGB888.
//		VuforiaBehaviour qcarBehaviour = (VuforiaBehaviour) FindObjectOfType(typeof(VuforiaBehaviour));
		if (qcarBehaviour != null)
		{
			qcarBehaviour.enabled = true ;
			qcarBehaviour.RegisterVuforiaStartedCallback(SetFrameFormat);
			Debug.Log ("CameraImageAccess Registerd") ; 
		}
//		https://developer.vuforia.com/library/resources/api/unity/class_vuforia_1_1_vuforia_behaviour-members
	}

	void OnDisable(){
		if (qcarBehaviour !=null){
			qcarBehaviour.enabled = false  ; 
		}
	}


	public void SetFrameFormat (){


		if (!m_RegisteredFormat)
		{
			m_PixelFormat = Image.PIXEL_FORMAT.RGBA8888 ; 
			m_RegisteredFormat = CameraDevice.Instance.SetFrameFormat(m_PixelFormat, true);	
			Debug.LogWarning("m_RegisteredFormat RGBA8888 " + m_RegisteredFormat);
		}
		 
		if (!m_RegisteredFormat)
		{
			m_PixelFormat = Image.PIXEL_FORMAT.RGB888 ;
			m_RegisteredFormat = CameraDevice.Instance.SetFrameFormat(m_PixelFormat, true);	
			Debug.LogWarning("m_RegisteredFormat RGB888 " + m_RegisteredFormat);
		}

		if (!m_RegisteredFormat)
		{
			m_PixelFormat = Image.PIXEL_FORMAT.RGB565 ; 
			m_RegisteredFormat = CameraDevice.Instance.SetFrameFormat(m_PixelFormat, true);	
			Debug.LogWarning("m_RegisteredFormat RGB565 " + m_RegisteredFormat);
		}

		if (!m_RegisteredFormat)
		{
			m_PixelFormat = Image.PIXEL_FORMAT.YUV ; 
			m_RegisteredFormat = CameraDevice.Instance.SetFrameFormat(m_PixelFormat, true);	
			Debug.LogWarning("m_RegisteredFormat YUV " + m_RegisteredFormat);
		}


		if (!m_RegisteredFormat)
		{
			m_PixelFormat = Image.PIXEL_FORMAT.GRAYSCALE ; 
			m_RegisteredFormat = CameraDevice.Instance.SetFrameFormat(m_PixelFormat, true);	
			Debug.LogWarning("m_RegisteredFormat GRAYSCALE " + m_RegisteredFormat);
		}


		if (!m_RegisteredFormat)
		{
			m_PixelFormat = Image.PIXEL_FORMAT.UNKNOWN_FORMAT;
			m_RegisteredFormat = CameraDevice.Instance.SetFrameFormat(m_PixelFormat, true);	
			Debug.LogWarning("m_RegisteredFormat UNKNOWN_FORMAT " + m_RegisteredFormat);
		}


	}

	public void OnTrackablesUpdated()
	{
		Debug.Log("Radian : " + name + " OnTrackablesUpdated.");

		if (cam == null) cam = CameraDevice.Instance;
		if (cam == null) Debug.Log("cam null") ; 
//		if (m_LogInfo)
//		{
		Image image = cam.GetCameraImage(m_PixelFormat);
		
		if (image == null ) Debug.Log("image null") ;
		image.CopyToTexture(texture2D) ; // https://developer.vuforia.com/library/resources/api/unity/class_vuforia_1_1_image#a8fd3c74ea7d6e9fde192e9332f113cca
		if (image == null)
		{
			Debug.Log(m_PixelFormat + " image is not available yet");
		}
		else
		{
			string s = m_PixelFormat + " image: \n";
			s += "  size: " + image.Width + "x" + image.Height + "\n";
			s += "  bufferSize: " + image.BufferWidth + "x" + image.BufferHeight + "\n";
			s += "  stride: " + image.Stride;
			Debug.Log(s);
//				m_LogInfo = false;
		}
//		}


	}

	public Texture2D TakePicture (){
		OnTrackablesUpdated();

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


//		GetComponent<CameraToPng>().SaveTextureToFile(texture2D , "tt.png");
		return texture2D ; 
	}
}	
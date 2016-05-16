using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.IO; 

public class DoneHint : MonoBehaviour {

	public static string SpriteFolder = "TeaLiqueur/routeDoneSprite/" ; 
	public Image i ; 

	public void Init (string spriteName){
		Sprite s = Resources.Load<Sprite>( SpriteFolder +  spriteName ) ;
		Debug.Log(s);
		i.overrideSprite = s ; 
	}
}

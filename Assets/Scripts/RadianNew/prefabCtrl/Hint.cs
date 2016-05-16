using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Hint : MonoBehaviour {
	public Text introText ; 
	public TextAlignment a ; 

	public void Init(string s , TextAnchor horizontalAlign = TextAnchor.MiddleLeft ){
		introText.text = s ; 
		introText.alignment = horizontalAlign ;
	}



}

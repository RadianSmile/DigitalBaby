using UnityEngine;
using System.Collections;

public class MSG : MonoBehaviour {

	// Use this for initialization

	public static MSG instance ; 
	public RadianPage panel ;
	void Awake (){
		instance = this ; 
	}

	void Start () {
		panel.gameObject.SetActive(false);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}

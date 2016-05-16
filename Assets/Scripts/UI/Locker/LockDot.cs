using UnityEngine;
using System.Collections;

public class LockDot : MonoBehaviour {

	public GameObject dash ; 
	public GameObject dot ; 

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void inputed (){
		dash.SetActive(false) ; 
		dot.SetActive(true) ; 
	}
	public void notInputed (){
		dash.SetActive(true) ; 
		dot.SetActive(false) ; 
	}
}

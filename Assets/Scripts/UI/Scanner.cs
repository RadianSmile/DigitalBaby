using UnityEngine;
using System.Collections;

public class Scanner : MonoBehaviour {


	public GameObject trackerContainer ; 

	public void closeTrackers () {
		trackerContainer.SetActive(false) ;
	}
	public void openTrackers (){
		trackerContainer.SetActive(true);

	}
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}

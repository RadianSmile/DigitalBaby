using UnityEngine;
using UnityEngine.Events ; 
using System.Collections;

public class Locker : MonoBehaviour {

	// Use this for initialization

	public LockDot[] dots ;  

	public string ans ; 
	public string password ="" ; 

	public GameObject whatToHide ; 
	public GameObject whatToHide2 ; 

	public delegate void Right () ;  
	
	public Right rightAction;

	void Start () {
	
	}

	// Update is called once per frame
	void Update () {
	
	}

	public void typeIn (string v){

		if (password.Length < 3){
			Debug.Log (password) ; 
			password += v ; 

			dots[password.Length - 1].inputed() ; 

			if (password.Length == 3){


				foreach (LockDot d in dots){
					d.notInputed() ;
				}


				if (ans == password){
					password ="" ;
					whatToHide.SetActive(false);
					whatToHide2.SetActive(false);
				
				}else{
					password ="" ;
				}

				
			}
		}
	}
}

using UnityEngine;
using System.Collections;
using System ; 
public class ThreadController : MonoBehaviour {

	// Use this for initialization
	public GameObject thread1 ; 
	public GameObject thread2 ; 
	public GameObject thread3 ; 
	public GameObject thread4 ;
	public GameObject Hint ;

	

	public void disableAllThread(){
		thread1.SetActive(false) ; 
		thread2.SetActive(false) ; 
		thread3.SetActive(false) ; 
		thread4.SetActive(false) ; 
		Hint.SetActive(false) ; 
	}

	public void Play (int theadNum ) {
		disableAllThread();
		switch (theadNum){

		case 1 : 
			thread1.SetActive(true) ; 
//			thread1.GetComponent<Movie>().Play(ShowHint) ; 
			break ;
		case 2 : 
			thread2.SetActive(true) ; 
//			thread2.GetComponent<Movie>().Play(ShowHint) ; 
			break ;
		case 3 : 
			thread3.SetActive(true) ; 
//			thread3.GetComponent<Movie>().Play(ShowHint) ; 
			break ;
		case 4 : 
			thread4.SetActive(true) ; 
//			thread4.GetComponent<Movie>().Play(ShowHint) ; 
			break ;

		default : break ; 
		}
	}
	public void Play (int theadNum , Action movieEndCallback ) {
		disableAllThread();
		switch (theadNum){
			
		case 1 : 
			thread1.SetActive(true) ; 
//			thread1.GetComponent<Movie>().Play(ShowHint) ; 
			break ;
		case 2 : 
			thread2.SetActive(true) ; 
//			thread2.GetComponent<Movie>().Play(ShowHint) ; 
			break ;
		case 3 : 
			thread3.SetActive(true) ; 
//			thread3.GetComponent<Movie>().Play(ShowHint) ; 
			break ;
		case 4 : 
			thread4.SetActive(true) ; 
//			thread4.GetComponent<Movie>().Play(ShowHint) ; 
			break ;
			
		default : break ; 
		}
	}

	public void ShowHint(){
		Hint.SetActive(true);
		Hint.GetComponent<EDU_UI>().fadeIn();
	}

	// Update is called once per frame
	void Update () {
		
	}
}

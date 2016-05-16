using UnityEngine;
using System.Collections;

public class PageSwitcher : MonoBehaviour {


//	public struct ChildPage {
//		public Page page ;
//		public EDU_UI.TRANSITION restTransition ;
//		public EDU_UI.TRANSITION workTransition ;
//	}

	public Page[] pages ;

	public int childPageIndex ; 

	void OnEnable(){
		pages[0].gameObject.SetActive(true);
	}

	void Start () {
		childPageIndex = 0 ;
	}

	public void switch_after ( int newPageIndex){
		if (newPageIndex < 0 || newPageIndex >= pages.Length) {
			Debug.Log("newPageIndex overflow ") ; 
		}
		pages[childPageIndex].Rest(pages[newPageIndex].Work);
		childPageIndex = newPageIndex ; 
	}

	public void switch_parallel ( int newPageIndex){
		if (newPageIndex < 0 || newPageIndex >= pages.Length) {
			Debug.Log("newPageIndex overflow ") ; 
		}
		pages[childPageIndex].Rest();
		pages[newPageIndex].Work() ;
		childPageIndex = newPageIndex ; 
	}

//	public void Task1_Switch (){
//		switch (status){
//		case STATUS.P_2: 
//			status = STATUS.P_1 ;
//			break ; 
//		case STATUS.P_1: 
//			status = STATUS.P_2 ;
//			break ; 
//		default :
//			break;
//		}
//
//		switch (status){
//		case STATUS.P_2: 
//
//			Result.GetComponent<EDU_UI>().moveTo(Result.GetComponent<Page>().hidePosition);
//			Try.GetComponent<EDU_UI>().moveTo(EDU_UI.POSITION.CENTER);
//
//			break ; 
//		case STATUS.P_1: 
//			Result.GetComponent<EDU_UI>().moveTo(EDU_UI.POSITION.CENTER);
//			Try.GetComponent<EDU_UI>().moveTo(Try.GetComponent<Page>().hidePosition);
//			break ; 
//		default :
//			break;
//		}
//	}
//	public bool HintPageShowing ;
//	public void ToggleHintPage (){
//		if (HintPageShowing){ // to hide
//			Hint.GetComponent<EDU_UI>().moveTo(Result.GetComponent<Page>().hidePosition);
//		}else {// to show
//			Hint.GetComponent<EDU_UI>().moveTo(EDU_UI.POSITION.CENTER);
//		}
//		HintPageShowing = !HintPageShowing ; 
//
//	}
}

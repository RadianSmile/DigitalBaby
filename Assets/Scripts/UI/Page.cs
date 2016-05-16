using UnityEngine;
using UnityEngine.Events;
using System.Collections;
using System ; 


[RequireComponent (typeof(EDU_UI))]
public class Page : MonoBehaviour {

	[Header("Transition")]
	public EDU_UI.TRANSITION showTransition = EDU_UI.TRANSITION.FADE_IN ;
	public float showTransitionDur = 1f ;
	public EDU_UI.TRANSITION hideTransition = EDU_UI.TRANSITION.FADE_OUT;
	public float hideTransitionDur = 1f ;



	[Header("Old Transition ")]

	public EDU_UI.POSITION startPosition = EDU_UI.POSITION.CENTER ;

	public EDU_UI.TRANSITION restTransition = EDU_UI.TRANSITION.SLIDE_RIGHT;
	public float restTransitionDur = 1f ;
	public EDU_UI.TRANSITION workTransition = EDU_UI.TRANSITION.SLIDE_CENTER ;
	public float workTransitionDur = 1f ;

//	public EDU_UI.TRANSITION transition ; 
	public Page prePage = null ;
	public Page nextPage = null ;

	
	[Header("For virtual page")]
	public Page firstPage = null ;
	public Page lastPage = null ;

	[Header("Events")]
	public UnityEvent OnPageStart ; 
	public UnityEvent OnPageStarted ;
	public UnityEvent OnPageEnd ; 
	public UnityEvent OnPageEnded ; 

//	[HideInInspector] 
	public bool isVirtualPage  ; 
//	[HideInInspector] 
	public Page parentPage ;  // when no next page , parentPage go to next page ; 



	void Awake () {
		Debug.Log(gameObject.name +  "Awaken");
		isVirtualPage = (firstPage != null) ; 
		parentPage = transform.parent.GetComponent<Page>() ;  // also return self

	}

	void Start(){

		Debug.Log(gameObject.name +  "Started");
		if (startPosition != EDU_UI.POSITION.ORIGIN)
			gameObject.GetComponent<RectTransform>().localPosition = EDU_UI.PositionMap[startPosition] ;

	}

	public void goToNextPage (){
		goToNextPage(null);
	}

	public void goToNextPage ( Action callback){
		if (nextPage == null) { // close self and call parent  to go next.
			OnPageEnd.Invoke();
			OnPageEnded.Invoke();
			if (parentPage != null   ){
				// parent go to next , and  close self ? 
				parentPage.goToNextPage();
			}
		}else{  
			Close(()=>{
				SetInActive() ;
				nextPage.Show() ;
			});
		}
	}

	public void goToPage (Page page ){
		Close(()=>{
			SetInActive() ;
			page.Show( ) ;
		});
	}
	public void goToPage (Page page , Action showCallback ){
		Close(()=>{
			SetInActive() ;
			page.Show(showCallback ) ;
		});
	}
	public void Show (){
		Show (null);
	}
	public void Show (Action callBack){

		OnPageStart.Invoke() ;

		Debug.Log(gameObject.name +  "Showing");
		gameObject.SetActive(true);
		Debug.Log(gameObject.name +  (isVirtualPage ? "isVirtualPage" : "notVirtualPage"));
		if (isVirtualPage) {
			firstPage.Show(callBack);
		}else{
			GetComponent<EDU_UI>().Transit(showTransition, showTransitionDur , callBack + OnPageStarted.Invoke   ) ;			
		}
	}
	/// <summary>
	/// extra 
	/// </summary>
	public void nextPageAfterSeconds (float stayTime ){
		StartCoroutine( goToNextPageAfterStayTime(stayTime) );
	}
	IEnumerator goToNextPageAfterStayTime ( float stayTime){		
		yield return new WaitForSeconds(stayTime);
		goToNextPage() ; 
	}

	public void Close (){
		Close (null);
	}

	public void Close (Action callback){
		Debug.Log(gameObject.name +  " Closing");

		OnPageEnd.Invoke() ; 

		if (isVirtualPage){
			if (lastPage != null){
				lastPage.Close(SetInActive + callback ) ;
			}else {
				callback ();
			}	

		}else{
			Debug.Log (name + " Closing");
			GetComponent<EDU_UI>().Transit(hideTransition,hideTransitionDur, callback + OnPageEnd.Invoke );
		}
	}
	public void Transit (EDU_UI.TRANSITION transition , float duration , Action callback ){
		GetComponent<EDU_UI>().Transit(transition,duration,callback);
	}

//	public void CloseAndCall (Action callback){ // if this is virtual page call next to close
//		if (!isVirtualPage){
//			if (callback != null){
//				GetComponent<EDU_UI>().fadeOut(callback);
//			}else {
//				GetComponent<EDU_UI>().fadeOut(true);
//			}
//		}else  {
//			gameObject.SetActive(false);
//			if (callback != null)callback();
//		}
//	}
	
	public void SetInActive(){
		gameObject.SetActive(false);
	}

	public void Work (){
		Work(null);
	}
	public void Work ( Action callback){
		GetComponent<EDU_UI>().Transit( workTransition ,workTransitionDur, callback);
	}
	public void Rest (){
		Rest(null);
	}
	public void Rest ( Action callback){
		GetComponent<EDU_UI>().Transit( restTransition ,restTransitionDur, callback);
	}




}



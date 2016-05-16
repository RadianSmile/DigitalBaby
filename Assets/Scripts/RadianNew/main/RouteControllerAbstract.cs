using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using TeaLiqueur.Route ; 


public abstract class RouteControllerAbstract : MonoBehaviour {

	public bool Finished = false  ; 

	public abstract int TaskId { get ;}

	private const float DestroyDelay = 2 ;

	[HideInInspector]
	public bool beSwitchable = true ; // for time control


	[Header("Settings")]

	public string folder ; 
	public string nextRouteName ;

	[SerializeField]
//	protected int _pageIndex = 0 ; 
	protected int pageIndex ;
//	{
//		set {
////			Debug.Log("PLLL") ; 
//			_pageIndex = pageIndex ; 
//		}
//		get{
//			return _pageIndex ;
//		}
// 
//	} 

//	public List<string>pageNames ; 

	[SerializeField]
	public List<PagePrefabIndex> PagePrefabIndexes ; 


	public GameObject currentPageGameObject;  


	public void Open (int pageIndex = -1 ){



		Debug.Log("Radian : Clean memo");

		beSwitchable = true ;

		if (pageIndex != -1)
			this.pageIndex = pageIndex ;
		
		if ( !(pageIndex < PagePrefabIndexes.Count) )
			this.pageIndex = 0 ; 
		
		Debug.LogWarning(GetType() + ": Open" ) ;
		Load(this.pageIndex);
		currentPageGameObject.GetComponent<RadianPageCtrl>().Open();

		if (TaskId > 0  ){
			StartRoute() ;
		}

		OnOpen();
//		Base b = new Child () ;
//		b.A() ; 
//		b.B();
//
	}

	public virtual void OnOpen (){
		
	}

	public virtual void OnClose (){
		
	}
//	public 


//	public void Pause (){
//
//		currentPageGameObject.GetComponent<RadianPage>().Close(()={
//			
//		}) ;
//	}

	public void UnloadCurrent (){

		if (currentPageGameObject != null )
			Unload(currentPageGameObject) ;
//		Resources.UnloadUnusedAssets() ;


	}

	public void CloseCurrentPage (System.Action callback = null ){

		//		Resources.UnloadUnusedAssets();
		Debug.Log("MOMORY CLEAN");
		currentPageGameObject.GetComponent<RadianPage>().Close(()=>{
			Unload(currentPageGameObject);
			callback();
		});
	}


	public void GotoNext (){
//		if (!callingPage.Equals(currentPageGameObject))
//			Debug.LogError("Current Page and calling page is not matched!") ; 


		CloseCurrentPage(()=>{
//			MENU.instance.CloseMenu();
			if (pageIndex + 1 < PagePrefabIndexes.Count){

				Load(++pageIndex);
				currentPageGameObject.GetComponent<RadianPageCtrl>().Open();
			}else {
				Debug.Log(ROUTE.instance);
				ROUTE.instance.SwitchToRoute(nextRouteName);
			}			
		});
	}

	public void GotoRoute (string routeName){
		CloseCurrentPage(()=>{
			ROUTE.instance.SwitchToRoute(routeName);
		});
	}

	public void GotoSpecificPage (string pageName ){
		foreach(PagePrefabIndex p in PagePrefabIndexes){
			if (pageName == p.name)
				pageIndex = PagePrefabIndexes.IndexOf(p);
		}
	}




	public abstract void InitPageByCase (PagePrefabIndex pagePrefabIndex , GameObject currentPageGameObject) ;

	public void Load (int nextPageIndex){
		
		Debug.LogWarning(GetType() + " : Load");

//		string path = this.path + this.pageNames[nextPageIndex] ;

		PagePrefabIndex pagePrefabIndex = PagePrefabIndexes[nextPageIndex] ; 
		string path ;


		if (pagePrefabIndex.isNormal)
			path = this.folder + pagePrefabIndex.name ;
		else 
			path = pagePrefabIndex.prefabLocation ; 
	

		path = "TeaLiqueur/" + path ;
		Debug.Log(path);
		currentPageGameObject = Instantiate( Resources.Load( path ,typeof (GameObject))) as GameObject ;
		currentPageGameObject.SetActive(true);
		InitPageByCase(pagePrefabIndex,currentPageGameObject);
			

		_configHierachy(currentPageGameObject) ; 

		pageIndex = nextPageIndex ;
		currentPageGameObject.GetComponent<RadianPageCtrl>().Init() ; 
	}

//	Coroutine c ; 
	public void Unload ( GameObject g ){
		g.SetActive(false);
		GameObject.Destroy(g,DestroyDelay);

//		if (c!= null)
//			StopCoroutine(c);
//		c = StartCoroutine(CleanMemory(DestroyDelay));
	}
//
//	IEnumerator CleanMemory (float DestroyDelay){
//		yield return new WaitForSeconds(DestroyDelay);
//		Resources.UnloadUnusedAssets();
//	}

		

	private void _configHierachy (GameObject g){
		currentPageGameObject.transform.SetParent(this.transform);

		RectTransform r = (RectTransform) currentPageGameObject.transform ; 
		r.offsetMax = Vector2.one ; 
		r.offsetMin = Vector2.zero ;
		r.localScale = Vector3.one ;
		r.localPosition = Vector3.zero;

	}







	public void StartRoute (){
		if (TaskId <= 0) return ;
		Debug.Log("StartRoute" + TaskId ) ;
//		MAIN.instantce.StartRouteTimer(TaskId);
		ROUTE.instance.SetCurrent(this);

//		MAIN.instantce.teamData.PathDataDict[TaskId].pathStatus = TeaLiqueur.PathStatus.RUNNING ; 
		Debug.Log("ROUTE.instance.current" + ROUTE.instance.current);
	}
	public void PauseRoute (){
		if (TaskId <= 0) return ;

//		MAIN.instantce.PauseRouteTimer(TaskId); 
		ROUTE.instance.SetCurrent(null);
//		MAIN.instantce.teamData.PathDataDict[TaskId].pathStatus = TeaLiqueur.PathStatus.PAUSED; 
	}
	public void FinishRoute (){
		if (TaskId <= 0) return ;
		Debug.Log("FinishRoute" + TaskId ) ;
		if (!Finished){
//			MAIN.instantce.teamData.PathDataDict[TaskId].pathStatus = TeaLiqueur.PathStatus.FINISHED ;
//			MAIN.instantce.FinishRouteTimer(TaskId); 
			ROUTE.instance.SetCurrent(null);
			Finished = true ; 

		}
	}

	public void OnDisable(){
//		MENU.instance.openable = false; 
	}
		

}

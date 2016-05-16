using UnityEngine;
using System.Collections;

[RequireComponent(typeof(RadianPage))]
public class RadianPageCtrl : MonoBehaviour {

//	[Header("Config")]

	[HideInInspector]
//	public string nextPageName ; 

	[Header("Auto Setting")]
	public RouteControllerAbstract r ; 
	RadianPage p ; 


	public void Awake (){
		p = gameObject.GetComponent<RadianPage>() ; 

	}
	/// <summary>
	/// Should be called after instantiate
	/// </summary>
	public void Init (){
		r = GetComponentInParent<RouteControllerAbstract>()  ; // wait f
	}
	public void Open (){
		p.Show();
		Init();
	}
	public void Close (){
//		if (nextPageName .Length > 0 )
			p.Close(GotoNext) ; 
	}

	public void CloseAndUnload (System.Action callback ){
		p.Close(()=>{
			
			callback();
			r.UnloadCurrent();
		});
	}

	public void GotoNext (){
//		Init();
		r.GotoNext();			
	}

	public void GotoRoute (string routeName){
		r.GotoRoute(routeName);
	}

}

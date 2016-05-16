using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class ROUTE : MonoBehaviour {

	// Use this for initialization

	public string startRoute ;

	public static ROUTE instance ;
	public List<RouteControllerAbstract> routeControllers ;
	public RouteControllerAbstract current ;
	public void SetCurrent (RouteControllerAbstract r){
		if (r != null )
			current = r ; 
		else {
			Debug.Log("WTF , who setted null to here !?");
		}

	}

	void Awake (){
		Debug.Log(this);
		instance = this ;
	}

	void Start (){

		RouteControllerAbstract[] rs = gameObject.GetComponentsInChildren<RouteControllerAbstract>(true) as RouteControllerAbstract[] ;
		if (rs != null)
			routeControllers.AddRange(rs);
	

		if (startRoute != null){
			RouteControllerAbstract r = getRouteByName(startRoute) ;
			if (r != null) {
				r.gameObject.SetActive(true ) ; 
				r.Open();
				SetCurrent(r);
			}
		}


	}

	public void PausePathToMain (RouteControllerAbstract current){
		current.PauseRoute() ;
	}

	public void SwitchToRoute (string routeName ){
		
		Debug.LogWarning(GetType() + " SwitchToRoute");
		current.gameObject.SetActive(false);


		RouteControllerAbstract routeController = getRouteByName(routeName) ; 
			
		if (routeController != null ){
			current = routeController; 
			current.gameObject.SetActive(true);
			current.Open();
		}else {
			Debug.LogError("routeController is null !");
		}

//		MENU.instance.CloseMenu();
		Resources.UnloadUnusedAssets(); // put behind 
	}
		

	public RouteControllerAbstract getRouteByName (string routeName){
		return routeControllers.Find((RouteControllerAbstract r)=>{
			return r.name.Equals(routeName);
		});

	}

}

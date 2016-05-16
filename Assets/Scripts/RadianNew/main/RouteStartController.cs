using UnityEngine;
using System.Collections;
using TeaLiqueur.Route ; 
using TeaLiqueur.MovieController ; 
public class RouteStartController : RouteControllerAbstract {

	public override int TaskId {
		get {
			return -1 ;
		}
	}

	public override void OnOpen ()
	{
		base.OnOpen ();
		TIME.instance.Hide() ;
	} 

	public override void InitPageByCase (PagePrefabIndex pagePrefabIndex , GameObject currentPageGameObject) {

		if (pagePrefabIndex.isNormal) return ;

		switch (pagePrefabIndex.name){


		case "ps.1.video.main1_1" : 
			currentPageGameObject.GetComponent<MovieController>().Init("main/main1_1.mp4",CTRL_MODE.AUTO,CTRL_MODE.AUTO);
			break;

		case "ps.2.hint.comeToStore" : 
			currentPageGameObject.GetComponent<Hint>().Init("隔天，小安來到店裡 . . .",TextAnchor.MiddleCenter);
			break;

		case "ps.3.video.main" : 
			currentPageGameObject.GetComponent<MovieController>().Init("main/main2_1.mp4",CTRL_MODE.AUTO,CTRL_MODE.AUTO);
			break;

		case "ps.4.hint.scan" : 
			currentPageGameObject.GetComponent<Hint>().Init("這時小安拿起了店長給他的便條紙，在鏡頭下出現了驚人的變化 . . .",TextAnchor.MiddleCenter);
			break;
		
		default : break ;

			
		}
//
	}
	void OnEnable (){

	}

		
}

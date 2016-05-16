using UnityEngine;
using System.Collections;
using TeaLiqueur.Route ;
public class RouteController_DigitalBaby : RouteControllerAbstract {


	public override int TaskId {
		get {
			return 0 ; 
		}
	}

	public override void InitPageByCase (PagePrefabIndex pagePrefabIndex , GameObject currentPageGameObject) {


		switch (pagePrefabIndex.name){



		case "p.start" : 
			TIME.instance.Hide();
			break ; 

		case "p.mission2" : 
			TIME.instance.Show();
			break ; 

		case "p.mission3" : 
			TIME.instance.Show();
			break ; 
		}

		
	}


	bool gotoWaitOnce =false ; 


	public void OnEnable (){
//		if (!MAIN.pathStatusStarted ){
//			MAIN.instantce.GameStartOnce() ;
//		}
//
//
//		if (MAIN.instantce.AllFinished && gotoWaitOnce == false){
//			gotoWaitOnce = true ; 
//			MAIN.instantce.UpdateSectionStatus(TeaLiqueur.SectionStatus.MAIN_WAIT);
//			GotoSpecificPage("pm.1.wait");
//		}
//
//		if (MAIN.instantce.sectionStatus == TeaLiqueur.SectionStatus.MAP){
//			GotoSpecificPage("pm.0.scan.entrance");
//			MAIN.instantce.GameGotoMapOnce();
//		}
//
			
	}

}

using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic; 

public class RadianFeedbackOptionCtrl : MonoBehaviour {

	RouteControllerAbstract r ; 

	public string pathName ; 

	[System.Serializable]
	public struct OptionPath {
		public string optionName ; 
		public List<string> imageNames ; 
	}
	[SerializeField]
	public List<OptionPath> optionPaths ; 


	public void Init (RadianFeedbackCtrl c ){

		r = GetComponentInParent<RouteControllerAbstract>() ; 
//		var p = MAIN.instantce.teamData.PathDataDict[r.TaskId] ; 

		foreach (var o in optionPaths){
			for (var i = 0 ; i < o.imageNames.Count ; i++){
				o.imageNames[i] = pathName + o.imageNames[i] ;
			}
		}

		Button[] bs =  GetComponentsInChildren<Button>() ; 
		foreach(Button b in bs ){
			string bb = b.name ; 
			b.onClick.AddListener(()=>{
//				p.Choice(bb);
				c.PlayOptionVideo(bb , getPaths(bb));
			});
		}
	}

	List<string> getPaths (string option) {
		Debug.Log(option);
		foreach ( OptionPath o in optionPaths ) {
			if ( o.optionName == option ) {
				return o.imageNames ; 
			}
		}
		return null ;
	}


}

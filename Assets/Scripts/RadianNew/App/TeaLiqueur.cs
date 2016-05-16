using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic ; 

namespace TeaLiqueur {

	// waiting for dev ; 
	public enum StageStatus {
		A,B,C,D,E
	}


	public enum DevStatus {
		DEV , PROD 
	}



	[System.Serializable]
	public class TeamData  {

		public Dictionary<int,PathData> PathDataDict = new Dictionary<int,PathData>() ; 

		public string teamName = "" ; 
		public Texture2D handWriteTexture = null ; 
		public string words = "";
		public string feedbackText = "" ;
		public Texture2D photo = null;



	}





	[System.Serializable]
	public class TeaAudio  {
		public AudioClip rightAudio ; 
		public AudioClip wrongAudio ; 
	}



	public enum SectionStatus {
		PATH ,
		PATH_FB ,
		MAP ,
		END ,
		MAIN ,
		MAIN_WAIT

	}


	public enum PathStatus {
		RUNNING ,
		UNDISCOVERD,
		PAUSED , 
		FINISHED 
	}
		


	[System.Serializable]
	public class PathData  {
		public string name = "" ; 
		public PathStatus pathStatus = PathStatus.UNDISCOVERD  ; // for save 
		public float timeUsed = 0f; 
		public bool finished = false  ; 
		public bool discovered = false ;
		public List<string> answer = new List<string>()  ;  // for save 
		public string choice = ""  ; // for save 

		public Vector3 location = Vector3.zero ; 

		public string locationString {
			get {
				return (location.x + " , "  + location.y + " , " + location.z ) ; 
			}
		}

		public PathData (string name){
//			this.finished = MAIN.allPass;
			this.name = name ; 
		}
//		public PathData (string name , Vector3 location){
//			this.name = name ; 
//			this.location = location ; 
//		}

//		public int pathPageIndex = 0 ;
		private float _startTime ; 

		public void SetLocation (Vector3 v){
			location = v ;
		}

		public void Start (float currentTime ){
			pathStatus = PathStatus.RUNNING ;
			_startTime = currentTime ;
		}
		public void Pause (float currentTime ){
			addTimeUsed( currentTime - _startTime ) ; 
			pathStatus = PathStatus.PAUSED ;
			_startTime = -1f ; 
		}
		public void Finished (float currentTime) {
			pathStatus = PathStatus.FINISHED ;
			finished = true ;
			Debug.LogWarning(name +" "+ finished);
			addTimeUsed( currentTime - _startTime ) ; 
			_startTime = -1f ; 
		}
		private void addTimeUsed ( float _timeUsed){
			timeUsed += _timeUsed ; 
			Debug.LogWarning( "TASK time used : " +  name + " : " + timeUsed);
		}

		public void Answer (string _answer){
			answer.Add(_answer) ; 
		}
		public void Choice (string _choice){
			choice = _choice ; 
		}
	}


	namespace Main {

		[System.Serializable]
		public class EntranceLabelData {
			public GameObject doneDot ;
			public PathData finished ;
			public string locationPoint ; 
		}

		[System.Serializable]
		public class TeaTime {
			public float duration  ;

			public void clock (float delta ){
				duration += delta  ;
			}
		}
	}


	namespace T2 {

		[System.Serializable]
		public class ImgPickerIniter {
			public ImgPicker imgPicker ; 
			public int initIndex ;
		}

		[System.Serializable]
		public class ImgOption {
			public Sprite img ; 
			public string txt ; 
		}

	}

	namespace T3 {
		[System.Serializable]
		public class BeaconData {
			public int id ; 
			public Color color ; 
			public float testValue = 7 ; 
			public Sprite instructSprite ;


			[HeaderAttribute("button")]
			public RectTransform lightBall; 
//			public Sprite finishedButtonSprite ;
			public Button button ;
			public bool finished ; 

		}

//		[System.Serializable]
//		public class LightBall {
//			public int beaconId ; 
//			public RectTransform ball ;
//			public Color baseColor ; 
//		}
	}



	namespace MovieController {

		public enum CTRL_MODE {
			AUTO , MANUAL , WAIT_1 , WAIT_3
		}


		public struct MovieControllerInitStruct {

			public string path ;
			public CTRL_MODE playMode ;
			public CTRL_MODE stopMode ;

			public MovieControllerInitStruct (string path ){
				this.path = path ; 
				this.playMode = CTRL_MODE.MANUAL ;
				this.stopMode = CTRL_MODE.MANUAL ;
			}

			public MovieControllerInitStruct (string path , CTRL_MODE playMode , CTRL_MODE stopMode ){
				this.path = path ; 
				this.playMode = playMode ; 
				this.stopMode = stopMode ;
			}
		}

	}

	namespace Route {
		[System.Serializable]
		public struct PagePrefabIndex {
			public string name  ;
			public PrefabType prefabType  ;

			public bool isNormal {
				get {
					return prefabType == PrefabType.N ;
				}
			}

			[HideInInspector] 
			public string prefabLocation {
				get {
					string path = "common/" ;
					switch(prefabType){

					case PrefabType.N :
						return name ;

					case PrefabType.VIDEO:
						return path + "VideoPanel" ; 

					case PrefabType.HINT:
						return path + "HintPanel" ; 
					
					case PrefabType.DONE_HINT:
						return path + "DoneHintPanel" ; 

					case PrefabType.FEEDBACK:
						return path + "FeedbackPanel" ; 

					default: 
						return null ; 
					}
				}
			}


//			PagePrefabIndex (string name,Prefab prefab ){
//				this.name = name ;
//				this.prefab = prefab.Equals("") ? name : prefab  ;
//			}
		}
	}

	public enum PrefabType {
		N = 0  , // N : normal 
		VIDEO  = 10  ,
		HINT = 21 ,
		DONE_HINT = 31 ,
		FEEDBACK = 41 
	}
}


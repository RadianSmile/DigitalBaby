using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events ; 
using System ; 
//using Leantween ; 
using System.Collections;
using System.Collections.Generic ; 
using TeaLiqueur.T3 ; 

public class iBeaconCallback_ClosetBeacon : MonoBehaviour {

	// Use this for initialization



	[Header("Configuration")]
//	public bool testWithTestValue = false ; 
	public float defaultDistanceMeter = 6 ;  // 7 meter 
	public float triggerDistance = .7f  ;
	public float duration = .3f ; 

	[Header("Control Objects")]
//	public iBeaconReceiverExample oIbeaconReciever  ; 
	public RadianIBeaconReceiver ibeaconReciever ; 
	public RectTransform lightBall ;
	public Image InstructPage ; 

	public Dictionary<int,float> distanceScaleMap = new Dictionary<int,float> {
		{70,384f/384f},
		{100,375f/384f},
		{150,350f/384f},
		{200,320f/384f},
		{250,280f/384f},
		{300,240f/384f},
		{350,180f/384f},
		{400,100f/384f}
	};

	private float distanceGetScale (float distance){
		distance *= 100 ;
		int closetKey = 400  ; 
		foreach(KeyValuePair<int,float> d in distanceScaleMap){
			if (distance < d.Key && d.Key < closetKey  ){
				closetKey = d.Key ; 
			}
		}
		return distanceScaleMap[closetKey] ;
	}

//	public Text debugText ;
	[Header("Debugs")]
	public Text detailText ;

	[Header("    Beacon Page ")]
	public Image instructPage ;
	public GameObject dismissInstructBtnObject ; 

 

	[Header("Beacon Data")]
	public List<BeaconData>  _beaconDataList ;
	public Dictionary<int,BeaconData>  beaconDataMap = new Dictionary<int,BeaconData>() ;

	[Header("Event")]
	public UnityEvent OnFinished ; 



	void Awake (){
		foreach (BeaconData b in _beaconDataList  ){
			beaconDataMap[b.id] = b ; 
			b.button.gameObject.name = b.id.ToString() ; 
		}
	}

	void Start () {
		StartCoroutine("Callback"); ; 
		Debug.Log("Radian  . . . .Start") ;
	}




	// Update is called once per frame
	void OnDestroy () {
		StopCoroutine("Callback");
	}

	IEnumerator Callback (){
		Debug.Log("Random .... callback");
		while(true){
			yield return new WaitForSeconds(duration) ; 

//			float devider = 0f  ;  // useless
//			Color finalColor = Color.black ; 
			float closetDistance = defaultDistanceMeter ;
			int closetBeaconId = -1 ; 


			#if UNITY_EDITOR
			foreach(BeaconData bc in _beaconDataList){
				LeanTween.scale( beaconDataMap[bc.id].lightBall, Vector3.one * distanceGetScale( bc.testValue) ,duration) ;
//				if (bc.testValue == 0) continue ;
//				float v = defaultDistanceMeter - bc.testValue ;
//				if (closetDistance > bc.testValue){
//					
//					closetDistance = bc.testValue ; 
//					closetBeaconId = bc.id;
				if (bc.testValue < triggerDistance){
					checkAndTrigger(beaconDataMap[bc.id]);
				}
//				}
//				v = v > 0 ? v : 0f ;
////				finalColor += bc.color * v ;
////				Debug.Log("\tRadian tween d : " + v +" tweeningColor : " + finalColor);
			}
			#endif
			#if UNITY_IOS 
			Debug.Log("Radian ios Beacon!!"); 
//			Debug.Log(ibeaconReciever.beacons);

			detailText.text = "Beacons : \n" ; 

			foreach (Beacon b in  ibeaconReciever.beacons) {
				if (!beaconDataMap.ContainsKey(b.major)) continue ; 

				detailText.text += b.major.ToString() +" : " + b.accuracy.ToString() +" : " +  b.range.ToString() + "\n" ;
//				if (b.accuracy > defaultDistanceMeter) continue ;
//				float colorScale = defaultDistanceMeter - (float)b.accuracy ;

				LeanTween.scale( beaconDataMap[b.major].lightBall, Vector3.one * distanceGetScale( (float)b.accuracy) ,duration) ;

				if (b.accuracy < triggerDistance){
					checkAndTrigger(beaconDataMap[b.major]);
				}

//				if (closetDistance > (float)b.accuracy){
//					closetDistance = (float)b.accuracy ; 
//					closetBeaconId = b.major;
//				}

//				finalColor += beaconDataMap[b.major].color *  colorScale ;
			}

//			if (closetDistance < triggerDistance){
//				checkAndTrigger(beaconDataMap[closetBeaconId]);
//			}

			#endif
//			debugText.text = closetDistance < 2 ? closetDistance.ToString() : "" ; 

//			finalColor = finalColor / defaultDistanceMeter ; 	
//			finalColor.a = 1f ; 
//			if (finalColor.Equals(Color.black)){
//				finalColor = Color.white / 8f ; 
//				finalColor.a = 1f ; 
//			}
//			LeanTween.color(lightBall,finalColor,duration) ; 				

//			Debug.Log("Radian finalColor : " + finalColor);

		}
	}



	private void checkAndTrigger (BeaconData b){
//		return yield new WaitForSeconds(1f) ; 

		Debug.Log("Radian : beacon triggered !! : " + b.id) ; 
		if (b.finished){
			return ; // do nothing ; 
		}else {
			b.finished = true ; 
			b.button.gameObject.SetActive(true);
			b.lightBall.gameObject.SetActive(false);
//			b.button.image.sprite = b.finishedButtonSprite ; 
			showInstruct(b.id);
		}
		return ; 
	}

	private void showInstruct (int bid){
		BeaconData b = beaconDataMap[bid] ; 
		InstructPage.sprite = b.instructSprite ; 
		dismissInstructBtnObject.SetActive(true) ; 
	}

	public void checkAndFinished (){
		foreach(BeaconData b in _beaconDataList){
			if (!b.finished ) return ; 
		}
		OnFinished.Invoke() ; 
	}

	public void callInstruct (GameObject buttonObj ){
		int bid = 0  ; 
		if (int.TryParse(buttonObj.name, out bid)){
			showInstruct(bid); 
		}else {
			Debug.LogError("bid not correct : " + buttonObj.name) ;
		}
	}
}


	
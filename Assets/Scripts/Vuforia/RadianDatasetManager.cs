using UnityEngine;
using System.Collections;
using Vuforia ; 

public class RadianDatasetManager : MonoBehaviour {

	// Use this for initialization
	// you can take a look : https://developer.vuforia.com/forum/unity-3-extension-technical-discussion/how-deactivate-dataset-or-image-tracking&sort=2



	[Header("Should Be awake")]

	public GameObject trackerContainer ;
	public GameObject trackerContainerInstnace ;

	[Header("This Page setting")]
	public string dataSetName ; 
	public bool goInstantiateTracker = false ;


//	public Transform trackerContainerTransform  ;
//	public GameObject trackers ; 

	void OnEnable(){

		if (goInstantiateTracker){
			trackerContainerInstnace = Instantiate(trackerContainer);
			trackerContainerInstnace.transform.parent =  GameObject.Find("TARGET_CONTAINER").transform ; 
			trackerContainerInstnace.SetActive(true);
		}else {
			trackerContainer.SetActive(true);  // must Activate first!!!
		}


		GameObject.FindGameObjectWithTag("Vuforia").GetComponent<VuforiaBehaviour>().enabled = true ; 

		StartCoroutine( ActivateDataset() );

	}
	void OnDisable(){
		DeactivateDataset();

		if (goInstantiateTracker){
			Destroy( trackerContainerInstnace) ; 
		}else {
			trackerContainer.SetActive(false);				
		}

		GameObject a = GameObject.FindGameObjectWithTag("Vuforia"); 
		if (a != null)
			a.GetComponent<VuforiaBehaviour>().enabled = false ;

	}

	IEnumerator ActivateDataset (){

//		float timeSteamp = Time.time;
		yield return new WaitUntil(()=> {
			Debug.Log("Radian : WaitingUntil ... TrackerManager.Instance.GetTracker<ObjectTracker>() != null") ;
			return TrackerManager.Instance.GetTracker<ObjectTracker>() != null ;
		}) ;
		ObjectTracker o = TrackerManager.Instance.GetTracker<ObjectTracker>() ;

		IEnumerable l = o.GetDataSets() ;

		bool done = false ;
		foreach ( DataSet d in l ){
			Debug.Log(d.Path);
			if (d.Path.Contains(dataSetName)){
				o.ActivateDataSet(d) ;
				done = true ;
				Debug.Log(dataSetName + " activated.") ;
			}
		}
		if (!done){
			DataSet d = o.CreateDataSet(); 	
			d.Load(dataSetName) ; 
			o.ActivateDataSet(d) ;
			Debug.Log(dataSetName + " Loaded and activated.") ;
		}
	}



	public void DeactivateDataset (){
		ObjectTracker o = TrackerManager.Instance.GetTracker<ObjectTracker>(); 
		StateManager stateManager = TrackerManager.Instance.GetStateManager ();



// Radian debug
//		foreach(TrackableBehaviour tb in stateManager.GetTrackableBehaviours()){
//			Debug.Log("\t" + tb.Trackable.ID);
//		}

		if (o!= null){
			foreach ( DataSet d in o.GetDataSets() ){
				if (d.Path.Contains(dataSetName)){
					foreach ( Trackable t in d.GetTrackables() )
						stateManager.DestroyTrackableBehavioursForTrackable (t,false);	

					o.DeactivateDataSet(d) ;
					o.DestroyDataSet(d,false);
					Debug.Log(dataSetName + " deactivated!") ;
					break;
				}			
			}			
		}

		Debug.Log(dataSetName + " deactivated");
	}
}

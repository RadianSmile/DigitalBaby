/*==============================================================================
Copyright (c) 2010-2014 Qualcomm Connected Experiences, Inc.
All Rights Reserved.
Confidential and Proprietary - Qualcomm Connected Experiences, Inc.
==============================================================================*/

using UnityEngine;
using UnityEngine.Events;

namespace Vuforia
{
    /// <summary>
    /// A custom handler that implements the ITrackableEventHandler interface.
    /// </summary>
	public class RadianTrackableEventHandler : MonoBehaviour,
                                                ITrackableEventHandler
    {
        #region PRIVATE_MEMBER_VARIABLES

        private TrackableBehaviour mTrackableBehaviour;
    
        #endregion // PRIVATE_MEMBER_VARIABLES

		public GameObject responseObject ; 
		public bool isBeingTracked = false  ; 

		[Header ("WaitFor")]
		public float waitForSeconds = -1f ; 
		public UnityEvent onWaitEnough ;

		[Header("Events")]
		public UnityEvent onFound ; 
		public UnityEvent onLost ; 


        #region UNTIY_MONOBEHAVIOUR_METHODS
    
        void Start()
        {
            mTrackableBehaviour = GetComponent<TrackableBehaviour>();
            if (mTrackableBehaviour)
            {
                mTrackableBehaviour.RegisterTrackableEventHandler(this);
            }
        }

        #endregion // UNTIY_MONOBEHAVIOUR_METHODS



        #region PUBLIC_METHODS


        /// <summary>
        /// 找到時：先開啟 responseObject，隨後觸發找到事件
		/// 消失時：先觸發事件，然後關閉 responseObject 
        /// </summary>
        public void OnTrackableStateChanged(
                                        TrackableBehaviour.Status previousStatus,
                                        TrackableBehaviour.Status newStatus)
        {
            if (newStatus == TrackableBehaviour.Status.DETECTED ||
                newStatus == TrackableBehaviour.Status.TRACKED ||
                newStatus == TrackableBehaviour.Status.EXTENDED_TRACKED)
            {
				Debug.Log(name + " " + "tracker found.");
				if (responseObject != null) responseObject.SetActive(true);
				onFound.Invoke() ;
				checkFoundThread(waitForSeconds);
				isBeingTracked = true ; 

            }
            else
            {
				Debug.Log(name + " " + "tracker lost.");
				onLost.Invoke() ;
				isBeingTracked = false; 
				if (responseObject != null) responseObject.SetActive(false);

            }
        }


        #endregion // PUBLIC_METHODS

		public void checkFoundThread (float trackWaitTime){
			StartCoroutine(checkTarget(GetComponent<RadianTrackableEventHandler>() ,trackWaitTime ));
		}

		System.Collections.IEnumerator checkTarget ( RadianTrackableEventHandler target ,float trackWaitTime){
			float duration = 0f ; 
			while ( duration <= trackWaitTime ){
				yield return new WaitForSeconds(.05f);
				Debug.Log(duration);
				if (!target.isBeingTracked)  break ;
				duration += 0.05f ; 
			}
			if (duration > trackWaitTime){
				onWaitEnough.Invoke() ;
			}
		}

    }
}

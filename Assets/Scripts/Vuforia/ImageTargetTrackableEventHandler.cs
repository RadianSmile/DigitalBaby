/*============================================================================== 
 * Copyright (c) 2012-2014 Qualcomm Connected Experiences, Inc. All Rights Reserved. 
 * ==============================================================================*/

using UnityEngine;
using System ;
using UnityEngine.Events ; 
using Vuforia;

/// <summary>
/// A custom handler that implements the ITrackableEventHandler interface.
/// </summary>
public class ImageTargetTrackableEventHandler : MonoBehaviour,
                                            ITrackableEventHandler
{
	 
    #region PUBLIC_MEMBER_VARIABLES
    public bool isBeingTracked;

    #endregion PUBLIC_MEMBER_VARIABLES
    
    #region PRIVATE_MEMBER_VARIABLES
    private TrackableBehaviour mTrackableBehaviour;
    #endregion // PRIVATE_MEMBER_VARIABLES

	public Canvas canvas ; 

    #region PUBLIC_METODS
    void Start()
    {
        mTrackableBehaviour = GetComponent<TrackableBehaviour>();
        if (mTrackableBehaviour)
        {
            mTrackableBehaviour.RegisterTrackableEventHandler(this);
        }
    }
    

    /// <summary>
    /// Implementation of the ITrackableEventHandler function called when the
    /// tracking state changes.
    /// </summary>
    public void OnTrackableStateChanged(
                                    TrackableBehaviour.Status previousStatus,
                                    TrackableBehaviour.Status newStatus)
    {
        if (newStatus == TrackableBehaviour.Status.DETECTED ||
            newStatus == TrackableBehaviour.Status.TRACKED ||
            newStatus == TrackableBehaviour.Status.EXTENDED_TRACKED)
        {

			if (gameObject.activeInHierarchy){ // rn.md 
            	OnTrackingFound();
			}
        }
        else
        {
			
				OnTrackingLost();
        }
    }

    #endregion // PUBLIC_METHODS



    #region PRIVATE_METHODS
    private void OnTrackingFound()
    {
        isBeingTracked = true;

		Target target = GetComponent<Target>() ; 
		if (target != null) target.Tracked () ; 


        Renderer[] rendererComponents = GetComponentsInChildren<Renderer>(true);
        Collider[] colliderComponents = GetComponentsInChildren<Collider>(true);

        // Enable rendering:
        foreach (Renderer component in rendererComponents)
        {
            if(ImageTargetUIEventHandler.ExtendedTrackingIsEnabled)
            {
                if(component.gameObject.name == "tower")
                {
                     component.enabled = true;
                }
            }
            else if(component.gameObject.name == "teapot")
            {
                component.enabled = true;
            }
        }

        // Enable colliders:
        foreach (Collider component in colliderComponents)
        {
             if(ImageTargetUIEventHandler.ExtendedTrackingIsEnabled)
                {
                    if(component.gameObject.name == "tower")
                    {
                         component.enabled = true;
                    }
                }
            else if(component.gameObject.name == "teapot")
            {
                component.enabled = true;
            }
        }
		if (canvas != null)
			canvas.gameObject.SetActive(true);


        Debug.Log("Trackable " + mTrackableBehaviour.TrackableName + " found");
    }


    private void OnTrackingLost()
    {
        isBeingTracked = false;
		Renderer[] rendererComponents = GetComponentsInChildren<Renderer>(true);
        Collider[] colliderComponents = GetComponentsInChildren<Collider>(true);

        // Disable rendering:
        foreach (Renderer component in rendererComponents)
        {
            component.enabled = false;
        }

        // Disable colliders:
        foreach (Collider component in colliderComponents)
        {
            component.enabled = false;
        }

		if (canvas != null)
			canvas.gameObject.SetActive(false);
		
        Debug.Log("Trackable " + mTrackableBehaviour.TrackableName + " lost");
    }

    #endregion // PRIVATE_METHODS
}

using UnityEngine;
using System.Collections;

[RequireComponent( typeof( AudioSource))]
public class PlaySound : MonoBehaviour {
	public AudioSource audioSource ; 
	public void PlayAudio (AudioClip x){
		audioSource.PlayOneShot(x) ; 
	}
	public void StopAudio (){
		audioSource.Stop();
	}
}

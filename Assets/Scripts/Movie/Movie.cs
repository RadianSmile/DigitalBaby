using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System ; 
//[RequireComponent (typeof(AudioSource))]

public class Movie : MonoBehaviour {



//	public MovieTexture movie ;
//	public AudioSource audio ;
	// Use this for initialization
	void Awake () {
//		GetComponent<RawImage>().texture =  movie as MovieTexture ; 
//		audio = GetComponent<AudioSource>() ; 
//		audio.clip = movie.audioClip ; 

	}


	public void Play (Action movieEndCallback){
//		movie.Stop();
//		movie.Play();
//		audio.Stop();
//		audio.Play();
//		StartCoroutine(FindEnd(movieEndCallback));
	}

	public void Play (){
//		movie.Stop();
//		movie.Play();
//		audio.Stop();
//		audio.Play();
	
	}

	private IEnumerator FindEnd(Action callback)
	{
//		while(movie.isPlaying)
//		{
//			yield return 0;
//		}
//		
		callback();
		yield break;
	}



	// Update is called once per frame
	void Update () {
//		if (Input.GetKeyDown(KeyCode.Space)){
//			if (movie.isPlaying) {
//				movie.Pause() ;
//			}else{
//				movie.Play(); 
//			}
//		}
	}
}

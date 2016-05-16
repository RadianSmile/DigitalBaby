using UnityEngine;
using UnityEngine.Events;
using System.Collections;
using System ; 

public class PickerLinker : MonoBehaviour {


	public ImgPicker[] pickers ; 

	// Use this for initialization
	void Start () {
		foreach (ImgPicker imgPicker in pickers){
			imgPicker.OnUserUpdate.RemoveAllListeners() ;
			imgPicker.OnUserUpdate.AddListener(genListener(imgPicker));
		}
	}
	public UnityAction genListener (ImgPicker changedImgPicker){
		return ()=>{
			foreach (ImgPicker imgPicker in pickers){
				imgPicker.SetCurretIndex( changedImgPicker.currentIndex ) ; 
			}
			GetComponent<ImgPickers>().update();
		} ;
	}
}

using UnityEngine;
using UnityEngine.Events;
using System.Collections;
using TeaLiqueur.T2 ; 

public class ImgPickers : MonoBehaviour {




	public ImgPickerIniter[] ImgPickerIniters ; 
	public ImgOption[] options ; 
	public string answer ; 
	public string currentValue ; 

	public UnityEvent OnPass ; 

	public bool pass ;
	public bool running ; 

	string _value ; 
	public string Value {
		get {  
			_value = "" ; 
			foreach  (ImgPickerIniter pickerIniter in ImgPickerIniters)
				if (pickerIniter.imgPicker != null) _value += pickerIniter.imgPicker.currentValue.ToString() ; 

			return _value ; 
		}
	}
	void Start () {
		pass = false; 
		initChildren();

	}


	public void  initChildren (){
		foreach (ImgPickerIniter  pickerIniter in ImgPickerIniters){
			pickerIniter.imgPicker.SetOptions (options) ;
			pickerIniter.imgPicker.SetInitIndex(pickerIniter.initIndex);
			pickerIniter.imgPicker.Init() ;
		}
	}


	public void reset (){
		foreach  (ImgPickerIniter  pickerIniter in ImgPickerIniters)
			if (pickerIniter.imgPicker != null) pickerIniter.imgPicker.Init();
	}

	public void update () {

		currentValue = "" ; 
		foreach (ImgPickerIniter  pickerIniter in ImgPickerIniters)
			currentValue += pickerIniter.imgPicker.currentValue ; 


		GetComponent<TimeCounter>().enabled = true ;
		GetComponent<TimeCounter>().duration = 0f ; 


		pass = currentValue == answer ; 

//		checkAnswer();
	}
		
	public void checkAnswer (){
//		MAIN.instantce.teamData.PathDataDict[2].Answer(currentValue);
//		if ( currentValue == answer ){
//			MAIN.instantce.AudioPlayRight();
//			OnPass.Invoke ();
//		}else {
//			MAIN.instantce.AudioPlayWrong();
//		}

	}
}

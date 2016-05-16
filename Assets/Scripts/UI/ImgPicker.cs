using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using System.Collections;

using TeaLiqueur.T2 ; 
public class ImgPicker : MonoBehaviour {

	// Use this for initialization
	public Image imgElement ; 
	public UnityEvent OnUserUpdate  ;
	private int initIndex = 0  ; 

	public string excludeValue ="" ; 

	public void SetInitIndex (int i){
		initIndex = i ; 
	}

	private ImgOption[] valueArray ; 
	public string currentValue {
		get { return _currentValue ;}
	}
	public int currentIndex {
		get { return _currentIndex ;}
	}

	string _currentValue ; 
	int _currentIndex ; 

	void OnEnable (){
		if (valueArray != null)
			SetCurretIndex(initIndex) ; 
		//		checkData () ; 
		//		Reset() ; 
		//
		//		updateValue();
	}


	public void SetOptions (ImgOption[] options){
		valueArray = options ; 
		updateValue();
	}

	void checkData (){
		if (valueArray == null)

		if (valueArray.Length == 0 ){
			//			Debug.LogError("value array have nothing") ;
			valueArray[0] = new ImgOption () ; 
		}
	}
	public void Init (){
		_currentIndex = initIndex ;
		updateValue () ; 
	}

	public void Up (){

		do {
			if (_currentIndex == valueArray.Length - 1 ) {
				_currentIndex = 0 ; 
			}else {
				_currentIndex++ ; 
			}
		}while (excludeValue.Contains(valueArray[_currentIndex].txt)) ;


		updateValue();
		OnUserUpdate.Invoke();
	}

	public void Down (){
		do {
			if (_currentIndex == 0 ) {
				_currentIndex = valueArray.Length - 1 ;
			}else{
				_currentIndex-- ; 		
			}
		}while (excludeValue.Contains(valueArray[_currentIndex].txt));


		updateValue(); 
		OnUserUpdate.Invoke();
	}
	public bool SetCurretIndex( int newIndex  ){
		
		if (_currentIndex >= 0 && _currentIndex < valueArray.Length ) {
			if (excludeValue.Contains(valueArray[_currentIndex].txt)) return false ;
			_currentIndex = newIndex ; 
			updateValue () ; 

			return true ; 
		}
		return false ;
	}

	public void updateValue (){
		_currentValue = valueArray[_currentIndex].txt;
		imgElement.sprite = valueArray[_currentIndex].img ; 
	}

}

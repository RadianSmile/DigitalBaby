using UnityEngine;
using UnityEngine.UI;
using System.Collections;


public class Picker : MonoBehaviour {

	// Use this for initialization
	public Text textElement ; 
	private char[] valueArray ; 
	public string currentValue {
		get { return _currentValue ;}
	}
	public int currentIndex {
		get { return _currentIndex ;}
	}

	string _currentValue ; 
	int _currentIndex ; 

	void OnEnable (){
//		checkData () ; 
//		Reset() ; 
//
//		updateValue();
	}

	public void SetOptions ( char[] options){
		valueArray = options ; 
		updateValue();
	}

	void checkData (){
		if (valueArray == null)


		if (valueArray.Length == 0 ){
//			Debug.LogError("value array have nothing") ;
			valueArray[0] = ' ';
		}
	}
	public void Reset (){
		_currentIndex = 0 ;
	}

	public void Up (){
		if (valueArray.Length > 0 ){
			if (_currentIndex == valueArray.Length - 1 ) {
				_currentIndex = 0 ; 
			}else {
				_currentIndex++ ; 
			}			
		}
		updateValue();
		
	}
	public void Down (){
		if (valueArray.Length > 0 ){
			if (_currentIndex == 0 ) {
				_currentIndex = valueArray.Length - 1 ;
			}else{
				_currentIndex-- ; 		
			}
		}
		updateValue();
	}

	public void updateValue (){
		_currentValue = valueArray[_currentIndex].ToString();
		textElement.text = currentValue;
	}

}

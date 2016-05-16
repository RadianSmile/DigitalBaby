using UnityEngine;
using System.Collections;

public class Pickers : MonoBehaviour {


	public Picker[] pickers ; 
	public string optionString ;
	public char[] options ; 
	public string answer ; 
	public string currentValue ; 

	public delegate void OnPass ()  ; 
	public OnPass onPass ; 

	public bool pass ;
	public bool running ; 

	string _value ; 
	public string Value {
		get {  
			_value = "" ; 
			foreach  (Picker picker in pickers)
				if (picker)_value += picker.currentValue.ToString() ; 

			return _value ; 
		}

	}
	void Start () {
		pass = false; 
		options = optionString.ToCharArray (); 
		initChildren();

	}
	

	public void  initChildren (){
		foreach (Picker picker in pickers){
			picker.SetOptions (options)  ; 

		}
	}


	public void reset (){
		foreach  (Picker picker in pickers)
			if (picker) picker.Reset();
	}

	void Update () {

		currentValue = "" ; 
		foreach (Picker picker in pickers)
			currentValue += picker.currentValue ; 

		pass = currentValue == answer ; 

		if (pass && onPass != null && !running) {
			onPass ();
			running = true ;
		} 

	} 
}

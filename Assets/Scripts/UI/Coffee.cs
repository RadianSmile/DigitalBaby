using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Coffee : MonoBehaviour {

	// Use this for initialization
	public bool selectable = false ;
	public bool changeMilkVal = false  ; 
	public bool changeCoffeeVal = false  ; 

	public float milkVal = 0f ; 
	public float coffeeVal = 0f  ; 

	public GameObject boble ; 
	public Text milkText ; 
	public Text coffeeText ; 

	public GameObject milkCtrl ; 
	public GameObject coffeeCtrl  ; 

	void OnEnable (){

		milkCtrl.SetActive(changeMilkVal);
		coffeeCtrl.SetActive(changeCoffeeVal);

	}

	void Start(){

		milkCtrl.SetActive(changeMilkVal);
		coffeeCtrl.SetActive(changeCoffeeVal);
	}

	void Update(){
		milkText.text = "" + milkVal.ToString("#0.0") ; 
		coffeeText.text = "" + coffeeVal.ToString("#0.0");

	}

//	public bool addMilk (){
//		if (!changeMilkVal || coffeeVal >= 5) return false ; 
//		milkVal += .5f ; 
//		return true ; 
//	}
//
//	public bool addCoffee (){
//		if (!changeCoffeeVal || coffeeVal >= 5) return false ; 
//		coffeeVal += .5f ; 
//		return true ; 
//	}
//
//	public bool minusMilk (){
//		if (!changeMilkVal || coffeeVal <= 0) return false ; 
//		milkVal -= .5f ; 
//		return true ; 
//	}
//	
//	public bool minusCoffee (){
//		if (!changeCoffeeVal || coffeeVal <= 0) return false ; 
//		coffeeVal -= .5f ; 
//		return true ; 
//	}


	public void addMilk (){
		if (!changeMilkVal || milkVal >= 5) return  ; 
		milkVal += .5f ; 
		return  ; 
	}
	
	public void addCoffee (){
		if (!changeCoffeeVal || coffeeVal >= 5) return  ; 
		coffeeVal += .5f ; 
		return  ; 
	}
	
	public void minusMilk (){
		if (!changeMilkVal || milkVal <= 0) return  ; 
		milkVal -= .5f ; 
		return  ; 
	}
	
	public void minusCoffee (){
		if (!changeCoffeeVal || coffeeVal <= 0) return  ; 
		coffeeVal -= .5f ; 
		return  ; 
	}


	public void reset (){
		if (changeMilkVal){
			milkVal = 0 ; 
		}
		if (changeCoffeeVal){
			coffeeVal = 0 ; 
		}
	}


}

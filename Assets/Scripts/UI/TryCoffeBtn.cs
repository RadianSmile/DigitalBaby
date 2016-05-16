using UnityEngine;
using System.Collections;

public class TryCoffeBtn : MonoBehaviour {

	public GameObject[] dots ; 
	public int value = 0  ; 
	bool changed = false; 
	// Use this for initialization
	void Start () {
	
	}
	void Update(){
		for(int i = 0 ; i < dots.Length ; i++){
			if (value - 1 >= i  ){
				dots[i].SetActive(true);
			}else {
				dots[i].SetActive(false);
			}
		}
	}
	// Update is called once per frame
	public void add (){
		if (value< 5){
			value += 1 ; 
		}
	}
	public void reset (){

		value = 0 ;
	}
}

using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using DigitalBaby ;
public class Ctrl_Mission2 : MonoBehaviour {


	public GameObject imageContainer ; 

	public QuestionUIAbstract ChoiceUI ;
	public QuestionUIAbstract SelectionUI ;

	bool questioning ;

	public Dictionary<string ,QuestionData> QuestionDataDict = new Dictionary<string ,QuestionData> {
		{"0", new QuestionData( QuestionType.CHOICE, "測試123" , new string[]{ "8"})  }
	};

	private Image[] images ;


	void Start (){
		images  = imageContainer.GetComponentsInChildren<Image>() ;
	}



	public void Found ( string imgName){

		// 當使用者在答題時忽略所有辨識。
		if (questioning ) return ; 


		// 跳出對應的樣板
		questioning = false ; 
		QuestionData q = QuestionDataDict[imgName] ;

		switch(q.type){
		case QuestionType.CHOICE :
			ChoiceUI.Init(q);
			break;
		
		case QuestionType.SELECTION :
			SelectionUI.Init(q);
			break;

		default : break ; 
			
		}

	}
}

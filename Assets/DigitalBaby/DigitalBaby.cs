using UnityEngine;
using System.Collections;
using DigitalBaby ; 
namespace DigitalBaby {

	public enum QuestionType {
		CHOICE , SELECTION
	}
		
	public class QuestionData  {
		public QuestionType type ; 
		public string description ; 
		public string[] answers ; 
		public QuestionData (QuestionType type , string description , string[] answers ){
			type = type ;
			description = description ;
			answers = answers ; 
		}


		public bool Answer (int choiceIndex){
			return Answer(choiceIndex.ToString()) ; 
		}
		public bool Answer (string answer){
			foreach(var s in answers){
				if( answer.Contains(answer))
					return true ;
			}
			return false ; 
		}
	}

}


public abstract class QuestionUIAbstract : MonoBehaviour {

	public abstract void Init (QuestionData questionData);

}

public class ChoiceUI :QuestionUIAbstract {
	
	public override void Init (QuestionData questionData){
		return ;
	}
}
public  class SelectionUI :QuestionUIAbstract {

	public override void Init (QuestionData questionData){
		return ;
	}

}
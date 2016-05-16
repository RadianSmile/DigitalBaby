using UnityEngine;
using System.Collections;

public class Ctrl_Mission1Start: RadianPageCtrl  {

	// Use this for initialization


	public void Callback_Mission1Start(){
		TIME.instance.Mission1Start();
		GotoNext();
	}
}

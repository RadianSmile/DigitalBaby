using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System ;


public class f : MonoBehaviour {

	// Use this for initialization



	public class Obj {
		public delegate void MyDelegate () ;
		public MyDelegate myDelegate ;
	}

	List<Action> actions = new List<Action>();
	List<Obj> objs = new List<Obj>() ;
	public delegate void MyDelegate () ;
	MyDelegate myDelegate ;


	void Start () {
	

		for (int counter = 0; counter < 10; counter++)
		{
			int copy = counter;
			actions.Add(() =>{ 
				Debug.Log(copy);
			});

			var b = new Obj() ;
			myDelegate += ()=>{
				Debug.Log(copy);
			};


			objs.Add(b);


		}

		// Then execute them
		foreach (Action action in actions)
		{
			action();
		}

		myDelegate();

	}
	
	// Update is called once per frame
	void Update () {
	
	}
}

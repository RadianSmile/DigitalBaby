using UnityEngine;
using System.Collections;

public class RadianPageMgr : MonoBehaviour {
	public Transform parent ; 
	public void Instantiate ( GameObject page){
		GameObject g = GameObject.Instantiate(page) ;
		g.transform.SetParent( parent.transform ); 
		RectTransform r = (RectTransform) g.transform ; 

		r.offsetMax = Vector2.one ; 
		r.offsetMin = Vector2.zero ;
		r.localScale = Vector3.one ;
		r.localPosition = Vector3.zero;

//		return  g ;
	}
}

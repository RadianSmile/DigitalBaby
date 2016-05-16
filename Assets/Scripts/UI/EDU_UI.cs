using UnityEngine;
using UnityEngine.UI ; 
using System.Collections;
using System ; 
using System.Collections.Generic ; 

[RequireComponent (typeof(CanvasGroup))]

public class EDU_UI : MonoBehaviour
{
	// http://forum.unity3d.com/threads/tweening-a-panel-on-off-screen-with-a-screen-space-canvas.307739/
	public static GameObject Preground ;
	public static GameObject Background ;



	[HideInInspector]
	public static Vector3 offScreenLeftPosition = new Vector3(-1024, 0, 0);
	[HideInInspector]
	public static Vector3 offScreenRightPosition = new Vector3( 1024, 0, 0); // Screen.width;
	[HideInInspector]
	public static Vector3 centerPosition = new Vector3(0, 0, 0);

	[HideInInspector]
	public static Vector3 offScreenUpPosition = new Vector3( 0, 768, 0);
	[HideInInspector]
	public static Vector3 offScreenDownPosition = new Vector3(  0, -768, 0); // Screen.width;


	[HideInInspector]
	public RectTransform rectTransform  ; 
	[HideInInspector]
	public Rect rect  ; 
	[HideInInspector]
	public CanvasGroup canvasGroup = null ; 
	// Use this for initialization


	public Vector3 originPosition  ;


	public enum POSITION {
		LEFT,CENTER,RIGHT,UP,DOWN,ORIGIN
	};
	
	public enum TRANSITION {
		FADETO_WHITE , 
		FADETO_BLACK ,
		FADE_FROM_BLACK,
		FADE_FROM_WHITE,
		FADE_IN , 
		FADE_OUT , 

		SLIDE_UP,
		SLIDE_DOWN,
		SLIDE_LEFT,
		SLIDE_RIGHT,
		SLIDE_CENTER,
		SLIDE_ORIGIN,
		NONE
	};
	public static Dictionary<POSITION, Vector3> PositionMap = new Dictionary<POSITION, Vector3>(){
		{POSITION.LEFT , offScreenLeftPosition },
		{POSITION.UP , offScreenUpPosition },
		{POSITION.CENTER , centerPosition },
		{POSITION.DOWN , offScreenDownPosition },
		{POSITION.RIGHT , offScreenRightPosition },
		{POSITION.ORIGIN , centerPosition }
	};
		

	void Awake()
	{      
//		Debug.LogError("colorFader");
		if (GetComponent<RectTransform>().localPosition != originPosition){
			originPosition = GetComponent<RectTransform>().localPosition;
		}

		if (Preground == null){
			Preground = GameObject.Find("P_PREGROUND") ; 
//			RectTransform r = Preground.GetComponent<RectTransform>() ; 
			Preground.SetActive(false) ;
			Debug.Log(Preground);
		}

		if (Background == null){
			Background = GameObject.Find("P_BACKGROUND") ; 
//			RectTransform r = Background.GetComponent<RectTransform>() ; 
			Background.SetActive(false) ;
			Debug.Log(Background);
		}


//		Debug.Log ("Awaken");
		rectTransform = gameObject.GetComponent<RectTransform>() ; 
		rect = rectTransform.rect ; 
		canvasGroup = gameObject.GetComponent<CanvasGroup>() ;
	}
	void Start(){
		if (GetComponent<RectTransform>().localPosition != originPosition){
			originPosition = GetComponent<RectTransform>().localPosition;
		}
		if (canvasGroup == null)
			canvasGroup = gameObject.GetComponent<CanvasGroup>() ;
		
	}
	void OnEnable(){
		if (canvasGroup == null)
			canvasGroup = gameObject.GetComponent<CanvasGroup>() ;
	}

	public void moveTo (POSITION to , float duration  ){
		moveTo ( to ,  duration , null ) ;  
	}
	public void moveTo (POSITION to , float duration , Action callback ){
		LeanTween.cancel(gameObject);
		Vector3 toVector = PositionMap[to] ; 
		if (to == POSITION.ORIGIN){
			toVector = originPosition ; 
			Debug.LogWarning(originPosition);
		}

		LTDescr anim = LeanTween.move(rectTransform,toVector, duration).setEase(LeanTweenType.easeInOutExpo);
		if (callback != null)
			anim.setOnComplete(callback); 

	}

	public void moveFromTo (POSITION from ,POSITION to ,  float duration , Action callback ){
		LeanTween.cancel(gameObject);
		rectTransform.localPosition = PositionMap[from];
		LeanTween.move(rectTransform, PositionMap[to],duration ).setEase(LeanTweenType.easeInOutExpo);  
	}

	public void show()
	{
		Debug.Log ("showing");
		LeanTween.cancel(this.gameObject);
		rectTransform.localPosition = offScreenLeftPosition;
		LeanTween.move(rectTransform, centerPosition, 3f).setEase(LeanTweenType.easeOutQuint);      
	}
///// <summary>
///// 	old !!
///// </summary>
//	public void fadeIn (){
//
//		if (canvasGroup == null)
//			canvasGroup = gameObject.GetComponent<CanvasGroup>() ;
//		
//		gameObject.SetActive(true);
////		Debug.Log ("fadeIn") ; 
//		LeanTween.cancel(this.gameObject);
//		canvasGroup.alpha = 0f ;
//		LeanTween.value( gameObject, canvasGroup.alpha, 1f, 1f).setOnUpdate( (float val)=>{ 
//			canvasGroup.alpha = val ;
//		} );
//	}
///// <summary>
///// 	old
///// </summary>
///// <param name="callBack">Call back.</param>
//	public void fadeIn (Action callBack){
//
//		if (canvasGroup == null)
//			canvasGroup = gameObject.GetComponent<CanvasGroup>() ;
//
//		gameObject.SetActive(true);
//		LeanTween.cancel(this.gameObject);
//		canvasGroup.alpha = 0f ;
//		LeanTween.value( gameObject, canvasGroup.alpha, 1f, 1f).setOnUpdate( (float val)=>{ 
//			canvasGroup.alpha = val ;
//		} ).setOnComplete(callBack) ;
//	}
//
///// <summary>
///// 	這裡是新的
///// </summary>
///// <param name="duration">Duration.</param>

	public void fadeIn (){
		fadeIn (1f , null) ; 
	}
	public void fadeIn (float duration ){
		fadeIn (duration , null) ; 
	}
	public void fadeIn (float duration , Action callback ){

		if (canvasGroup == null)
			canvasGroup = gameObject.GetComponent<CanvasGroup>() ;

		gameObject.SetActive(true);
		LeanTween.cancel(this.gameObject);
		canvasGroup.alpha = 0f ;
		LTDescr anim =  LeanTween.value( gameObject, canvasGroup.alpha, 1f, duration).setOnUpdate( (float val)=>{ 
			canvasGroup.alpha = val ;
		} );

		if (callback != null)
			anim.setOnComplete(callback) ; 
	}

	public void fadeOut (){
		fadeOut (1f , null) ; 
	}
	public void fadeOut ( float duration){
		fadeOut (duration , null) ;
	}
	public void fadeOut ( float duration , Action callback){
		LeanTween.cancel(this.gameObject);

		if (canvasGroup == null)
			canvasGroup = gameObject.GetComponent<CanvasGroup>() ;

		LTDescr anim = LeanTween.value( gameObject, canvasGroup.alpha, 0f, duration).setOnUpdate( (float val)=>{ 
			canvasGroup.alpha = val ;
		} );

		if (callback != null)
			anim.setOnComplete(callback + SetInActive) ;
	}

	public void fadeToColor (  ){
		
	}
		

	private void SetInActive(){
		gameObject.SetActive(false);
	}
		
	public void scaleIn()
	{
		rectTransform.localScale = new Vector3 (0f,0f,0f);
		rectTransform.localPosition = centerPosition ;
		LeanTween.scale( rectTransform, new Vector2(1f,1f), 2f ).setEase( LeanTweenType.easeOutQuad );
		LeanTween.move ( rectTransform, new Vector2(0 ,0) , 2f ).setEase( LeanTweenType.easeOutQuad);
	}

	public void fadeToColor ( Color to , float duration , Action callback ){
		Color from = new Color ( to.r,to.g,to.b,0f) ;
		fadeFromToColor ( from , to , duration , callback );
	}
	public void fadeFromColor ( Color from , float duration , Action callback ){
		Color to = new Color ( from.r,from.g,from.b,0f) ;
		fadeFromToColor ( from , to , duration , callback );
	}

	public void fadeFromToColor (Color from , Color to , float duration ,  Action callback){
		
//		RectTransform r = Preground.GetComponent<RectTransform>();

//		Preground.GetComponent<RectTransform>().localScale = gameObject.GetComponent<RectTransform>().localScale;
//		Preground.transform.SetParent(gameObject.transform ) ; 
		Preground.SetActive(true);
//		Preground.GetComponent<RectTransform>().offsetMax = new Vector2 (0f ,0f );

//		LeanTween.cancel(Preground);
		LTDescr anim = LeanTween.value( Preground, from, to, duration ).setOnUpdate( (Color val)=>{ 
//			Debug.Log("tweened val:"+val);
			Preground.GetComponent<Image>().color = val ;
		});

		if (callback != null){
			anim.setOnComplete ( ()=>{
				callback() ; 
				if (Preground.GetComponent<Image>().color.a == 0f ){
					Preground.SetActive(false);
				}
			});
		}else {
			anim.setOnComplete ( ()=>{
//				callback() ; 
				if (Preground.GetComponent<Image>().color.a == 0f ){
					Preground.SetActive(false);
				}
			});
		}
	}

	public void Transit (TRANSITION transition, float duration){
		Transit ( transition,  duration , null) ; 
	}
	public void Transit (TRANSITION transition, float duration, Action callback ){

		switch(transition){

		case TRANSITION.FADE_IN : 
			fadeIn(duration ,callback ) ; break ;

		case TRANSITION.FADE_OUT : 
			fadeOut(duration ,callback ) ; break ; 

		case TRANSITION.FADE_FROM_BLACK : 
			fadeFromColor( new Color(0f,0f,0f,1f) , duration , callback); break ;

		case TRANSITION.FADE_FROM_WHITE : 
			fadeFromColor( new Color(1f,1f,1f,1f) ,duration , callback); break ;

		case TRANSITION.FADETO_BLACK : 
			fadeToColor( new Color(0f,0f,0f,1f) , duration , callback); break ;

		case TRANSITION.FADETO_WHITE : 
			fadeToColor( new Color(1f,1f,1f,1f) ,duration , callback); break ;


		case TRANSITION.SLIDE_CENTER : 
			moveTo(POSITION.CENTER , duration , callback ) ; break ;

		case TRANSITION.SLIDE_UP : 
			moveTo(POSITION.UP , duration , callback ) ; break ;

		case TRANSITION.SLIDE_DOWN : 
			moveTo(POSITION.DOWN , duration , callback ) ; break ;

		case TRANSITION.SLIDE_LEFT : 
			moveTo(POSITION.LEFT , duration , callback ) ; break ;

		case TRANSITION.SLIDE_RIGHT : 
			moveTo(POSITION.RIGHT , duration , callback ) ; break ;

		case TRANSITION.SLIDE_ORIGIN : 
			moveTo(POSITION.ORIGIN , duration , callback ) ; break ;

		default : break  ;
		}
	}
}





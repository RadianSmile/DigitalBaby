using UnityEngine;
using System.Collections;
using UnityEngine.UI ; 
using UnityEngine.Events ; 
using UnityEngine.EventSystems ; 


public class CircleArrow : MonoBehaviour {
	public static int UnitPortionAngle = 30 ; 
	// Use this for initialization
//	private Vector3 currentMousePos ; 
	private Vector3 originMousePos ; 
	private Vector3 originPos ; 
//	public GameObject arrowImg ; 
	public UnityEvent onTuned ; 
	public GameObject circle;
	public CircleArrow pre ;
	public CircleArrow after ;
	public int expandPortion ;
	public float expandAngle {
		get  {
			return _expandAngle ; 
		}	
	} 
	[SerializeField]
	private float _expandAngle  ; 

	public int id ; 

	void Start () {
		_expandAngle = 360f - transform.localEulerAngles.z ;  // Rn. dangerous
//		LeanTween.rotateAroundLocal(transform as RectTransform ,Vector3.forward,_angle,10f);
		Debug.LogWarning(_expandAngle);
		updateCircle (_expandAngle);
		fixRotation();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	public float getRotationFromDir (Vector3 dir){
		
		int dirPatch =  Vector3.Cross(Vector3.up , dir ).z < 0 ? -1 : 1 ; 
		float rotation =   	Vector3.Angle( Vector3.up , dir ) * dirPatch  ;
		return rotation ; 
	}

	public float getAngle (float rotation) {
		//  這裡要把 z  轉換成 可以用的角度 (0-160)
		float angle = 0f ; 
		if (rotation < 0 ){
			angle = rotation * -1f ;
		}else if (rotation > 0 ){
			angle = 360f - rotation ; 
		}

		return angle ; 

	}
	public void updateCircle (float angle , bool tween = false){
		if (tween){
			LeanTween.cancel(circle);
			LeanTween.value(circle,circle.GetComponent<Image>().fillAmount, angle / 360f,1f).setOnUpdate((val)=> {
				circle.GetComponent<Image>().fillAmount = val ; 
			}) ;
		}else {
			Debug.Log(angle);
			circle.GetComponent<Image>().fillAmount = angle / 360f ; 
		}
	}
	public void updateArrow (float angle , bool tween = false ) {
		var oa = transform.eulerAngles ; 
		oa.z = 360 - angle ;

		if (tween){
			LeanTween.value(gameObject,transform.eulerAngles,oa,1f).setOnUpdate((Vector3 val)=>{
				transform.eulerAngles = val ;
				_expandAngle = 360 - val.z ; 
			});
		}else {
			transform.eulerAngles = oa ;
		}

//		LeanTween.value(
	}

	public void Tune ( BaseEventData e ) {

		Vector3 dir = Input.mousePosition - transform.position ;
		float rotation = getRotationFromDir (dir) ; 
		float currentAngle = getAngle (rotation) ;
		/// perfect solution ! for draggable object
		//	transform.position = Input.mousePosition - ( arrowImg.transform.position - transform.position ); // # http://answers.unity3d.com/questions/849117/46-ui-image-follow-mouse-position.html

		// angle patch  http://answers.unity3d.com/questions/181867/is-there-way-to-find-a-negative-angle.html

		float preAngle = pre != null ? pre.expandAngle : 0 ; 
		float afterAngle = after != null ? after.expandAngle : 360 ; 
		Debug.Log(currentAngle);

//		if (currentAngle > preAngle &&  currentAngle < afterAngle ){
			_expandAngle = currentAngle ; 
			onTuned.Invoke();

			updateArrow (_expandAngle);
			updateCircle(_expandAngle);
//		}
	}
	public void fixRotation (BaseEventData e = null ){

		int basePortion = (int)_expandAngle / UnitPortionAngle ; 
		float targetAngle = _expandAngle ; 
		if ( targetAngle % UnitPortionAngle> UnitPortionAngle / 2 ){
			targetAngle = (basePortion + 1 ) * UnitPortionAngle;
			expandPortion = (basePortion + 1 );
		}else {
			targetAngle = basePortion * UnitPortionAngle ; 
			expandPortion = (basePortion  );
		}

		updateCircle(targetAngle,true);
		updateArrow(targetAngle,true);
		
	}
	public int getPortion (){
		if (pre == null){
			return expandPortion ; 
		}else {
			return expandPortion - pre.expandPortion ; 
		}
	}
}

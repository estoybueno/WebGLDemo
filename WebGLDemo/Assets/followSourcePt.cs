using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class followSourcePt : MonoBehaviour {
	public Vector3 targetPos;
	public Vector3 targetRot;
	private Quaternion targetRotQuat = new Quaternion();
	private Quaternion initRot;
	private Vector3 initPos;
	private Rigidbody rb;
	// Use this for initialization
	void Start () {
		initPos = this.transform.position;
		initRot = this.transform.rotation;
		targetRotQuat.eulerAngles = targetRot;
		rb = GetComponent<Rigidbody> ();
		//StartCoroutine ("positionOverTime");
	}
	void OnEnable(){
		StartCoroutine ("positionOverTime");
	}
	IEnumerator positionOverTime(){//move from init to target over 1 second
		while(this.transform.position != targetPos){
			this.transform.position = Vector3.Lerp(this.transform.position, targetPos, 0.01f) ;
			this.transform.rotation = Quaternion.Lerp(this.transform.rotation, targetRotQuat, 0.01f) ;
			yield return new WaitForSecondsRealtime(0.01f);
		}

	}
	void OnDisable(){
		this.transform.position = initPos;//may not be necessary
		this.transform.rotation = initRot;
	}
}

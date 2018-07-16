using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SegueCam : MonoBehaviour {

	private Transform target;
	private Vector3 posCurrent, currentCam;

	private void Start() {
		target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
		
	}

	private void Update() {

		posCurrent = target.position - transform.position;
		transform.position = Vector3.Lerp(transform.position,target.position + new Vector3(0,1,0),TimeAndSpeedControl.instance.speedCam*Time.deltaTime);

		Quaternion rotation = Quaternion.LookRotation(posCurrent);
		Quaternion current = transform.localRotation;

		transform.localRotation = Quaternion.Slerp(current, rotation, 20.0f *Time.deltaTime);

	}
}

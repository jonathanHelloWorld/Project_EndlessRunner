using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SegueCam : MonoBehaviour {

	private Transform target;
	private Vector3 posCurrent;

	private void Start() {
		target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
		posCurrent = transform.position - target.position;
	}

	private void Update() {

		transform.position = target.position + posCurrent;

	}
}

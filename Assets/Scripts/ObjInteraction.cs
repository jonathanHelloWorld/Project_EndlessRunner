using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjInteraction : MonoBehaviour {

	private void OnCollisionExit(Collision collision) {
		Debug.Log("foi");
		if (collision.gameObject.CompareTag("Player")) {
			gameObject.SetActive(false);
		}

	}

	private void OnCollisionEnter(Collision collision) {
		Debug.Log("eaee");
	}
}

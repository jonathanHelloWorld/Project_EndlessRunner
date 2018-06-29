using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerController : MonoBehaviour {

	private void OnTriggerEnter(Collider other) {
		Debug.Log("eaeee");
	}

	private void OnTriggerExit(Collider other) {
		//gameObject.SetActive(false);
	}

}

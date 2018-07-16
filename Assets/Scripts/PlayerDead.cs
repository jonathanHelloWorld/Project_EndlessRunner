using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerDead : MonoBehaviour {

	private Collider _collider;
	Ray ray;

	void Start () {

		_collider = GetComponent<Collider>();

	}
	
	
	void Update () {
		
		RaycastHit hit;
		if (Physics.Raycast(transform.position, transform.forward,out hit, 0.6f,1 << LayerMask.NameToLayer("curva"),QueryTriggerInteraction.Ignore)) {
			Dead();
			Destroy(this.gameObject);
			SceneManager.LoadScene(1);
		}

		if (Physics.Raycast(transform.position, transform.right, out hit, 0.6f, 1 << LayerMask.NameToLayer("curva"), QueryTriggerInteraction.Ignore)) {
			Dead();
			Destroy(this.gameObject);
			SceneManager.LoadScene(1);
		}

		if (Physics.Raycast(transform.position, -transform.right, out hit, 0.6f, 1 << LayerMask.NameToLayer("curva"), QueryTriggerInteraction.Ignore)) {
			Dead();
			Destroy(this.gameObject);
			SceneManager.LoadScene(1);
		}

		Debug.DrawLine(transform.position, transform.forward, Color.red);

	}

	void Dead() {

		TimeAndSpeedControl.instance.result = TimeAndSpeedControl.instance.distance;
		
	}
}

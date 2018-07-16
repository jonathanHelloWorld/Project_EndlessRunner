using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour {

	private Vector3 centerBox;
	private CharacterController controller;

	private float varFloat;

	private bool activeRotate = false;
	private bool activeRight = true;
	private bool condicaoLeft = false;
	private bool condicaoRight = false;

	private bool ajustando = false;
	private bool rightRotate = false;

	private int auxAxis;
	private int aux = 0;

	private Collider _collider;

	void Start() {

		controller = GetComponent<CharacterController>();
		_collider = GetComponent<Collider>();
	}

	void Update() {

		if (activeRotate && Input.GetKeyDown(KeyCode.RightArrow)) {

			ajeitarPosicao(_collider);
			rightRotate = true;
			ajustando = true;
			activeRotate = false;
			activeRight = true;
		}
		else
		if (activeRotate && Input.GetKeyDown(KeyCode.LeftArrow)) {

			ajeitarPosicao(_collider);
			ajustando = true;
			activeRotate = false;
			activeRight = true;

		}
		else
		if (TimeAndSpeedControl.instance.lestGo && activeRight && Input.GetKeyDown(KeyCode.RightArrow) && aux < 1) {
			aux++;
			condicaoRight = true;
		}
		else
		if (TimeAndSpeedControl.instance.lestGo && activeRight && Input.GetKeyDown(KeyCode.LeftArrow) && aux > -1) {
			aux--;
			condicaoLeft = true;
		}

		if (ajustando) {
			OnAjeitandoPos();
		}
		else {
			controller.Move(transform.forward * TimeAndSpeedControl.instance.speed * Time.deltaTime);
		}
		moveLaterais();

	}

	private void OnTriggerEnter(Collider other) {
		if (other.CompareTag("curvaD") || other.CompareTag("curvaE")) {
			activeRotate = true;
			activeRight = false;
			_collider = other;
		}
	}

	void moveLaterais() {
		if (condicaoLeft && TimeAndSpeedControl.instance.contadorSegundos > 0.0f ) {
			controller.Move(-transform.right * TimeAndSpeedControl.instance.speedLado * Time.deltaTime);
			TimeAndSpeedControl.instance.contadorSegundos -= Time.deltaTime;
			activeRight = false;
		}
		else if (condicaoRight && TimeAndSpeedControl.instance.contadorSegundos > 0.0f) {
			controller.Move(transform.right * TimeAndSpeedControl.instance.speedLado * Time.deltaTime);
			TimeAndSpeedControl.instance.contadorSegundos -= Time.deltaTime;
			activeRight = false;
		}
		else {
			TimeAndSpeedControl.instance.contadorSegundos = TimeAndSpeedControl.instance.timeTravel;
			condicaoLeft = false;
			condicaoRight = false;
			activeRight = true;
		}
	}

	void ajeitarPosicao(Collider curva) {

		float Dcenter = Vector3.Distance(transform.position, curva.bounds.center);
		float DRight = Vector3.Distance(transform.position, curva.bounds.center + transform.forward * 2f);
		float DLeft = Vector3.Distance(transform.position, curva.bounds.center + transform.forward * -2f);

		centerBox = new Vector3(curva.bounds.center.x, transform.position.y, curva.bounds.center.z);
		
		if (Dcenter < DRight && Dcenter < DLeft) {
			auxAxis = 0;
			aux = 0;
			//Debug.Log("center");
		}
		else
		if (DRight < Dcenter && DRight < DLeft) {
			auxAxis = 1;
			//Debug.Log("Right");
		}
		else
		if (DLeft < Dcenter && DLeft < DRight) {
			auxAxis = 2;
			//Debug.Log("Left");
		}

	}

	void OnAjeitandoPos() {

		switch (auxAxis) {
			case 0:

				transform.position = Vector3.Lerp(transform.position, centerBox, 2.0f * Time.deltaTime);
				if (Vector3.Distance(transform.position, centerBox) < 0.3f) {
					if (rightRotate) {
						transform.Rotate(0, 90.0f, 0, Space.World);
						rightRotate = false;
					}
					else {
						transform.Rotate(0, -90.0f, 0, Space.World);
					}
					ajustando = false;
				}
				break;
			case 1:

				if (rightRotate)
					varFloat = 2f;
				else
					varFloat = -2f;

				transform.position = Vector3.Lerp(transform.position, centerBox + transform.forward * 2f + transform.right * -2f, 2.0f * Time.deltaTime);
				if (Vector3.Distance(transform.position, centerBox + transform.forward * 2f + transform.right * -2f) < 0.3f) {
					if (rightRotate) {
						transform.Rotate(0, 90.0f, 0, Space.World);
						rightRotate = false;
						aux = -1;
					}
					else {
						transform.Rotate(0, -90.0f, 0, Space.World);
						aux = 1;
					}
					ajustando = false;
				}
				break;
			case 2:

				if (rightRotate)
					varFloat = 2f;
				else
					varFloat = -2f;

				transform.position = Vector3.Lerp(transform.position, centerBox + transform.forward * -2f + transform.right * varFloat, 2.0f * Time.deltaTime);
				if (Vector3.Distance(transform.position, centerBox + transform.forward * -2f + transform.right * varFloat) < 0.3f) {
					if (rightRotate) {
						transform.Rotate(0, 90.0f, 0, Space.World);
						rightRotate = false;
						aux = 1;
					}
					else {
						transform.Rotate(0, -90.0f, 0, Space.World);
						aux = -1;
					}
					ajustando = false;
				}
				break;
			default:
				break;
		}
	}


}

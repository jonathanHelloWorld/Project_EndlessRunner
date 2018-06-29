using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour {

	private Vector3 moveV;
	private CharacterController controller;

	private float speed = 4.0f;
	private float gravityVertical = 0.0f;
	private float gravity = 10.0f;

	void Start() {

		controller = GetComponent<CharacterController>();

	}


	void Update() {
		moveV = Vector3.zero;

		if (controller.isGrounded) {
			gravityVertical = -0.3f;
		}
		else {
			gravityVertical -= gravity * Time.deltaTime;
		}

		/*moveV.x = Input.GetAxisRaw("Horizontal") * speed;

		moveV.y = gravityVertical;

		moveV.z = speed;*/

		controller.Move(transform.forward * speed * Time.deltaTime);

	}
}

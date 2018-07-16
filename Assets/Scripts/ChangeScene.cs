using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour {

	private Button _btn;
	
	void Start () {

		_btn = GetComponent<Button>();

		_btn.onClick.AddListener(() => {
			offsetScene();
			SceneManager.LoadScene(0);
		});

	}

	void Update () {
		
	}

	void offsetScene() {
		TimeAndSpeedControl.instance.distance = 0.0f;
		TimeAndSpeedControl.instance.timeControl = 0.0f;
		TimeAndSpeedControl.instance.speed = 4.0f;
		TimeAndSpeedControl.instance.speedLado = 4.0f;
		TimeAndSpeedControl.instance.speedCam = 1.0f;
		TimeAndSpeedControl.instance.contadorSegundos = 0.5f;
		TimeAndSpeedControl.instance.timeTravel = 0.5f;
		TimeAndSpeedControl.instance.CdTime = 10.0f;
	}
}

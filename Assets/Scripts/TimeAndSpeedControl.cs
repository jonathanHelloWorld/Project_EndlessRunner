using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeAndSpeedControl : Singleton<TimeAndSpeedControl> {

	public float speed;
	public float speedLado;
	public float speedCam;
	public float contadorSegundos;
	public float timeTravel, result = 0f;
	public float CdTime, timeControl, distance;
	public bool lestGo = true;

	void Start() {
		CdTime = 10f;
		timeControl = 0.0f;
		getTimeAndSpeed();
	}



	void Update() {

		timeControl += Time.deltaTime;
		lowerCd();
		getDistance();

	}

	void lowerCd() {
		if (CdTime <= 0.0f && speed < 10.0f) {
			lestGo = false;
			speed += 1.0f;
			speedCam += 0.2f;
			CdTime = 10.0f;
			//getTimeAndSpeed();
		}
		else {
			CdTime -= Time.deltaTime;
			
		}
		lestGo = true;
	}

	float getVelocidade(float timeSugerido) {

		float velocidade = 2.0f / timeSugerido;
		return velocidade;
	}

	float getTime(float velocidadeSugerida) {

		float Time = 2.0f / velocidadeSugerida;
		return Time;
	}

	void getTimeAndSpeed() {
		contadorSegundos = getTime(speedLado);
		speedLado = getVelocidade(contadorSegundos);
		timeTravel = contadorSegundos;
	}

	void getDistance() {

		if (speed < 10.0f) {
			 distance = ((4.0f + speed) / 2) * timeControl;
		}else {
			distance = 10.0f * timeControl;
		}

	}
}

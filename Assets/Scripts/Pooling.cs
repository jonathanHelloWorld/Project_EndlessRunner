using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pooling : MonoBehaviour {

	public List<GameObject> TypesWays;
	public int qtdObjects;

	private int qtdCurrentView = 4;
	private int qtdActiveFalse = 0;
	private int LastNum = -1;
	private int axisIndex = -1;
	private int lastWayIndex;

	private int qtdRetasOpenInZ = 0;
	private int qtdRetasOpenReverse = 0;
	private int qtdRetasAntCurvZ = 0;
	private int qtdRetasAntReverse = 0;

	private int qtdRetaEOne = 0;
	private int qtdRetaETwo = 0;
	private int qtdRetaETree = 0;
	private int qtdRetaEFour = 0;

	private int contWayOpen = 4;

	private Transform playerPos;

	bool primaryWay = true;

	BoxCollider _colliderNew;
	BoxCollider _colliderLast;
	List<GameObject> Ways;

	void Start() {

		playerPos = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();

		Ways = new List<GameObject>();
		for (int i = 0; i < qtdObjects; i++) {

			GameObject objtWay;
			if (i < 3) {
				objtWay = (GameObject)Instantiate(TypesWays[0]);
			}
			else {
				objtWay = (GameObject)Instantiate(TypesWays[RandomNum()]);
			}
			objtWay.SetActive(false);
			Ways.Add(objtWay);

		}

		for (int i = 0; i < qtdCurrentView; i++) {
			Spawn(i);
		}

	}

	void Update() {

		if (Vector3.Distance(playerPos.position, Ways[contWayOpen - 2].transform.position) < 15.0f && contWayOpen < qtdObjects) {
			//Debug.Log(contWayOpen);
			Spawn(contWayOpen);
			contWayOpen++;
			StartCoroutine(desativaWay());
			if (contWayOpen == 42) {
				for (int i = 0; i < qtdCurrentView; i++) {
					Spawn(i);
				}
				contWayOpen = 4;
			}
			if(qtdActiveFalse == 41) {
				qtdActiveFalse = 0;
			}
			
		
		}

	}

	void Spawn(int index) {

		if (primaryWay) {
			qtdRetasOpenInZ++;
			Ways[index].transform.position = transform.position;
			Ways[index].SetActive(true);
			lastWayIndex = index;
			_colliderNew = Ways[index].gameObject.GetComponent<BoxCollider>();
			primaryWay = false;
		}
		else {

			controllerAxis(axisIndex, index);
		}
		_colliderLast = _colliderNew;

	}

	void ActiveFalse(int i) {
		Ways[i].SetActive(false);
	}

	int RandomNum() {

		int randomIndex = LastNum;

		while (randomIndex == LastNum) {

			randomIndex = Random.Range(0, TypesWays.Count);
		}

		LastNum = randomIndex;
		return randomIndex;

	}

	void controllerAxis(int indexAxis, int indexWay) {
		// Debug.Log(axisIndex);
		if (Ways[indexWay].tag == "curvaD")
			axisIndex++;

		if (Ways[indexWay].tag == "curvaE" && axisIndex == -1) {
			axisIndex = 2;
		}else
		if (Ways[indexWay].tag == "curvaE") {
			axisIndex--;
		}

		switch (indexAxis) {
			case -1:

				Ways[indexWay].SetActive(true);
				if (Ways[indexWay].tag == "reta") {
					qtdRetasOpenInZ++;
					qtdRetaEOne++;
				}
					
				if (Ways[indexWay].tag == "curvaD" && qtdRetasOpenReverse >= qtdRetasOpenInZ) {

					Ways[indexWay].transform.position = Ways[lastWayIndex].transform.position;
					Ways[indexWay].SetActive(false);
					axisIndex--;
					break;
				}else
				if (Ways[indexWay].tag == "curvaE" && qtdRetaETwo >= qtdRetaEOne ) {

					Ways[indexWay].transform.position = Ways[lastWayIndex].transform.position;
					Ways[indexWay].SetActive(false);
					axisIndex++;
					break;
				}

				if(Ways[indexWay].tag == "curvaD") {
					qtdRetaETree++;
				}

				if (Ways[indexWay].tag == "curvaE") {
					qtdRetasAntReverse++;
				}
				

				_colliderNew = Ways[indexWay].gameObject.GetComponent<BoxCollider>();

				var z = _colliderLast.bounds.center.z + _colliderLast.bounds.extents.z + _colliderNew.bounds.extents.z - _colliderNew.center.z;
				var x = Ways[lastWayIndex].gameObject.transform.position.x;

				Ways[indexWay].transform.rotation = Quaternion.identity;
				Ways[indexWay].transform.position = new Vector3(x, Ways[lastWayIndex].gameObject.transform.position.y, z);

				lastWayIndex = indexWay;
				qtdRetasAntCurvZ = 0;
				qtdRetaEFour = 0;
				break;

			case 0:

				Ways[indexWay].SetActive(true);
				if (Ways[indexWay].tag == "reta") {
					qtdRetasAntCurvZ++;
					qtdRetaETree++;
				}
					

				if (Ways[indexWay].tag == "curvaD" && qtdRetasAntReverse >= qtdRetasAntCurvZ) {

					Ways[indexWay].transform.position = Ways[lastWayIndex].transform.position;
					Ways[indexWay].SetActive(false);
					axisIndex--;
					break;
				}else
				if (Ways[indexWay].tag == "curvaE" && qtdRetaEFour >= qtdRetaETree) {

					Ways[indexWay].transform.position = Ways[lastWayIndex].transform.position;
					Ways[indexWay].SetActive(false);
					axisIndex++;
					break;
				}

				if (Ways[indexWay].tag == "curvaD") {
					qtdRetaETwo++; 
				}

				if (Ways[indexWay].tag == "curvaE") {
					qtdRetasOpenInZ++;
				}

				_colliderNew = Ways[indexWay].gameObject.GetComponent<BoxCollider>();

				z = Ways[lastWayIndex].gameObject.transform.position.z + _colliderLast.center.z + _colliderLast.center.x; //- _colliderNew.center.x
				x = _colliderLast.bounds.center.x + _colliderLast.bounds.extents.x + _colliderNew.bounds.extents.x - _colliderNew.center.z;

				Ways[indexWay].transform.rotation = Quaternion.identity;
				Ways[indexWay].transform.Rotate(0, 90, 0);
				Ways[indexWay].transform.position = new Vector3(x, Ways[lastWayIndex].gameObject.transform.position.y, z);

				lastWayIndex = indexWay;
				qtdRetasOpenReverse = 0;
				qtdRetaEOne = 0;
				break;

			case 1:

				Ways[indexWay].SetActive(true);
				if (Ways[indexWay].tag == "reta") {
					qtdRetasOpenReverse++;
					qtdRetaETwo++;
				}
					

				if (Ways[indexWay].tag == "curvaD" && qtdRetasOpenInZ >= qtdRetasOpenReverse) {

					Ways[indexWay].transform.position = Ways[lastWayIndex].transform.position;
					Ways[indexWay].SetActive(false);
					axisIndex--;
					break;
				}else
				if (Ways[indexWay].tag == "curvaE" && qtdRetaEOne >= qtdRetaETwo) {

					Ways[indexWay].transform.position = Ways[lastWayIndex].transform.position;
					Ways[indexWay].SetActive(false);
					axisIndex++;
					break;
				}

				if (Ways[indexWay].tag == "curvaD") {
					qtdRetaEFour++;
				}

				if (Ways[indexWay].tag == "curvaE") {
					qtdRetasAntCurvZ++;
				}

				_colliderNew = Ways[indexWay].gameObject.GetComponent<BoxCollider>();

				z = _colliderLast.bounds.center.z - _colliderLast.bounds.extents.z - _colliderNew.bounds.extents.z + _colliderNew.center.z;
				x = Ways[lastWayIndex].gameObject.transform.position.x + _colliderLast.center.z + _colliderLast.center.x;

				Ways[indexWay].transform.rotation = Quaternion.identity;
				Ways[indexWay].transform.Rotate(0, 180, 0);
				Ways[indexWay].transform.position = new Vector3(x, Ways[lastWayIndex].gameObject.transform.position.y, z);

				lastWayIndex = indexWay;
				qtdRetasAntReverse = 0;
				qtdRetaETree = 0;
				break;

			case 2:

				Ways[indexWay].SetActive(true);
				if (Ways[indexWay].tag == "reta") {
					qtdRetasAntReverse++;
					qtdRetaEFour++;
				}
					

				if (Ways[indexWay].tag == "curvaD" && qtdRetasAntCurvZ >= qtdRetasAntReverse) {

					Ways[indexWay].transform.position = Ways[lastWayIndex].transform.position;
					Ways[indexWay].SetActive(false);
					axisIndex--;
					break;
				}else
				if (Ways[indexWay].tag == "curvaE" && qtdRetaETree >= qtdRetaEFour) {

					Ways[indexWay].transform.position = Ways[lastWayIndex].transform.position;
					Ways[indexWay].SetActive(false);
					axisIndex++;
					break;
				}

				_colliderNew = Ways[indexWay].gameObject.GetComponent<BoxCollider>();

				z = Ways[lastWayIndex].gameObject.transform.position.z - _colliderLast.center.z - _colliderLast.center.x;
				x = _colliderLast.bounds.center.x - _colliderLast.bounds.extents.x - _colliderNew.bounds.extents.x + _colliderNew.center.z;

				Ways[indexWay].transform.rotation = Quaternion.identity;
				Ways[indexWay].transform.Rotate(0, 270, 0);
				Ways[indexWay].transform.position = new Vector3(x, Ways[lastWayIndex].gameObject.transform.position.y, z);
				
				lastWayIndex = indexWay;
				qtdRetaETwo = 0;
				break;

			case 3:

				Ways[indexWay].SetActive(true);
				
				_colliderNew = Ways[indexWay].gameObject.GetComponent<BoxCollider>();

				z = _colliderLast.bounds.center.z + _colliderLast.bounds.extents.z + _colliderNew.bounds.extents.z - _colliderNew.center.z;
				x = Ways[lastWayIndex].gameObject.transform.position.x - _colliderLast.center.z - _colliderLast.center.x;

				Ways[indexWay].transform.rotation = Quaternion.identity;
				Ways[indexWay].transform.position = new Vector3(x, Ways[lastWayIndex].gameObject.transform.position.y, z);

				qtdRetasOpenInZ = 1;
				qtdRetaEOne = 1;
				lastWayIndex = indexWay;

				if (Ways[indexWay].tag == "curvaE") {
					axisIndex = 2;
				}else 
				if (Ways[indexWay].tag == "curvaD"){
					axisIndex = 0;
				}
				else {
					axisIndex = -1;
				}

				break;
			default:
				break;
		}


	}

	IEnumerator desativaWay() {
		yield return new WaitForSeconds(5f);
		ActiveFalse(qtdActiveFalse);
		qtdActiveFalse++;

	}

}




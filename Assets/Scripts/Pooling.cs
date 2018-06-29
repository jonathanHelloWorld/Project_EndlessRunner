using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pooling : MonoBehaviour {

	public List<GameObject> TypesWays;
	public int qtdObjects;

	private int qtdCurrentView = 10;
	private int qtdActiveFalse = 0;
	private int LastNum = -1;
	private int axisIndex = -1;

	private Transform playerPos;

	private Transform childT;
	private Transform childF;

	private float spawnZ = 0.0f;
	private float safeZ = 10.0f;

	bool primaryWay = true;

	BoxCollider _colliderNew;
	Collider _colliderLast;
	Vector3 center;
	Vector3 size, sizeOther;
	List<GameObject> Ways;

	void Start() {

		playerPos = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();

		Ways = new List<GameObject>();
		for (int i = 0; i < qtdObjects; i++) {

			GameObject objtWay = (GameObject)Instantiate(TypesWays[0]);
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

		/*if (playerPos.position.z - safeZ > (spawnZ - qtdCurrentView * wayLengthZ) && contWayOpen < qtdObjects) {
			Spawn(contWayOpen);
			contWayOpen++;
			ActiveFalse(qtdActiveFalse);
			qtdActiveFalse++;
		}
		*/
	}

	void Spawn(int index) {

		if (primaryWay) {
			Ways[index].transform.position = transform.position;
			Ways[index].SetActive(true);

			_colliderNew = Ways[index].gameObject.GetComponent<BoxCollider>();
			primaryWay = false;
		}
		else {

			Ways[index].SetActive(true);

			childT = Ways[index].gameObject.transform.GetChild(0);
			
			_colliderNew = Ways[index].gameObject.GetComponent<BoxCollider>();


			//var z = _colliderLast.bounds.center.z + _colliderLast.bounds.extents.z + _colliderNew.bounds.extents.z - _colliderNew.center.z;
			//var x = Ways[index - 1].gameObject.transform.position.x; //- _colliderNew.center.x;

			//Ways[index].transform.position = new Vector3(x, Ways[index - 1].gameObject.transform.position.y, z);
			//Ways[index].gameObject.transform.SetParent(childT);

			_colliderNew.transform.Rotate(childF.localEulerAngles);			
			Ways[index].transform.localPosition = childF.position + new Vector3(0,0, _colliderNew.bounds.extents.z - _colliderNew.center.z);




		}

		childF = Ways[index].gameObject.transform.GetChild(1);

		Debug.Log(Ways[index].gameObject.transform.GetChild(1));
		Debug.Log(childF);
		Debug.Log(childF.position);

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
		

		if (Ways[indexWay].tag == "curvaD")
			axisIndex++;

		switch (indexAxis) {
			case -1:

				Ways[indexWay].SetActive(true);

				_colliderNew = Ways[indexWay].gameObject.GetComponent<BoxCollider>();

				var z = _colliderLast.bounds.center.z + _colliderLast.bounds.extents.z + _colliderNew.bounds.extents.z - _colliderNew.center.z;
				var x = Ways[indexWay - 1].gameObject.transform.position.x; //- _colliderNew.center.x;

				Ways[indexWay].transform.position = new Vector3(x, Ways[indexWay - 1].gameObject.transform.position.y, z);
				break;

			case 0:

				Ways[indexWay].SetActive(true);

				_colliderNew = Ways[indexWay].gameObject.GetComponent<BoxCollider>();

				z = Ways[indexWay - 1].gameObject.transform.position.x; //- _colliderNew.center.x
				x = _colliderLast.bounds.center.x + _colliderLast.bounds.extents.x + _colliderNew.bounds.extents.z - _colliderNew.center.z;

				Ways[indexWay].transform.position = new Vector3(x, Ways[indexWay - 1].gameObject.transform.position.y, z);
				break;
		}

		
	}


}




using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CanvasControl : MonoBehaviour {

	private Text text;
	
	void Start () {

		text = GetComponent<Text>();

	}
	
	
	void Update () {

		text.text = TimeAndSpeedControl.instance.distance.ToString("00") + "m";

	}
}

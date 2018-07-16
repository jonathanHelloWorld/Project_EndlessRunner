using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GetText : MonoBehaviour {

	private Text _text;
	
	void Start () {

		_text = GetComponent<Text>();
		_text.text += TimeAndSpeedControl.instance.result.ToString("00") + "m";

	}
	
}

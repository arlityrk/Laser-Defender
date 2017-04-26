using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreKeeper : MonoBehaviour {

	private Text textObject;
	public static int score = 0;

	void Start() {
		textObject = GetComponent<Text> ();
		textObject.text = string.Concat("SCORE: ", score.ToString());
	}

	public void Score (int points) {
		score += points;
		textObject.text = string.Concat("SCORE: ", score.ToString());
	}

	public static void Reset() {
		score = 0;
		//textObject.text = string.Concat("SCORE: ", score.ToString());
	}
}

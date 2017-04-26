using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreDisplay : MonoBehaviour {

	void Start() {
		Text scoreText = GetComponent<Text> ();
		scoreText.text = ScoreKeeper.score.ToString();
		ScoreKeeper.Reset ();
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthTracker : MonoBehaviour {

	public int playerHealth;
	public int initialPlayerHealth = 1000;
	Text textObject;

	// Use this for initialization
	void Start () {
		textObject = GetComponent<Text> ();
		playerHealth = initialPlayerHealth;
		textObject.text = string.Concat("HEALTH: ", playerHealth.ToString ());
		textObject.color = Color.green;
	}
	
	public void updateHealth(int addHealth){
		playerHealth -= addHealth;
		if(playerHealth < 0){
			playerHealth = 0;
		}
		textObject.text = string.Concat("HEALTH: ", playerHealth.ToString ());
		if(playerHealth <=initialPlayerHealth / 2 && playerHealth > initialPlayerHealth / 4){
			textObject.color = Color.yellow;
		}
		else if (playerHealth <=initialPlayerHealth / 4) {
			textObject.color = Color.red;
		}
	}

	public int getHealth(){
		return playerHealth;
	}

	public void resetHealth() {
		playerHealth = initialPlayerHealth;
	}
}

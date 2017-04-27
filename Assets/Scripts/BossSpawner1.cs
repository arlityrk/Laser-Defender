using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossSpawner1 : MonoBehaviour {

	public GameObject bossPrefab;
	private bool bossSpawned = false;

	void Update(){
		spawnBoss ();
	}

	void spawnBoss() {
		if (Wave.wave == 3 && bossSpawned == false) {
			GameObject boss = Instantiate (bossPrefab, this.transform.GetChild(0).position, Quaternion.identity);
			boss.transform.parent = this.transform.parent;
			bossSpawned = true;
		}
	}
}

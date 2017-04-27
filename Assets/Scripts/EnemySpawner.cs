using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour {

	public GameObject smallEnemyPrefab;
	public GameObject mediumEnemyPrefab;
	public float width = 12.5f;
	public float height = 9.5f;
	public float enemySpeed = 3;
	public float spawnDelay = 0.5f;

	private bool movingRight = true;
	private float minX;
	private float maxX;

	void Start () {
		float distance = this.transform.position.z - Camera.main.transform.position.z;
		Vector3 leftEdge = Camera.main.ViewportToWorldPoint (new Vector3(0,0,distance));
		Vector3 rightEdge = Camera.main.ViewportToWorldPoint (new Vector3(1,0,distance));
		minX = leftEdge.x;
		maxX = rightEdge.x;

		SpawnEnemies ();
	}
		
	void Update () {

		float rightEdgeOfFormation = transform.position.x + (0.5f*width);
		float leftEdgeOfFormation = transform.position.x - (0.5f*width);

		if (movingRight) {
			this.transform.position += Vector3.right * enemySpeed * Time.deltaTime;
		} else {
			this.transform.position += Vector3.left * enemySpeed * Time.deltaTime;
		}

		if (rightEdgeOfFormation >= maxX) {
			movingRight = false;
		} else if(leftEdgeOfFormation <= minX) {
			movingRight = true;
		}

		if(AllMembersDead()) {
			if (Wave.wave != 3) {
				SpawnUntilFull ();
				Debug.Log ("wave: "+ Wave.wave);
			}
		}
	}

	void OnDrawGizmos() {
		Gizmos.DrawWireCube (this.transform.position, new Vector3 (width, height));
	}

	void SpawnEnemies() {
			foreach (Transform child in this.transform) {
				GameObject enemy = Instantiate (smallEnemyPrefab, child.transform.position, Quaternion.identity) as GameObject;
				enemy.transform.parent = child;
			}
	}

	void SpawnUntilFull() {
		GameObject enemyPrefab = smallEnemyPrefab;

		if(Wave.wave >= 2){
			enemyPrefab = mediumEnemyPrefab;
		}

		Transform freePosition = NextFreePosition ();

		if(freePosition) {
			GameObject enemy = Instantiate (enemyPrefab, freePosition.position, Quaternion.identity) as GameObject;
		enemy.transform.parent = freePosition;
		}

		if (freePosition) {
			Invoke ("SpawnUntilFull", spawnDelay);
		}
	}

	Transform NextFreePosition(){
		foreach(Transform childPositionGameObject in this.transform) {
			if(childPositionGameObject.childCount == 0) {
				return childPositionGameObject.transform;
			}
		}
		return null;
	}

	bool AllMembersDead() {
		foreach(Transform childPositionGameObject in this.transform) {
			if(childPositionGameObject.childCount > 0){
				return false;
			}
		}
		return true;
	}
}

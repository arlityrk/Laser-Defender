using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehavior : MonoBehaviour {

	public float health = 200f;
	public GameObject enemyProjectile;
	public float enemyProjectileSpeed = 5f;
	public float shotsPerSeconds = 0.5f;
	public bool enemyArrived = false;
	public int smallEnemyKillScore = 50;
	public AudioClip enemyFireClip;
	public AudioClip enemyDeathClip;
	public GameObject explosion;

	private ScoreKeeper scoreKeeper;

	void Start() {
		scoreKeeper = GameObject.Find ("Score").GetComponent<ScoreKeeper> ();
	}

	void OnTriggerEnter2D(Collider2D col) {
		if(enemyArrived){
			Projectile missile = col.gameObject.GetComponent<Projectile> ();
			if(missile){
				health -= missile.getDamage ();
				missile.Hit ();
				if(health <= 0){
					Destroy (this.gameObject);
					scoreKeeper.Score (smallEnemyKillScore);
					AudioSource.PlayClipAtPoint (enemyDeathClip, this.transform.position);
					Instantiate (explosion, transform.position, Quaternion.identity);
				}
			}
		}
	}

	void enemyFire(){
		GameObject enemyBeam = Instantiate (enemyProjectile, this.transform.position, Quaternion.identity) as GameObject;
		enemyBeam.GetComponent<Rigidbody2D> ().velocity = new Vector2 (0, -enemyProjectileSpeed);
		AudioSource.PlayClipAtPoint (enemyFireClip, this.transform.position);
	}

	void Update() {
		//TODO: valemi selgitud loeng 113
		if(enemyArrived){
			float probability = Time.deltaTime * shotsPerSeconds;
			if(Random.value < probability){
			enemyFire ();
			}
		}
	}
}

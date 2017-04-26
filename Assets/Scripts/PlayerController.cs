using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Threading;

public class PlayerController : MonoBehaviour {

	public float playerMoveSpeed;
	public float padding = 1.0f;
	public GameObject projectile;
	public float projectileSpeed;
	public float fireRate;
	public AudioClip playerFire;
	public GameObject explosion;

	private float playerMinXPos;
	private float playerMaxXPos;
	PlayerHealthTracker playerHealthTracker;

	void Start () {
		float distance = transform.position.z - Camera.main.transform.position.z;
		Vector3 leftMost = Camera.main.ViewportToWorldPoint (new Vector3(0,0,distance));
		Vector3 rightMost = Camera.main.ViewportToWorldPoint (new Vector3(1,0,distance));
		playerMinXPos = leftMost.x + padding;
		playerMaxXPos = rightMost.x - padding;

		playerHealthTracker = GameObject.Find ("Health").GetComponent<PlayerHealthTracker> ();
	}

	void OnTriggerEnter2D(Collider2D col){
		Projectile enemyMissile = col.gameObject.GetComponent<Projectile> ();
		if(enemyMissile){
			playerHealthTracker.updateHealth (enemyMissile.getDamage ());
			enemyMissile.Hit ();
			if(playerHealthTracker.getHealth() <= 0){
				Die ();
			}
		}
	}

	void Die(){
		LevelManager levelManager = GameObject.Find ("LevelManager").GetComponent<LevelManager> ();
		Destroy (gameObject);
		Instantiate (explosion, transform.position, Quaternion.identity);
		levelManager.LoadLevel ("End");
	}

	void Fire() {
		GameObject beam = Instantiate (projectile, this.transform.position, Quaternion.identity) as GameObject;
		beam.GetComponent<Rigidbody2D> ().velocity = new Vector2 (0, projectileSpeed);
		AudioSource.PlayClipAtPoint (playerFire, this.transform.position);
	}

	void Update () {
		//InvokeRepeating has a bug with 0 time value. Because of this use > 0
		if(Input.GetKeyDown("space")){
			InvokeRepeating ("Fire",0.000001f,fireRate);
		}

		if(Input.GetKeyUp("space")){
			CancelInvoke ("Fire");
		}

		//Time.deltaTime makes movement independent from framerate
		if(Input.GetButton("Horizontal") && Input.GetAxisRaw("Horizontal") < 0){
			this.transform.position += Vector3.left * playerMoveSpeed * Time.deltaTime;
		}
		else if(Input.GetButton("Horizontal") && Input.GetAxisRaw("Horizontal") > 0) {
			this.transform.position += Vector3.right * playerMoveSpeed * Time.deltaTime;
		}

		float newX = Mathf.Clamp(this.transform.position.x,playerMinXPos,playerMaxXPos);
		this.transform.position = new Vector3(newX,this.transform.position.y,this.transform.position.z);
	}
}

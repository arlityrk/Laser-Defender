using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour {

	public int damage;

	void Start(){
		damage = 300;
	}

	public int getDamage (){
		return damage;
	}

	public void Hit(){
		Destroy(this.gameObject);
	}
}

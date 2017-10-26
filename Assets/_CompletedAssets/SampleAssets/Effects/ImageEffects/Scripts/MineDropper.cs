using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CompleteProject
{
	public class MineDropper : MonoBehaviour {


		public GameObject projectile;
		public float timeBetweenBullets = 0.25f;        // The time between each shot.
		// public float range = 100f;                      // The distance the gun can fire.
		float timer;      
		private float throwRange = 0f;
		public float throwRangeModifier = 1000f;
		public float maxThrowRange = 1000;
		float buttonHeld = 0f;
	

		void Update(){
			
			timer += Time.deltaTime;
			if (Input.GetButton ("Fire1")) {

				throwRange += Time.deltaTime * throwRangeModifier;
				if (throwRange > maxThrowRange)	throwRange = maxThrowRange;
				
			}
			if(Input.GetButtonUp("Fire1")  && timer >= timeBetweenBullets && Time.timeScale != 0){
				timer = 0f;
				GameObject bullet = Instantiate (projectile, transform.position+(transform.forward*2), Quaternion.identity) as GameObject;
				bullet.GetComponent<Rigidbody> ().AddForce (transform.forward * throwRange);
				throwRange = 0;
			}
		}
	}

}
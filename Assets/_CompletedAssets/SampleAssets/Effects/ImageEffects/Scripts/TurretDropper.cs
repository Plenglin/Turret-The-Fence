using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CompleteProject
{
	public class TurretDropper : MonoBehaviour {


		public GameObject turret;
		public int ammo = 3;
		public int maxActive = 1;
		private int dropped = 0;
		private float timer = 0f;


		void Update(){

			timer += Time.deltaTime;

			if (Input.GetButton ("Fire2")) {

				if (ammo >= 1 && dropped < maxActive && timer >= 0.25f) { 
					GameObject bullet = Instantiate (turret, transform.position, Quaternion.identity) as GameObject;
					bullet.GetComponent<TurretDestroyer> ().SetTurretDropper (this);
					ammo--;
					dropped++;
					timer = 0f;
				}

			}
		}
		public void turretDies(bool ammoRet){
			if (ammoRet)ammo++;
			dropped--;
		}
	}

}
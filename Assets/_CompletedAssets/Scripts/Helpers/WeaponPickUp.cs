using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CompleteProject{
	public class WeaponPickUp : MonoBehaviour {
		public GameObject PickUp;
		// Use this for initialization

		void OnTriggerEnter(Collider other)
		{
			if (other.tag == "Player") {

				PickUp.transform.position = GameObject.Find ("GunMount").transform.position;
				PickUp.transform.rotation = GameObject.Find ("GunMount").transform.rotation;
				other.gameObject.GetComponent<PlayerWeapons> ().AddWeapon(PickUp);
				PickUp.transform.parent = other.gameObject.transform;
				Destroy (gameObject);


			}
		}

	}
}
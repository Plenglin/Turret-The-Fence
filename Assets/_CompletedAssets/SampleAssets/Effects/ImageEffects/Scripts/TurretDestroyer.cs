using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CompleteProject{
	public class TurretDestroyer : MonoBehaviour {


		public float lifeTime;
		public bool returnAmmo = true;
		public TurretDropper TD;
		 

		void Start ()
		{


		}
		void Update () {
			lifeTime -= Time.deltaTime;
			if (lifeTime <= 0) {
				//bool am = returnAmmo;
				TD.turretDies (returnAmmo);
				Destroy (gameObject);
			}

		}
		public void SetTurretDropper(TurretDropper TDR){
			TD = TDR;
		}

	}
}
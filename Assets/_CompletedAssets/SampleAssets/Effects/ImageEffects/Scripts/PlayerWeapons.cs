using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CompleteProject
{
public class PlayerWeapons : MonoBehaviour {


		public List<GameObject> GunList = new List<GameObject>();
		private GameObject CurrentGun;
		private int GunIndex = 0;
		public float timeBetweenBullets = 0.25f;        // The time between each shot.
		// public float range = 100f;                      // The distance the gun can fire.
		float timer;  

			void Awake ()
			{
			if(GunList.Count>0)GunList[GunIndex].SetActive (true);

			}
		public void AddWeapon(GameObject weapon){

			GunList.Add (weapon);
		}
			


			void Update ()
			{
				timer += Time.deltaTime;
			if(Input.GetKeyDown(KeyCode.Q) && timer >= timeBetweenBullets && Time.timeScale != 0 && GunList.Count>0)
				{
				timer = 0f;
				GunList[GunIndex].SetActive (false);
				GunIndex++;
					if (GunIndex >= GunList.Count) GunIndex = 0;					
					GunList[GunIndex].SetActive (true);

				}
			}


	
		}
	}
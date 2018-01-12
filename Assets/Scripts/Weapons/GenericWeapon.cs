using System.Collections;
using System.Collections.Generic;
using TurretTheFence.Weapons.Ammo;
using TurretTheFence.Weapons.Firing;
using UnityEngine;
using UnityEngine.UI;

namespace TurretTheFence.Weapons {

    public class GenericWeapon : MonoBehaviour {
        
        public MonoBehaviour firingManager, ammoManager;

        public float fireDelay;

        private float lastFired;
        private IAmmoManager ammo;
        private IFiringManager firing;
        private Text ammoIndicator;

        // Use this for initialization
        private void Start() {
            ammo = ammoManager as IAmmoManager;
            firing = firingManager as IFiringManager;
            ammoIndicator = GameObject.FindGameObjectWithTag("AmmoIndicator").GetComponent<Text>();
        }

        private void FixedUpdate() {
            if (Input.GetButton("Fire1") && Time.time >= lastFired + fireDelay && ammo.CanFire()) {
                ammo.OnFire();
                firing.OnFire();
                lastFired = Time.time;
            }
            if (Input.GetKeyDown("r") && ammo.CanReload()) {
                ammo.OnReload();
            }
        }

        // Update is called once per frame
        private void Update() {
            ammoIndicator.text = ammo.GetAmmoIndicatorText();
        }
    }

}
using System;
using System.Collections;
using System.Collections.Generic;
using TurretTheFence.Player;
using TurretTheFence.Weapons.Ammo;
using TurretTheFence.Weapons.Firing;
using UnityEngine;
using UnityEngine.UI;

namespace TurretTheFence.Weapons {

    public class GenericWeapon : MonoBehaviour, WeaponEventListenable {

        public MonoBehaviour firingManager;
        public MonoBehaviour ammoManager;

        public float fireDelay;
        public bool canFireOverride = true;

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
            if (canFireOverride && Input.GetButton("Fire1") && Time.time >= lastFired + fireDelay && ammo.CanFire()) {
                bool consumeAmmo = firing.OnFire();
                if (consumeAmmo) {
                    ammo.OnFire();
                }
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

        public void SetFiringEnabled(bool enabled) {
            canFireOverride = enabled;
        }

        public void OnAddedToInventory(WeaponMode weaponMode) {

        }
    }

    [System.Serializable]
    public class WeaponData {

        public WeaponData(string name, GameObject obj) {
            this.name = name;
            this.obj = obj;
        }
        public string name;
        public GameObject obj;
    }

}
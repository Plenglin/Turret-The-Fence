using CompleteProject;
using System;
using System.Collections;
using System.Collections.Generic;
using TurretTheFence.Player;
using TurretTheFence.Weapons.Firing;
using UnityEngine;
using UnityEngine.UI;

namespace TurretTheFence.Weapons.Weapons {

    public class Minigun : MonoBehaviour, WeaponEventListenable {

        public MonoBehaviour muzzleObject;

        private IFiringManager muzzle;

        public float heatPerShot, cooldownTime;
        public float firingDelay, spinUpTime, spinDownTime;

        public float heat = 0;
        public Light muzzleFlash, overheat;
        public GameObject spinner;
        public int barrels = 6;
        public bool canFireOverride = true;

        private float maxSpinRate, currentSpinRate, spinUpRate, spinDownRate, cooldownRate, nextFire = 0, heatLightIntensity;
        private bool spinning = true, overheated = false;
        private Text ammoIndicator;

        private RelativePlayerMovement playerMovement;
        private int shootables;

        private void Awake() {
            shootables = LayerMask.GetMask("Shootable", "BulletObstacle");
            maxSpinRate = 360 / barrels / firingDelay;
            spinUpRate = maxSpinRate / spinUpTime;
            spinDownRate = maxSpinRate / spinDownTime;
            cooldownRate = 1 / cooldownTime;
            heatLightIntensity = overheat.intensity;

            ammoIndicator = GameObject.FindWithTag("AmmoIndicator").GetComponent<Text>();
            playerMovement = GameObject.FindWithTag("Player").GetComponent<RelativePlayerMovement>();
            muzzle = (IFiringManager)muzzleObject;
        }

        // Use this for initialization
        void Start() {
            shootables = LayerMask.GetMask("Shootable", "BulletObstacle");
            maxSpinRate = 360 / barrels / firingDelay;
            spinUpRate = maxSpinRate / spinUpTime;
            spinDownRate = maxSpinRate / spinDownTime;
            cooldownRate = 1 / cooldownTime;
            heatLightIntensity = overheat.intensity;

            playerMovement = GameObject.FindGameObjectWithTag("Player").GetComponent<RelativePlayerMovement>();
        }

        // Update is called once per frame
        void Update() {
            string output = String.Format("Heat: {0}%\nMinigun", (int)(heat * 100));
            if (!canFireOverride) return;

            bool firing = Input.GetButton("Fire1");
            spinning = firing || Input.GetButton("Fire2");
            if (spinning) {
                currentSpinRate += spinUpRate * Time.deltaTime;
                playerMovement.hindered = true;
            } else {
                currentSpinRate = Math.Max(currentSpinRate - spinDownRate * Time.deltaTime, 0);
                playerMovement.hindered = false;
            }
            if (currentSpinRate > maxSpinRate) {
                currentSpinRate = maxSpinRate;
                if (firing && !overheated && Time.time >= nextFire) {
                    heat += heatPerShot;
                    nextFire = Time.time + firingDelay;
                    muzzle.OnFire();
                }
            }
            if (overheated || !firing) {
                heat -= cooldownRate * Time.deltaTime;
            }
            if (overheated) {
                output = "OVERHEATED!\n" + output;
            }
            spinner.transform.Rotate(0, 0, currentSpinRate * Time.deltaTime);
            overheat.intensity = heatLightIntensity * heat;
            if (heat >= 1) {
                heat = 1;
                overheated = true;
            } else if (heat <= 0) {
                heat = 0;
                overheated = false;
            }
            ammoIndicator.text = output;
        }

        public void SetFiringEnabled(bool enabled) {
            canFireOverride = enabled;
        }

        public void OnAddedToInventory(WeaponMode weaponMode) {

        }
    }
    
}

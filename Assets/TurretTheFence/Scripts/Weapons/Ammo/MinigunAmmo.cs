using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace TurretTheFence.Weapons.Ammo {

    public class MinigunAmmo : MonoBehaviour, IAmmoManager {

        public float heat = 0;
        public float heatPerShot;
        private bool spinning = false, overheated = false;
        public Light overheat;

        private float heatLightIntensity;

        private void Awake() {
            heatLightIntensity = overheat.intensity;
        }

        public bool CanFire() {
            return spinning && !overheated;
        }

        public bool CanReload() {
            return false;
        }

        public string GetAmmoIndicatorText() {
            string output = String.Format("Heat: {0}%\nMinigun", (int)(heat * 100));
            if (overheated) {
                output = "OVERHEATED!\n" + output;
            }
            return output;
        }

        public void OnFire() {
            Debug.Log("firing");
            heat += heatPerShot;
        }

        public void OnReload() {
            throw new NotImplementedException();
        }

        // Use this for initialization
        void Start() {
        }

        // Update is called once per frame
        void Update() {
            if (heat >= 1) {
                heat = 1;
                overheated = true;
            } else if (heat <= 0) {
                heat = 0;
                overheated = false;
            }
            overheat.intensity = heatLightIntensity * heat;
        }
    }

}

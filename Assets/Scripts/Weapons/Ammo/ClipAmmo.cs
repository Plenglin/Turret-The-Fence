using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TurretTheFence.Weapons.Ammo {

    public class ClipAmmo : MonoBehaviour, IAmmoManager {

        public int clipSize;
        public int bullets;

        public float reloadTime;
        public bool reloading;

        // Use this for initialization
        private void Start() {
            bullets = clipSize;
        }

        public bool CanFire() {
            return bullets > 0 && !reloading;
        }

        public void OnFire() {
            bullets--;
        }

        private IEnumerator Reload() {
            reloading = true;
            yield return new WaitForSeconds(reloadTime);
            bullets = clipSize;
            reloading = false;
        }

        public bool CanReload() {
            return bullets < clipSize;
        }

        public void OnReload() {
            StartCoroutine(Reload());
        }

        public string GetAmmoIndicatorText() {
            string output;
            if (reloading) {
                output = "RELOADING";
            } else {
                output = string.Format("{0}/{1}", bullets, clipSize);
            }
            return output + "\nAssault Rifle";
        }

    }

}
using System;
using System.Collections;
using System.Collections.Generic;
using TurretTheFence.Utils;
using UnityEngine;

namespace TurretTheFence.Player {

    public class WeaponSwitcher : MonoBehaviour {

        public WeaponMode weaponMode = new WeaponMode();
        private Mode[] modeCycle;
        private int cycleIndex = 0;
        private GameObject currentWeapon;

        private void Awake() {
            modeCycle = new Mode[] { weaponMode };
        }

        void Start() {
            foreach (Mode m in modeCycle) {
                m.OnStart();
            }
        }

        public void CycleWeaponMode() {
            cycleIndex = (cycleIndex + 1) % modeCycle.Length;
        }

        void Update() {
            float scroll = Input.GetAxis("Mouse ScrollWheel");
            Mode current = modeCycle[cycleIndex];
            if (scroll > 0) {
                current.OnScrollIndex(-1);
            } else if (scroll < 0) {
                current.OnScrollIndex(1);
            }
            foreach (KeyValuePair<KeyCode, int> p in Constants.keyToNumber) {
                if (Input.GetKeyDown(p.Key)) {
                    current.OnSwitchIndex(p.Value - 1);
                    break;
                }
            }
            if (Input.GetKeyDown(KeyCode.Tab)) {
                CycleWeaponMode();
            }
        }

    }

    interface Mode {
        void OnStart();
        void OnEnable();
        void OnDisable();
        void OnSwitchIndex(int index);
        void OnScrollIndex(int direction);
    }

    [System.Serializable]
    public class WeaponMode : Mode {

        public List<GameObject> guns = new List<GameObject>();
        public int index = 0;

        public void OnStart() {
            OnDisable();
            OnEnable();
        }

        public void OnEnable() {
            SwitchTo(index);
        }

        public void OnDisable() {
            guns.ForEach(go => go.SetActive(false));
        }

        public void OnSwitchIndex(int index) {
            if (index < guns.Capacity) {
                SwitchTo(index);
            }
        }

        private void SwitchTo(int index) {
            guns[this.index].SetActive(false);
            this.index = index;
            guns[this.index].SetActive(true);
        }
        
        public void OnScrollIndex(int direction) {
            int count = guns.Capacity;
            SwitchTo((index + direction + count) % count);
        }
    }

}
using System;
using System.Collections;
using System.Collections.Generic;
using TurretTheFence.Utils;
using UnityEngine;
using UnityEngine.UI;

namespace TurretTheFence.Player {

    public class WeaponSwitcher : MonoBehaviour {

        public WeaponMode weaponMode = new WeaponMode();
        public TurretMode turretMode = new TurretMode();
        private Mode[] modeCycle;
        private int cycleIndex = 0;
        private GameObject currentWeapon;

        public Mode currentMode {
            get { return modeCycle[cycleIndex]; }
        }

        private void Awake() {
            modeCycle = new Mode[] { weaponMode, turretMode };
        }

        void Start() {
            foreach (Mode m in modeCycle) {
                m.OnStart();
            }
        }

        public void CycleWeaponMode() {
            currentMode.OnDisable();
            cycleIndex = (cycleIndex + 1) % modeCycle.Length;
            currentMode.OnEnable();
        }

        void Update() {
            float scroll = Input.GetAxis("Mouse ScrollWheel");
            if (scroll > 0) {
                currentMode.OnScrollIndex(-1);
            } else if (scroll < 0) {
                currentMode.OnScrollIndex(1);
            }
            foreach (KeyValuePair<KeyCode, int> p in Constants.keyToNumber) {
                if (Input.GetKeyDown(p.Key)) {
                    currentMode.OnSwitchIndex(p.Value - 1);
                    break;
                }
            }
            if (Input.GetKeyDown(KeyCode.Tab)) {
                CycleWeaponMode();
            }
        }

    }

    public interface Mode {
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

    [System.Serializable]
    public class TurretMode : Mode {
        public TurretType[] turrets;
        public Text dataDisplay;
        public TurretBuilderTool tool;
        private int index;

        private void SetIndex(int index) {
            this.index = index;
            tool.OnTurretChange(GetTurret());
        }

        public TurretType GetTurret() {
            return turrets[index];
        }

        public void OnStart() {
            OnDisable();
            SetIndex(0);
        }

        public void OnEnable() {
            tool.gameObject.SetActive(true);
        }

        public void OnDisable() {
            tool.gameObject.SetActive(false);
        }

        public void OnSwitchIndex(int index) {
            if (index < turrets.Length) {
                SetIndex(index);
            }
        }

        public void OnScrollIndex(int direction) {
            SetIndex((index + direction + turrets.Length) % turrets.Length);
        }
    }

}
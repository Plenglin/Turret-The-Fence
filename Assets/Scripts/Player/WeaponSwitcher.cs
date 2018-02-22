using System;
using System.Collections;
using System.Collections.Generic;
using TurretTheFence.Utils;
using TurretTheFence.Weapons;
using UnityEngine;
using UnityEngine.UI;

namespace TurretTheFence.Player {

    public class WeaponSwitcher : MonoBehaviour {

        public WeaponMode weaponMode = new WeaponMode();
        public TurretMode turretMode = new TurretMode();
        private Mode[] modeCycle;
        private int cycleIndex = 0;
        private GameObject currentWeapon;
        private Text dataDisplay;

        public Mode currentMode {
            get { return modeCycle[cycleIndex]; }
        }

        private void Awake() {
            modeCycle = new Mode[] { weaponMode, turretMode };
            dataDisplay = GameObject.FindGameObjectWithTag("InventoryIndicator").GetComponent<Text>();
        }

        void Start() {
            foreach (Mode m in modeCycle) {
                m.OnStart();
                m.OnDisable();
            }
            currentMode.OnEnable();
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
            dataDisplay.text = currentMode.GetInventoryDisplay();
        }

    }

    public interface Mode {
        void OnStart();
        void OnEnable();
        void OnDisable();
        void OnSwitchIndex(int index);
        void OnScrollIndex(int direction);
        string GetInventoryDisplay();
    }

    [System.Serializable]
    public class WeaponMode : Mode {

        public List<WeaponData> weaponData = new List<WeaponData>();
        public int index = 0;

        public void OnStart() {
            OnDisable();
        }

        public void OnEnable() {
            SwitchTo(index);
        }

        public void OnDisable() {
            weaponData.ForEach(wd => wd.obj.SetActive(false));
        }

        public void OnSwitchIndex(int index) {
            if (index < weaponData.Capacity) {
                SwitchTo(index);
            }
        }

        private void SwitchTo(int index) {
            weaponData[this.index].obj.SetActive(false);
            this.index = index;
            weaponData[this.index].obj.SetActive(true);
        }
        
        public void OnScrollIndex(int direction) {
            int count = weaponData.Capacity;
            SwitchTo((index + direction + count) % count);
        }

        public string GetInventoryDisplay() {
            string output = "Weapons\n";
            for (int i = 0; i < weaponData.Capacity; i++) {
                WeaponData weap = weaponData[i];
                output += string.Format("{0}. {1}\n", i + 1, weap.name);
            }
            return output;
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
            SetIndex(0);
            OnDisable();
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

        public string GetInventoryDisplay() {
            string output = "Turrets\n";
            for (int i = 0; i < turrets.Length; i++) {
                TurretType turr = turrets[i];
                output += string.Format("{0}. {1} (${2})\n", i + 1, turr.name, turr.cost);
            }
            return output;
        }
    }

}
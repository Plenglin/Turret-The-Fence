using System.Collections;
using System.Collections.Generic;
using TurretTheFence.Utils;
using UnityEngine;

namespace TurretTheFence.Player {

    public class WeaponSwitcher : MonoBehaviour {

        public List<GameObject> guns = new List<GameObject>();
        private GameObject current;
        private int index = 0;

        void Start() {
            guns.ForEach(go => go.SetActive(false));
            if (guns.Count > 0) SwitchTo(0);
        }

        public void AddWeapon(GameObject weapon) {
            guns.Add(weapon);
        }

        public void SwitchTo(int index) {
            guns[this.index].SetActive(false);
            guns[index].SetActive(true);
            this.index = index;
        }

        void Update() {
            float scroll = Input.GetAxis("Mouse ScrollWheel");
            int count = guns.Count;
            if (scroll > 0) {
                SwitchTo((index + 1 + count) % count);
            } else if (scroll < 0) {
                SwitchTo((index + 1 + count) % count);
            }
            foreach (KeyValuePair<KeyCode, int> p in Constants.keyToNumber) {
                if (Input.GetKeyDown(p.Key) && p.Value <= count) {
                    SwitchTo(p.Value - 1);
                    break;
                }
            }
        }

    }
}
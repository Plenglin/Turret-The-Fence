using System.Collections;
using System.Collections.Generic;
using TurretTheFence.Player;
using UnityEngine;

namespace TurretTheFence.UI {
    public class ShopOpener : MonoBehaviour {

        private GameObject shop, player;

        private bool _showing;
        private bool showing {
            get {
                return _showing;
            }
            set {
                _showing = value;
                shop.SetActive(value);
                Cursor.visible = value;
                Cursor.lockState = value ? CursorLockMode.None : CursorLockMode.Locked;
                player.GetComponent<FirstPersonCamera>().enabled = !value;
                player.GetComponent<WeaponSwitcher>().weaponMode.firingEnabled = !value;
            }
        }

        private void Awake() {
            shop = GameObject.FindWithTag("Shop");
            player = GameObject.FindWithTag("Player");
        }

        private void Start() {
            showing = false;
        }

        void Update() {
            if (Input.GetKeyDown(KeyCode.B)) {
                showing = !showing;
            }
        }
    }
}
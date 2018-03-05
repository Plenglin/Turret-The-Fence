using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TurretTheFence.UI {
    public class ShopOpener : MonoBehaviour {

        private GameObject shop;

        private void Awake() {
            shop = GameObject.FindWithTag("Shop");
        }

        private void Start() {
            shop.SetActive(false);
        }

        void Update() {
            if (Input.GetKeyDown(KeyCode.B)) {
                Debug.Log("b");
                shop.SetActive(!shop.activeSelf);
            }
        }
    }
}
using CompleteProject;
using System;
using System.Collections;
using System.Collections.Generic;
using TurretTheFence.Player;
using UnityEditor;
using UnityEngine;


namespace TurretTheFence.UI {
    public class WeaponBuyListener : BuyListener {

        public GameObject weapon;

        public override void OnBuy(ShopPaneController pane) {
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            WeaponMode weaponmanager = player.GetComponent<WeaponSwitcher>().weaponMode;
            Instantiate(weapon, player.transform);
            weaponmanager.AddWeapon(weapon);
        }

        [MenuItem("Assets/Create/WeaponBuyListener")]
        public static void CreateAsset() {
            ScriptableObjectUtility.CreateAsset<WeaponBuyListener>();
        }
    }
}
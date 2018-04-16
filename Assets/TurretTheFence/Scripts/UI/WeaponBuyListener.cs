using CompleteProject;
using System;
using System.Collections;
using System.Collections.Generic;
using TurretTheFence.Player;
using TurretTheFence.Weapons;
using UnityEditor;
using UnityEngine;


namespace TurretTheFence.UI {
    public class WeaponBuyListener : BuyListener {

        public string weaponName;
        public GameObject weapon;

        public override void OnBuy(ShopPaneController pane) {
            GameObject player = GameObject.FindGameObjectWithTag("PlayerHead");
            WeaponMode weaponmanager = player.GetComponent<WeaponSwitcher>().weaponMode;
            GameObject instance = Instantiate(weapon, player.transform);
            weaponmanager.AddWeapon(new WeaponData(weaponName, instance));
        }

        [MenuItem("Assets/Create/WeaponBuyListener")]
        public static void CreateAsset() {
            ScriptableObjectUtility.CreateAsset<WeaponBuyListener>();
        }
    }
}
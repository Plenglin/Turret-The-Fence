using CompleteProject;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;


namespace TurretTheFence.UI {
    public class WeaponBuyListener : BuyListener {

        public GameObject weapon;

        public override void OnBuy() {
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            PlayerWeapons weaponmanager = player.GetComponent<PlayerWeapons>();
            Instantiate(weapon, player.transform);
            weaponmanager.AddWeapon(weapon);
        }

        [MenuItem("Assets/Create/WeaponBuyListener")]
        public static void CreateAsset() {
            ScriptableObjectUtility.CreateAsset<WeaponBuyListener>();
        }
    }
}
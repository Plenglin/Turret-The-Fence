using System;
using System.Collections;
using System.Collections.Generic;
using TurretTheFence.Player;
using TurretTheFence.Turret;
using TurretTheFence.UI;
using UnityEditor;
using UnityEngine;


namespace TurretTheFence.UI {
    public class TurretBuyScript : BuyListener {

        public int turretIndex;
        public float turretUpgradeRate;
        private TurretManager turretManager;
        private TurretMode turretMode;

        private void Awake() {
            turretMode = GameObject.FindWithTag("Player").GetComponent<WeaponSwitcher>().turretMode;
        }

        public override void OnBuy(ShopPaneController pane) {
            TurretType turr = turretManager.turrets[turretIndex];
            if (!turr.bought) {
                turr.bought = true;
                turretManager.RefreshTurretCache();
            } else {
                turr.upgrades++;
                pane.entry.price = turr.GetUpgradePrice();
            }
        }

        [MenuItem("Assets/Create/TurretBuyListener")]
        public static void CreateAsset() {
            ScriptableObjectUtility.CreateAsset<TurretBuyScript>();
        }

    }

    [System.Serializable]
    public class TurretBuyParameters {
        public int blueprint, initialUpgrade;
        public float increase;
    }
}
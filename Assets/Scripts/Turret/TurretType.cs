using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class TurretType {

    public string name;
    public int baseBuildCost, upgrades, baseUpgradeCost;
    public float buyIncrease = 1;
    public bool bought = false;
    public GameObject prefab;

    public int GetUpgradePrice() {
        return (int) (baseUpgradeCost * Mathf.Pow(buyIncrease, upgrades));
    }

}

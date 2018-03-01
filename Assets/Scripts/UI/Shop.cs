using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace TurretTheFence.UI {

    [System.Serializable]
    public class ShopItem {

        public bool repeatable;

        public string title;

        [TextArea(3, 10)]
        public string desc;

        [SerializeField]
        public BuyListener buyListener;
    }

    [System.Serializable]
    public class WeaponShopItem : ShopItem {

    }

    public abstract class BuyListener : ScriptableObject {
        public abstract void OnBuy();
    }

    public class Shop : MonoBehaviour {

        public List<ShopItem> shopItems;

        // Use this for initialization
        void Start() {

        }

        // Update is called once per frame
        void Update() {

        }
    }

}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace TurretTheFence.UI {
    public class ShopPaneController : MonoBehaviour {

        public ShopItem entry;

        private GameObject btnObj;
        private Text buttonText;
        private MoneyControl money;
        private Button btn;
        private bool bought;

        private void Awake() {
            btnObj = transform.Find("Button").gameObject;
            btn = btnObj.GetComponent<Button>();
            buttonText = transform.Find("Button").Find("Text").GetComponent<Text>();
            money = GameObject.FindGameObjectWithTag("Player").GetComponent<MoneyControl>();
        }

        public void UpdateText() {
            transform.Find("Title").gameObject.GetComponent<Text>().text = entry.title;
            transform.Find("Description").gameObject.GetComponent<Text>().text = entry.desc;
            buttonText.text = (bought && entry.repeatable) ? "Sold out!" : "Buy ($" + entry.price + ")";
        }

        public void OnBuyButtonClick() {
            money.money -= entry.price;
            entry.buyListener.OnBuy(this);
            bought = true;
        }

        // Use this for initialization
        void Start() {

        }

        // Update is called once per frame
        void Update() {
            btn.interactable = money.money >= entry.price && (entry.repeatable || !bought);
        }
    }
}
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace TurretTheFence.UI {

    [System.Serializable]
    public class ShopItem {

        public bool repeatable;
        public int price;
        public string title;

        [TextArea(3, 10)]
        public string desc;

        [SerializeField]
        public BuyListener buyListener;
    }

    public abstract class BuyListener : ScriptableObject {
        public abstract void OnBuy(ShopPaneController pane);
    }

    public class Shop : MonoBehaviour {

        public List<ShopItem> shopItems = new List<ShopItem>();
        public int columns = 2;
        public GameObject shopItemBase;
        public RectTransform contentPane;

        // Use this for initialization
        void Start() {
            for (int i=0; i < shopItems.Capacity; i++) {
                GameObject pane = Instantiate(shopItemBase, contentPane);
                ShopPaneController paneController = pane.GetComponent<ShopPaneController>();
                RectTransform transform = pane.GetComponent<RectTransform>();

                paneController.entry = shopItems[i];
                paneController.UpdateText();
                int row = i / columns;
                int col = i % columns;
                float x = Mathf.Abs(col * transform.rect.width);
                float y = -Mathf.Abs(row * transform.rect.height);
                transform.anchoredPosition = new Vector2(x, y);
            }
            float paneHeight = shopItemBase.GetComponent<RectTransform>().rect.height;
            contentPane.sizeDelta = new Vector2(contentPane.rect.width, (shopItems.Capacity / columns + 1) * paneHeight);
        }

        // Update is called once per frame
        void Update() {

        }

    }

}
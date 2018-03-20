using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoneyDrop : MonoBehaviour {

    public int money;
    public float decay = 5;

    private MoneyControl balance;
    private Collider targetCollider;
    private bool triggered = false;
    private StatisticTracker stats;

	// Use this for initialization
	void Start () {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        balance = player.GetComponent<MoneyControl>();
        targetCollider = player.GetComponent<Collider>();
        stats = player.GetComponent<StatisticTracker>();
        stats.OnMoneyDropped(money);
        Destroy(gameObject, decay);
    }

    private void OnTriggerEnter(Collider other) {
        if (!triggered && other == targetCollider) {
            balance.money += money;
            triggered = true;
            stats.OnMoneyCollected(money);
            Destroy(gameObject);
        }
    }
}

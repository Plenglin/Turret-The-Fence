using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoneyDrop : MonoBehaviour {

    public int money;
    public float decay = 5;

    private MoneyControl balance;
    private Collider targetCollider;
    private bool triggered = false;

	// Use this for initialization
	void Start () {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        Debug.Log(player);
        balance = player.GetComponent<MoneyControl>();
        targetCollider = player.GetComponent<Collider>();
        Destroy(gameObject, decay);
    }

    private void OnTriggerEnter(Collider other) {
        Debug.Log("tet");
        if (!triggered && other == targetCollider) {
            balance.money += money;
            triggered = true;
            Destroy(gameObject);
        }
    }
}

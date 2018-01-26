using CompleteProject;
using System.Collections;
using System.Collections.Generic;
using TurretTheFence.Utils;
using UnityEngine;

public class EnemyMeleeAttack : MonoBehaviour {

    public int damage;
    public float wait;

    private GameObject player;
    private PlayerHealth playerHealth;
    private PeriodicDelayer timer;
    private bool attacking = false;

	// Use this for initialization
	void Awake() {
        player = GameObject.FindGameObjectWithTag("Player");
        playerHealth = player.GetComponent<PlayerHealth>();
    }

    private void Start() {
        timer = new PeriodicDelayer(wait);
    }

    // Update is called once per frame
    void Update () {
        if (attacking) {
            timer.Try(() => playerHealth.TakeDamage(damage));
        }
	}

    private void OnTriggerEnter(Collider other) {
        if (other.gameObject == player) {
            attacking = true;
        }
    }

    private void OnTriggerExit(Collider other) {
        if (other.gameObject == player) {
            attacking = false;
        }
    }
}

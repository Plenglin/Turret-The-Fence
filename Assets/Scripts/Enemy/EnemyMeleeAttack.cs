using CompleteProject;
using System;
using System.Collections;
using System.Collections.Generic;
using TurretTheFence.Utils;
using UnityEngine;

public class EnemyMeleeAttack : MonoBehaviour {

    public int damage;
    public float preDamage, postDamage;

    public CollisionCounter damageRange, startAttackRange;

    private GameObject player;
    private PlayerHealth playerHealth;
    private bool attacking = false;
    private bool playerInRadius = false;

    // Use this for initialization
    private void Awake() {
        player = GameObject.FindGameObjectWithTag("Player");
        playerHealth = player.GetComponent<PlayerHealth>();
    }

    public void TryDoAttack(Action onFinished) {
        if (!attacking) {
            StartCoroutine(Attack(onFinished));
        }
    }

    public bool PlayerInRange() {
        return startAttackRange.inTrigger.Find(x => x.gameObject == player);
    }

    public bool PlayerCanBeDamaged() {
        return damageRange.inTrigger.Find(x => x.gameObject == player);
    }

    private IEnumerator Attack(Action onFinished) {
        attacking = true;
        yield return new WaitForSeconds(preDamage);
        if (PlayerCanBeDamaged()) {
            playerHealth.TakeDamage(damage);
        }
        yield return new WaitForSeconds(postDamage);
        attacking = false;
        onFinished.Invoke();
    }

}

using CompleteProject;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flamethrower : MonoBehaviour {

    public const float BURN_DELAY = 0.25f;

    public int damagePerHit;
    public float burnTime = 5f;

    public bool running = false;
    public ParticleSystem partycools;
    private float nextDamageTick = 0;
    private List<EnemyHealth> toAffect = new List<EnemyHealth>();

    // Update is called once per frame
    void Update() {
        running = Input.GetButton("Fire1");
        if (running) {
            partycools.Play();
            if (nextDamageTick <= Time.time) {
                nextDamageTick = Time.time + BURN_DELAY;
                Debug.Log("brunic");
                foreach (EnemyHealth health in toAffect) {
                    health.TakeDamage(damagePerHit, health.gameObject.transform.position);
                    health.burning = burnTime;
                }
            }
        }
    }

    private void OnTriggerEnter(Collider other) {
        EnemyHealth health = other.gameObject.GetComponent<EnemyHealth>();
        if (health != null) {
            toAffect.Add(health);
        }
    }

    private void OnTriggerExit(Collider other) {
        EnemyHealth health = other.gameObject.GetComponent<EnemyHealth>();
        if (health != null) {
            toAffect.Remove(health);
        }
    }

}

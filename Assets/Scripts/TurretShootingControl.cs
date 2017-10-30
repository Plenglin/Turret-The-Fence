using CompleteProject;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretShootingControl : MonoBehaviour {

    public float firingDelay;
    public int damagePerShot;
    public LineRenderer tracer;

    private TurretDirectionControl targeter;
    private TurretTargetingControl tar;
    private int shootableMask;


    // Use this for initialization
    void Start () {
		targeter = GetComponent<TurretDirectionControl>();
        shootableMask = LayerMask.GetMask("Shootable");
	}
	
	// Update is called once per frame
	void Update () {
		if (targeter.targetObject != null) {
            Fire();
        }
	}

    private float lastFiring;
    
    void AttemptToFire() {
        float currentTime = Time.time;
        if (lastFiring + firingDelay >= currentTime) {
            Fire();
            lastFiring = currentTime;
        }
    }

    void Fire() {
        Debug.Log("Turret FIRING");
        Vector3 offset = targeter.targetObject.transform.position - transform.position;
        // Try and find an EnemyHealth script on the gameobject hit.
        GameObject enemy = targeter.targetObject;
        EnemyHealth enemyHealth = enemy.GetComponent<EnemyHealth>();

        // If the EnemyHealth component exist...
        if (enemyHealth != null) {
            // ... the enemy should take damage.
            enemyHealth.TakeDamage(damagePerShot, enemy.transform.position);
        }
       
    }
}

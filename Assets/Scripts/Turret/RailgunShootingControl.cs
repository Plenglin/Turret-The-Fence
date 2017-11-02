using CompleteProject;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RailgunShootingControl : MonoBehaviour {

    public float firingDelay;
    public float lineDuration;
    public int damagePerShot;
    public GameObject tracerObject;

    private TurretTargetingControl targetControl;
    private int shootableMask;
    private LineRenderer tracer;
    private float lastFiring;

    // Use this for initialization
    void Start () {
		targetControl = GetComponent<TurretTargetingControl>();
        tracer = tracerObject.GetComponent<LineRenderer>();
        shootableMask = LayerMask.GetMask("Shootable");
        lastFiring = -firingDelay;
	}
	
	// Update is called once per frame
	void Update () {
        float currentTime = Time.time;
        if (targetControl.target != null && lastFiring + firingDelay <= currentTime) {
            Fire();
            lastFiring = currentTime;
        }
        tracer.SetPosition(0, tracerObject.transform.position);
        tracer.enabled = (lastFiring + lineDuration >= currentTime);
    }

    void Fire() {
        GameObject enemy = targetControl.target;

        if (enemy != null) {
            Vector3 offset = enemy.transform.position - transform.position;
            // Try and find an EnemyHealth script on the gameobject hit.
            EnemyHealth enemyHealth = enemy.GetComponent<EnemyHealth>();

            // If the EnemyHealth component exist...
            if (enemyHealth != null) {
                // ... the enemy should take damage.
                enemyHealth.TakeDamage(damagePerShot, enemy.transform.position);
                tracer.SetPosition(1, enemy.transform.position + targetControl.pointingOffset);
            }
        }
       
    }
}

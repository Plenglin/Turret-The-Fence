using CompleteProject;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretTargetingControl : MonoBehaviour {

    /**
     * The furthest we can shoot. Use a negative value for infinite distance.
     */
    public float maxDistance;
    public GameObject target;
    public Vector3 pointingOffset;
    public bool ignoreObstacles = true;

    private const string TARGET_TAG = "TurretTargets";
    private TurretDirectionControl directionControl;

	// Use this for initialization
	void Start () {
        directionControl = GetComponent<TurretDirectionControl>();
	}
	
	// Update is called once per frame
	void Update () {
        target = null;
        float closestDist = maxDistance >= 0 ? maxDistance * maxDistance : float.MaxValue;
        GameObject[] possibleTargets = GameObject.FindGameObjectsWithTag(TARGET_TAG);
        foreach (GameObject obj in possibleTargets) {
            Vector3 offset = obj.transform.position - transform.position;
            float dist = offset.sqrMagnitude;
            if (dist < closestDist && !obj.GetComponent<EnemyHealth>().isDead) {
                RaycastHit raycast;
                if (ignoreObstacles) {
                    target = obj;
                    closestDist = dist;
                } else if (Physics.Raycast(transform.position, offset, out raycast, maxDistance, LayerMask.GetMask("Shootable", "BulletObstacle"))) { // raycast, including targets and obstacles
                    Debug.Log("Hit a " + raycast.collider.gameObject.name);
                    if (((1 << raycast.collider.gameObject.layer) & LayerMask.GetMask("Shootable")) > 0) {  // did we hit a target and not an obstacle?
                        target = obj;
                        closestDist = dist;
                    }
                }
            }
        }
        if (target != null) {
            directionControl.targetPosition = target.transform.position + pointingOffset;
        }
	}
}

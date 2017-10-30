using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretTargetingControl : MonoBehaviour {

    /**
     * The furthest we can shoot. Use a negative value for infinite distance.
     */
    public float maxDistance;

    private const string TARGET_TAG = "TurretTargets";
    private TurretDirectionControl targeter;

	// Use this for initialization
	void Start () {
        targeter = GetComponent<TurretDirectionControl>();
        Debug.Log(targeter);
	}
	
	// Update is called once per frame
	void Update () {
        GameObject target = null;
        float closestDist = maxDistance >= 0 ? maxDistance * maxDistance : float.MaxValue;
        GameObject[] possibleTargets = GameObject.FindGameObjectsWithTag(TARGET_TAG);
        foreach (GameObject obj in possibleTargets) {
            float dist = (obj.transform.position - transform.position).sqrMagnitude;
            if (dist < closestDist) {
                target = obj;
                closestDist = dist;
            }
        }
        targeter.targetObject = target;
        Debug.Log(possibleTargets.Length);
	}
}

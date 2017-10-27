using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretTargetingControl : MonoBehaviour {

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
        float closestDist = float.MaxValue;
        foreach (GameObject obj in GameObject.FindGameObjectsWithTag(TARGET_TAG)) {
            float dist = (obj.transform.position - transform.position).sqrMagnitude;
            if (dist < closestDist) {
                target = obj;
                closestDist = dist;
            }
        }

        targeter.targetObject = target;
	}
}

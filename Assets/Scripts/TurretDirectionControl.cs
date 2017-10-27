using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretDirectionControl : MonoBehaviour {

    public GameObject center;
    public GameObject yawObject;
    public GameObject pitchObject;
    public GameObject targetObject;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (targetObject != null) {
            Vector3 offset = targetObject.transform.position - center.transform.position;
            Vector3 angles = Quaternion.LookRotation(offset).eulerAngles;
            pitchObject.transform.localRotation = Quaternion.Euler(angles.x, 0, 0);
            yawObject.transform.localRotation = Quaternion.Euler(0, angles.y + 90, 0);
        }
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretDirectionControl : MonoBehaviour {

    public GameObject center;
    public GameObject yawObject;
    public GameObject pitchObject;
    public Vector3 targetPosition;

    public float yawOffset = 0;
    public float pitchOffset = 0;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (targetPosition != null) {
            Vector3 offset = targetPosition - center.transform.position;
            Vector3 angles = Quaternion.LookRotation(offset).eulerAngles;
            pitchObject.transform.localRotation = Quaternion.Euler(0, 0, angles.x + pitchOffset);
            yawObject.transform.localRotation = Quaternion.Euler(0, angles.y + yawOffset, 0);
        }
	}
}

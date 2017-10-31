using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RadialCameraFollow: MonoBehaviour {

    public GameObject follow;
    public float distance;
    public Transform center;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        transform.position = follow.transform.position + distance * (center.position - follow.transform.position).normalized;
        transform.position.Set(transform.position.x, center.position.y, transform.position.z);
        transform.LookAt(follow.transform);
	}
}

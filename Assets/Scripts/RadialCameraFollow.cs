using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RadialCameraFollow: MonoBehaviour {

    public GameObject follow;
    public float distance;
    public Vector3 center;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        this.transform.position = follow.transform.position + distance * (center - follow.transform.position);
        this.transform.LookAt(follow.transform);
	}
}

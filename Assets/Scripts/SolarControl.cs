using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SolarControl : MonoBehaviour { 

    public Vector3 offset;

    public GameObject track;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
        Quaternion rotation = track.transform.rotation;
        rotation *= Quaternion.Euler(offset);
        this.transform.rotation = rotation;
    }

}

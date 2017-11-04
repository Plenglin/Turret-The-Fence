using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AOEManager : MonoBehaviour {

    public List<Collider> touching;

	// Use this for initialization
	void Start () {
        touching = new List<Collider>();
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnTriggerEnter(Collider other) {
        touching.Add(other);
    }

    void OnTriggerExit(Collider other) {
        touching.Remove(other);
    }

}

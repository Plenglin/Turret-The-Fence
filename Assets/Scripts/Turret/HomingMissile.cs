﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CompleteProject;

public class HomingMissile : MonoBehaviour {

    public GameObject target;

    public float speed = 50;  // Units/sec
    public float turning = 30;  // Degrees/sec
    public int damage = 30;

	// Use this for initialization
	void Start () {
        //velocity = transform.forward.normalized * (speed);
	}
	
	// Update is called once per frame
	void Update () {
        float maxTurn = turning * speed;
        /*
        Vector3 offset = target.transform.position - thisCollider.transform.position;
        Quaternion change = Quaternion.FromToRotation(this.transform.forward, offset);
        Quaternion toRotate = Quaternion.Lerp(Quaternion.identity, change, 0.01f);
        transform.rotation *= toRotate;
        transform.position += transform.forward.normalized * speed;*/
        //find the vector pointing from our position to the target
        Vector3 offset = (target.transform.position - transform.position).normalized;

        //create the rotation we need to be in to look at the target
        Quaternion look = Quaternion.LookRotation(offset);

        //rotate us over time according to speed until we are in the required rotation
        transform.rotation = Quaternion.Slerp(transform.rotation, look, Time.deltaTime * turning);
        transform.position += transform.forward * Time.deltaTime * speed;
        
    }

    void OnTriggerEnter(Collider other) {
        Debug.Log(other);
        if (other.gameObject == target) {
            Debug.Log("Rocket hit object");
            other.gameObject.GetComponent<EnemyHealth>().TakeDamage(damage, other.ClosestPoint(transform.position));
            Destroy(gameObject);
        }
    }
}
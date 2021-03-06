﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CompleteProject;

public class HomingMissile : MonoBehaviour {

    public Vector3 target;
    public GameObject model;
    public AOEManager explosion;
    public ParticleSystem explosionParticles;

    public float speed = 50;  // Units/sec
    public float turning = 30;  // Degrees/sec
    public float timeUntilExplosion = 5;  // Explode if you haven't already by this time
    public float minExplodeDistance = 10;  // Explode if you are this close
    public int directHitDamage;  // Not including the AOE
    public int splashDamage;

    private float creation;
    private bool stopped = false;

    // Use this for initialization
    void Start () {
        creation = Time.time;
	}
	
	// Update is called once per frame
	void Update () {
        if (stopped) {
            model.SetActive(false);
            return;
        }
        if (Time.time >= creation + timeUntilExplosion) {
            Explode();
        }

        float maxTurn = turning * speed;
        /*
        Vector3 offset = target.transform.position - thisCollider.transform.position;
        Quaternion change = Quaternion.FromToRotation(this.transform.forward, offset);
        Quaternion toRotate = Quaternion.Lerp(Quaternion.identity, change, 0.01f);
        transform.rotation *= toRotate;
        transform.position += transform.forward.normalized * speed;*/
        //find the vector pointing from our position to the target
        Vector3 offset = (target - transform.position);

        if (offset.magnitude <= minExplodeDistance) {
            Explode();
            return;
        }
        //create the rotation we need to be in to look at the target
        Quaternion look = Quaternion.LookRotation(offset);

        //rotate us over time according to speed until we are in the required rotation
        transform.rotation = Quaternion.Slerp(transform.rotation, look, Time.deltaTime * turning);
        transform.position += transform.forward * Time.deltaTime * speed;


    }

    void OnTriggerEnter(Collider other) {
        Debug.Log(other);
        if (other.gameObject.tag == "TurretTargets") {
            Explode();
        }
    }

    void Explode() {
        explosionParticles.Stop();
        explosionParticles.Play();
        foreach (Collider collider in explosion.touching) {
            if (collider != null) {
                EnemyHealth hp = collider.gameObject.GetComponent<EnemyHealth>();
                if (hp != null) {
                    hp.TakeDamage(splashDamage, collider.ClosestPoint(transform.position));
                }
            }
        }
        stopped = true;
        Destroy(gameObject, 1);
    }

}

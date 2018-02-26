using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissileShootingControl : MonoBehaviour {

    public Transform[] missileOrigins;
    public GameObject missile;

    public float missileSpread;

    public float salvoDelay;
    public float reloadingTime;

    private TurretTargetingControl targetControl;
    private TurretDirectionControl directionControl;
    private float lastFire;

    // Use this for initialization
    void Start () {
        lastFire = -reloadingTime;
        targetControl = GetComponent<TurretTargetingControl>();
        directionControl = GetComponent<TurretDirectionControl>();
    }

    // Update is called once per frame
    void Update () {
        if (Time.time >= lastFire + reloadingTime && targetControl.target != null) {
            Debug.Log("starting salvo");
            StartCoroutine("Fire", 0);
            lastFire = Time.time;
        }
	}

    IEnumerator Fire() {
        targetControl.enabled = false;
        directionControl.enabled = false;
        Vector3 target = targetControl.target.transform.position;
        foreach (Transform origin in missileOrigins) {
            Debug.Log("firing");
            GameObject miss = Instantiate(this.missile);
            miss.transform.position = origin.position;
            miss.transform.rotation = origin.rotation * Quaternion.Euler(
                Random.Range(-missileSpread, missileSpread),
                Random.Range(-missileSpread, missileSpread),
                Random.Range(-missileSpread, missileSpread)
            );
            HomingMissile missile = miss.GetComponent<HomingMissile>();
            missile.target = target;
            yield return new WaitForSeconds(salvoDelay);
        }
        targetControl.enabled = true;
        directionControl.enabled = true;
    }

}

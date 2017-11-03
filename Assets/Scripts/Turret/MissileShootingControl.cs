using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissileShootingControl : MonoBehaviour {

    public Transform[] missileOrigins;
    public GameObject missile;

    public float firingDelay;
    public float reloadingTime;

    private TurretTargetingControl targetControl;
    private TurretDirectionControl directionControl;
    private float lastFire = 0;

    // Use this for initialization
    void Start () {
        targetControl = GetComponent<TurretTargetingControl>();
        directionControl = GetComponent<TurretDirectionControl>();
    }

    // Update is called once per frame
    void Update () {
        if (lastFire + reloadingTime <= Time.time && targetControl.target != null) {
            Fire();
            lastFire = Time.time;
        }
	}

    void Fire() {
        foreach (Transform t in missileOrigins) {
            GameObject m = Instantiate(missile);
            HomingMissile tar = m.GetComponent<HomingMissile>();
            m.transform.position = t.position;
            m.transform.rotation = t.rotation;
            tar.target = targetControl.target;
        }
    }

}

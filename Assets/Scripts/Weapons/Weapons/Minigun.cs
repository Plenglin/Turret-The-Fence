using CompleteProject;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Minigun : MonoBehaviour {

    public float heatPerShot, cooldownTime;

    public int damagePerShot;
    public float firingDelay, accuracy, spinUpTime, spinDownTime, tracerDuration;
    public Transform muzzleEnd;

    public float heat = 0;
    public Light muzzleFlash, overheat;
    public LineRenderer tracer;
    public Text ammoIndicator;
    public GameObject spinner;
    public int barrels = 6;

    private float maxSpinRate, currentSpinRate, spinUpRate, spinDownRate, cooldownRate, nextFire = 0, heatLightIntensity;
    private bool spinning = true, overheated = false;

    private RelativePlayerMovement playerMovement;
    private int shootables;
    
    // Use this for initialization
    void Start () {
        shootables = LayerMask.GetMask("Shootable", "BulletObstacle");
        maxSpinRate = 360 / barrels / firingDelay;
        spinUpRate = maxSpinRate / spinUpTime;
        spinDownRate = maxSpinRate / spinDownTime;
        cooldownRate = 1 / cooldownTime;
        heatLightIntensity = overheat.intensity;

        playerMovement = GameObject.FindGameObjectWithTag("Player").GetComponent<RelativePlayerMovement>();
    }

    // Update is called once per frame
    void Update () {
        string output = String.Format("Heat: {0}%\nMinigun", (int) (heat * 100));
        bool firing = Input.GetButton("Fire1");
        spinning = firing || Input.GetButton("Fire2");
        if (spinning) {
            currentSpinRate += spinUpRate * Time.deltaTime;
            playerMovement.hindered = true;
        } else {
            currentSpinRate = Math.Max(currentSpinRate - spinDownRate * Time.deltaTime, 0);
            playerMovement.hindered = false;
        }
        if (currentSpinRate > maxSpinRate) {
            currentSpinRate = maxSpinRate;
            if (firing && !overheated && Time.time >= nextFire) {
                heat += heatPerShot;
                nextFire = Time.time + firingDelay;
                StartCoroutine(Fire());
            }
        }
        if (overheated || !firing) {
            heat -= cooldownRate * Time.deltaTime;
        }
        if (overheated) {
            output = "OVERHEATED!\n" + output;
        }
        spinner.transform.Rotate(0, 0, currentSpinRate * Time.deltaTime);
        overheat.intensity = heatLightIntensity * heat;
        if (heat >= 1) {
            heat = 1;
            overheated = true;
        } else if (heat <= 0) {
            heat = 0;
            overheated = false;
        }
        ammoIndicator.text = output;
	}

    IEnumerator Fire() {
        RaycastHit hit;
        // First rotate some degrees up, then rotate around forward
        //Vector3 direction = Quaternion.AngleAxis(Random.Range(0, 360), muzzleEnd.forward) * (Quaternion.AngleAxis(Random.Range(0, accuracy), muzzleEnd.right) * muzzleEnd.forward);
        Vector3 direction = (Quaternion.AngleAxis(UnityEngine.Random.Range(-accuracy, accuracy), muzzleEnd.up) * muzzleEnd.forward);
        Ray ray = new Ray(muzzleEnd.position, direction);
        if (Physics.Raycast(ray, out hit, 1000f, shootables)) {
            EnemyHealth health = hit.collider.gameObject.GetComponent<EnemyHealth>();
            tracer.SetPosition(0, muzzleEnd.position);
            tracer.SetPosition(1, hit.point);
            if (health != null) {
                health.TakeDamage(damagePerShot, hit.point);
            }
        }

        muzzleFlash.enabled = true;
        tracer.enabled = true;
        yield return new WaitForSeconds(tracerDuration);
        muzzleFlash.enabled = false;
        tracer.enabled = false;
    }
    
}

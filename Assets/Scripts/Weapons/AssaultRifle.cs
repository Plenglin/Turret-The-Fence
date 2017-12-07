using CompleteProject;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AssaultRifle : MonoBehaviour {

    public int damagePerShot;
    public float firingDelay;
    public float accuracy;
    public float reloadTime;
    public Transform muzzleEnd;

    public int clipSize;
    public int bullets;

    public bool firingEnabled = true;

    public float tracerDuration;

    public Light muzzleFlash;
    public LineRenderer tracer;
    public Text ammoIndicator;

    private int shootables;
    private float lastFired = 0;
    private bool reloading = false;

	// Use this for initialization
	void Start () {
        shootables = LayerMask.GetMask("Shootable", "BulletObstacle");
        bullets = clipSize;
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown("r") && bullets < clipSize) {
            StartCoroutine(Reload());
        }
        if (firingEnabled && bullets > 0 && Input.GetButton("Fire1") && Time.time >= lastFired + firingDelay) {
            StartCoroutine(Fire());
            lastFired = Time.time;
            bullets--;
            if (bullets <= 0) {
                StartCoroutine(Reload());
            }
        }
        string ammo;
        if (reloading) {
            ammo = "RELOADING";
        } else {
            ammo = string.Format("{0}/{1}", bullets, clipSize);
        }
        ammoIndicator.text = ammo + "\nAssault Rifle";
    }

    IEnumerator Fire() {
        RaycastHit hit;
        // First rotate some degrees up, then rotate around forward
        //Vector3 direction = Quaternion.AngleAxis(Random.Range(0, 360), muzzleEnd.forward) * (Quaternion.AngleAxis(Random.Range(0, accuracy), muzzleEnd.right) * muzzleEnd.forward);
        Vector3 direction = (Quaternion.AngleAxis(Random.Range(-accuracy, accuracy), muzzleEnd.up) * muzzleEnd.forward);
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

    IEnumerator Reload() {
        firingEnabled = false;
        reloading = true;
        yield return new WaitForSeconds(reloadTime);
        bullets = clipSize;
        firingEnabled = true;
        reloading = false;
    }

}

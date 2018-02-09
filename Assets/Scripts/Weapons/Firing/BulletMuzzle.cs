using CompleteProject;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace TurretTheFence.Weapons.Firing {

    public class BulletMuzzle : MonoBehaviour, IFiringManager {

        public float accuracy, tracerDuration;
        public int damagePerShot;
        public Light muzzleFlash;

        private LineRenderer tracer;

        private int shootables;
        private Transform player;

        private void Awake() {
            player = GameObject.FindGameObjectWithTag("Player").transform;
            tracer = GetComponent<LineRenderer>();
            shootables = LayerMask.GetMask("Shootable", "BulletObstacle");
        }

        private void Update() {
            tracer.SetPosition(0, transform.position);
        }

        private IEnumerator FinishFire() {
            muzzleFlash.enabled = true;
            tracer.enabled = true;
            yield return new WaitForSeconds(tracerDuration);
            muzzleFlash.enabled = false;
            tracer.enabled = false;
        }

        public bool OnFire() {
            RaycastHit hit;
            // First rotate some degrees up, then rotate around forward
            float theta = UnityEngine.Random.Range(0, 360);
            float rad = UnityEngine.Random.Range(0, accuracy);
            Vector3 direction = Quaternion.AngleAxis(theta, transform.forward) * (Quaternion.AngleAxis(rad, transform.right) * player.forward);

            // Rotate a random amount sideways
            //Vector3 direction = (Quaternion.AngleAxis(UnityEngine.Random.Range(-accuracy, accuracy), transform.up) * transform.forward);
            Ray ray = new Ray(player.position, direction);
            if (Physics.Raycast(ray, out hit, 100f, shootables)) {
                EnemyHealth health = hit.collider.gameObject.GetComponent<EnemyHealth>();
                tracer.SetPosition(1, hit.point);
                if (health != null) {
                    health.TakeDamage(damagePerShot, hit.point);
                }
                StartCoroutine(FinishFire());
                return true;
            }
            return false;
        }
    }

}

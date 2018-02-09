using CompleteProject;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TurretTheFence.Effect;
using System;
using TurretTheFence.Utils;

namespace TurretTheFence.Weapons.Firing {

    public class FlamethrowerMuzzle : MonoBehaviour, IFiringManager {
        public int damagePerHit, damagePerDecisecond;
        public float burnTime = 5f;
        public float minRange, maxRange;

        public GameObject burningEffect;

        private ParticleSystem partycools;
        private float nextDamageTick = 0;
        private List<GameObject> toAffect = new List<GameObject>();
        private GameObject player;
        private Vector3 lastPlayerPos;
        private SmoothedAverage sizeBuffer;
        private CapsuleCollider areaOfEffect;

        private void Awake() {
            player = GameObject.FindGameObjectWithTag("Player");
            sizeBuffer = new SmoothedAverage(20, (maxRange + minRange) / 2);
            lastPlayerPos = player.transform.position;
            areaOfEffect = GetComponent<CapsuleCollider>();
            partycools = GetComponent<ParticleSystem>();
        }

        public bool OnFire() {
            // Vector projection of velocity onto forward, for doppler effect
            Vector3 playerPos = player.transform.position;
            Vector3 displacement = playerPos - lastPlayerPos;
            float projection = Vector3.Dot(displacement.normalized, player.transform.forward);
            lastPlayerPos = playerPos;
            float length = Mathf.Lerp(minRange, maxRange, (projection + 1) / 2);
            sizeBuffer.push(length);

            UpdateCollider(sizeBuffer.avg);

            partycools.Play();
            foreach (GameObject enemy in toAffect) {
                EnemyHealth health = enemy.GetComponent<EnemyHealth>();
                StatusEffectManager effects = enemy.GetComponent<StatusEffectManager>();
                health.TakeDamage(damagePerHit, health.gameObject.transform.position);
                effects.Apply(new BurningEffect(damagePerDecisecond, burnTime, burningEffect));
            }
            return true;
        }

        private void UpdateCollider(float size) {
            areaOfEffect.height = size;
            areaOfEffect.center = new Vector3(0, 0, size / 2 + 1);
        }

        private void OnTriggerEnter(Collider other) {
            EnemyHealth health = other.gameObject.GetComponent<EnemyHealth>();
            if (health != null) {
                toAffect.Add(other.gameObject);
            }
        }

        private void OnTriggerExit(Collider other) {
            toAffect.Remove(other.gameObject);
        }
    }

}
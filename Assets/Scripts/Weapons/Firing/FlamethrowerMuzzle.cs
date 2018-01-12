using CompleteProject;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utils;
using System;

namespace TurretTheFence.Weapons.Firing {

    public class FlamethrowerMuzzle : MonoBehaviour, IFiringManager {
        public const float BURN_DELAY = 0.25f;

        public int damagePerHit;
        public float burnTime = 5f;
        public float maxRange;
        public float minRange;

        public ParticleSystem partycools;
        private float nextDamageTick = 0;
        private List<EnemyHealth> toAffect = new List<EnemyHealth>();
        private GameObject player;
        private Vector3 lastPlayerPos;
        private SmoothedAverage sizeBuffer;
        private CapsuleCollider areaOfEffect;

        private void Start() {

        }

        private void Update() {

        }

        public void OnFire() {
            throw new NotImplementedException();
        }

        private void UpdateCollider(float size) {
            areaOfEffect.height = size;
            areaOfEffect.center = new Vector3(0, 0, size / 2 + 1);
        }

        private void OnTriggerEnter(Collider other) {
            EnemyHealth health = other.gameObject.GetComponent<EnemyHealth>();
            if (health != null) {
                toAffect.Add(health);
            }
        }

        private void OnTriggerExit(Collider other) {
            EnemyHealth health = other.gameObject.GetComponent<EnemyHealth>();
            if (health != null) {
                toAffect.Remove(health);
            }
        }
    }

}
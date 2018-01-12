using CompleteProject;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utils;

public class FlamethrowerMuzzle : MonoBehaviour {

    public const float BURN_DELAY = 0.25f;

    public int damagePerHit;
    public float burnTime = 5f;
    public float maxRange;
    public float minRange;

    public bool running = false;
    public ParticleSystem partycools;
    private float nextDamageTick = 0;
    private List<EnemyHealth> toAffect = new List<EnemyHealth>();
    private GameObject player;
    private Vector3 lastPlayerPos;
    private SmoothedAverage sizeBuffer;
    private CapsuleCollider areaOfEffect;

    private void Start() {
        player = GameObject.FindGameObjectWithTag("Player");
        sizeBuffer = new SmoothedAverage(20, (maxRange + minRange) / 2);
        lastPlayerPos = player.transform.position;
        areaOfEffect = GetComponent<CapsuleCollider>();
    }

    // Update is called once per frame
    void Update() {
        running = Input.GetButton("Fire1");
        if (running) {

            Vector3 playerPos = player.transform.position;
            Vector3 displacement = playerPos - lastPlayerPos;
            float projection = Vector3.Dot(displacement.normalized, player.transform.forward);
            lastPlayerPos = playerPos;
            float length = Mathf.Lerp(minRange, maxRange, (projection + 1) / 2);
            sizeBuffer.push(length);

            UpdateCollider(sizeBuffer.avg);

            partycools.Play();
            if (nextDamageTick <= Time.time) {
                nextDamageTick = Time.time + BURN_DELAY;
                foreach (EnemyHealth health in toAffect) {
                    health.TakeDamage(damagePerHit, health.gameObject.transform.position);
                    health.burning = burnTime;
                }
            }
        }
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

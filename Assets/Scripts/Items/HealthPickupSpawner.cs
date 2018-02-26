using CompleteProject;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace TurretTheFence.Items {

    public class HealthPickupSpawner : MonoBehaviour {

        public int health;
        public RegeneratingHealthPickup pack;
        public float respawnTime;

        private GameObject player;

        private void Awake() {
            player = GameObject.FindGameObjectWithTag("Player");
        }

        // Use this for initialization
        void Start() {
            pack.gameObject.SetActive(true);
        }

        // Update is called once per frame
        void Update() {

        }

        private void OnTriggerEnter(Collider other) {
            Debug.Log("adsf");
            if (other.gameObject == player) {
                if (player.GetComponent<PlayerHealth>().Heal(health)) {
                    StartCoroutine(DisabledPickup());
                }
            }
        }

        IEnumerator DisabledPickup() {
            pack.gameObject.SetActive(false);
            yield return new WaitForSeconds(respawnTime);
            pack.gameObject.SetActive(true);
        }

    }

}
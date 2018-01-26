using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace TurretTheFence.Enemy {

    public class EnemyMovement : MonoBehaviour {

        NavMeshAgent nav;
        GameObject player;

        // Use this for initialization
        private void Awake() {
            nav = GetComponent<NavMeshAgent>();
            player = GameObject.FindGameObjectWithTag("Player");
        }

        // Update is called once per frame
        private void Update() {
            nav.SetDestination(player.transform.position);
        }
    }

}
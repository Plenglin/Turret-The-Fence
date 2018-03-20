using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace TurretTheFence.Enemy {

    public class SimpleMeleeEnemyController : MonoBehaviour {

        public SimpleMeleeEnemyState state;
        public EnemyMeleeAttack attack;
        public Animator anim;
        private NavMeshAgent nav;
        private GameObject player;

        private void Awake() {
            nav = GetComponent<NavMeshAgent>();
            player = GameObject.FindGameObjectWithTag("Player");
        }

        // Use this for initialization
        void Start() {
            state = SimpleMeleeEnemyState.FOLLOWING_PLAYER;
        }

        // Update is called once per frame
        void Update() {
            switch (state) {
                case SimpleMeleeEnemyState.FOLLOWING_PLAYER:
                    nav.SetDestination(player.transform.position);
                    anim.SetBool("Walking", true);
                    if (attack.PlayerInRange()) {
                        state = SimpleMeleeEnemyState.ATTACKING;
                        anim.SetBool("Walking", false);
                        nav.SetDestination(transform.position);
                        anim.SetTrigger("Attack");
                        attack.TryDoAttack(() => {
                            state = SimpleMeleeEnemyState.FOLLOWING_PLAYER;
                            anim.ResetTrigger("Attack");
                        });
                    }
                    break;
            }
        }
    }

    public enum SimpleMeleeEnemyState {
        FOLLOWING_PLAYER, ATTACKING
    }

}
using System.Collections;
using System.Collections.Generic;
using TurretTheFence.Utils;
using UnityEngine;

namespace TurretTheFence.Enemy {

	public class EnemyHealth : MonoBehaviour {
         
		public int maxHealth, health, money;

        public WeightedLootTable lootTable = new WeightedLootTable();

        private void Awake() {
            lootTable.GenerateCumulativeTable();
        }

        private void Start() {
            health = maxHealth;
        }

        public void Damage(int damage) {
            health -= damage;

            if (health <= 0) {
                OnDeath();
            }
        }

        private void OnDeath() {
            Destroy(this, 2f);
            GameObject drop = Instantiate(lootTable.Get());
            drop.transform.position = this.transform.position;
        }

    }

}
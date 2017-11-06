using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EnemySpawning {

    [System.Serializable]
    public class Wave {

        public SpawnDescription[] spawns;

    }

    [System.Serializable]
    public class SpawnDescription {

        public GameObject enemy;
        public int bursts = 1;
        public float timeBetweenBursts = 1;
        public int burstsOf = 1;
        public float timeBetweenSpawns = 0.5f;

        public IEnumerator DoSpawnAt(Vector3 position) {
            for (int i = 0; i < bursts; i++) {
                for (int j = 0; j < burstsOf; j++) {
                    MonoBehaviour.Instantiate(enemy);
                    enemy.transform.position = position;
                    yield return new WaitForSeconds(timeBetweenSpawns);
                }
                yield return new WaitForSeconds(timeBetweenBursts - timeBetweenSpawns);
            }
        }

    }
}
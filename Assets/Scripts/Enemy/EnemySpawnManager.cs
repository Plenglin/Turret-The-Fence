using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EnemySpawning {

    /// <summary>
    /// Manages actually spawning the enemies.
    /// </summary>
    public class EnemySpawnManager : MonoBehaviour {

        public Transform[] spawnAreas;
        public Wave wave;

        private const float waitOnCoroutines = 0.25f;
        private int completed;

        public IEnumerator DoWave() {
            completed = 0;
            foreach (SpawnDescription sp in wave.spawns) {
                StartCoroutine(DoSpawnDescription(sp));
            }
            yield return new WaitUntil(() => completed == wave.spawns.Length);
        }

        public IEnumerator DoSpawnDescription(SpawnDescription sp) {
            for (int i = 0; i < sp.bursts; i++) {
                Vector3 spawnArea = getRandomSpawnArea();
                for (int j = 0; j < sp.burstsOf; j++) {
                    GameObject enemy = MonoBehaviour.Instantiate(sp.enemy);
                    enemy.transform.position = spawnArea;
                    yield return new WaitForSeconds(sp.timeBetweenSpawns);
                }
                yield return new WaitForSeconds(sp.timeBetweenBursts - sp.timeBetweenSpawns);
            }
            completed++;
        }

        public Vector3 getRandomSpawnArea() {
            return spawnAreas[Random.Range(0, spawnAreas.Length)].position;
        }

    }
}
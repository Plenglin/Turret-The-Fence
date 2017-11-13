using CompleteProject;
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
        public List<GameObject> enemies;

        private StatisticTracker stats;

        private const float waitOnCoroutines = 0.25f;
        private int completed;

        private void Start() {
            stats = GameObject.FindGameObjectWithTag("Player").GetComponent<StatisticTracker>();
        }

        public IEnumerator DoWave() {
            enemies = new List<GameObject>();
            completed = 0;
            stats.OnWaveBegin();
            foreach (SpawnDescription sp in wave.spawns) {
                StartCoroutine(DoSpawnDescription(sp));
            }
            yield return new WaitUntil(() => (completed == wave.spawns.Count));
            stats.OnWaveEnd();
            yield return new WaitUntil(() => {
                return enemies.TrueForAll((obj) => obj == null);
            });  // Wait until all enemies are dead
            stats.OnLastEnemyKilled();
        }

        public IEnumerator DoSpawnDescription(SpawnDescription sp) {
            for (int i = 0; i < sp.bursts; i++) {
                Vector3 spawnArea = getRandomSpawnArea();
                for (int j = 0; j < sp.burstsOf; j++) {
                    GameObject enemy = MonoBehaviour.Instantiate(sp.enemy);
                    enemy.transform.position = spawnArea;
                    enemies.Add(enemy);
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
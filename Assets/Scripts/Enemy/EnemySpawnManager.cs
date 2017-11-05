using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EnemySpawning {

    /// <summary>
    /// Manages actually spawning the enemies.
    /// </summary>
    public class EnemySpawnManager : MonoBehaviour {

        public Transform[] spawnAreas;
        public WaveDescription wave;

        private const float waitOnCoroutines = 0.25f;

        public IEnumerator DoWave() {
            Debug.Log("beginning wave");
            foreach (SemiWaveDescription sw in wave.spawns) {
                foreach (SpawnDescription sp in sw.toSpawn) {
                    yield return sp.DoSpawnAt(getRandomSpawnArea());
                }
            }
            yield return new WaitForSeconds(10);
        }

        public Vector3 getRandomSpawnArea() {
            return spawnAreas[Random.Range(0, spawnAreas.Length)].position;
        }

    }
}
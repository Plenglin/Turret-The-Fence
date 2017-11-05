using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EnemySpawning {

    public enum BranchType {
        SimultaneousAll,  // Spawn all the things at once and wait for all of them to end before spawning the next set
        Sequential  // These are to be spawned sequentially
    }

    [System.Serializable]
    public class WaveDescription {
        public SemiWaveDescription[] spawns;
    }

    [System.Serializable]
    public class SemiWaveDescription {
        public BranchType type;
        public SpawnDescription[] toSpawn;
    }

    [System.Serializable]
    public class SpawnDescription {

        public GameObject enemy;
        public int bursts = 1;
        public float timeBetweenBursts = 1;
        public int burstsOf = 1;
        public float timeBetweenSpawns = 0.5f;

        public IEnumerator DoSpawnAt(Vector3 position, EnemySpawnManager parent) {
            for (int i = 0; i < bursts; i++) {
                for (int j = 0; j < burstsOf; j++) {
                    MonoBehaviour.Instantiate(enemy);
                    enemy.transform.position = position;
                    if (parent != null) {
                        parent.OnCoroutineComplete();
                    }
                    yield return new WaitForSeconds(timeBetweenSpawns);
                }
                yield return new WaitForSeconds(timeBetweenBursts - timeBetweenSpawns);
            }
        }

        public IEnumerator DoSpawnAt(Vector3 position) {
            return DoSpawnAt(position, null);
        }

    }
}
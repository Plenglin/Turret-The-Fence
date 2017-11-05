using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpawnManager {
    
    /// <summary>
    /// Determines what waves to spawn next.
    /// </summary>
    public class WaveManager : MonoBehaviour {

        public WaveDescription[] waves;
        public EnemySpawnManager spawner;

        // Use this for initialization
        void Start() {

        }

        // Update is called once per frame
        void Update() {

        }

    }

    /// <summary>
    /// Manages actually spawning the enemies.
    /// </summary>
    public class EnemySpawnManager : MonoBehaviour {

        public Transform[] spawnAreas;
        public WaveDescription wave;

        public int completedCoroutines;

        private const float waitOnCoroutines = 0.25f;

        // Use this for initialization
        void Start() {
            
        }

        // Update is called once per frame
        void Update() {

        }

        IEnumerator DoWave() {
            foreach (SemiWaveDescription sw in wave.spawns) {
                switch (sw.type) {
                    case BranchType.Sequential:
                        foreach (SpawnDescription sp in sw.toSpawn) {
                            yield return sp.DoSpawnAt(getRandomSpawnArea());
                        }
                        break;
                    case BranchType.SimultaneousAll:
                        completedCoroutines = 0;
                        List<Coroutine> running = new List<Coroutine>();
                        foreach (SpawnDescription sp in sw.toSpawn) {
                            running.Add(StartCoroutine(sp.DoSpawnAt(getRandomSpawnArea())));
                        }
                        while (completedCoroutines < running.Count) {
                            yield return new WaitForSeconds(waitOnCoroutines);
                        }
                        break;
                }
            }
            yield return new WaitForSeconds(10);
        }

        public Vector3 getRandomSpawnArea() {
            return spawnAreas[Random.Range(0, spawnAreas.Length)].position;
        }

        public void OnCoroutineComplete() {
            completedCoroutines++;
        }

    }

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
                yield return new WaitForSeconds(timeBetweenBursts);
            }
        }

        public IEnumerator DoSpawnAt(Vector3 position) {
            return DoSpawnAt(position, null);
        }

    }
}
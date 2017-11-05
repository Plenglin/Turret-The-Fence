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
}
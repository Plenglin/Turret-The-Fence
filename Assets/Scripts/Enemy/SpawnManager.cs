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
        public WaveDescription current;

        // Use this for initialization
        void Start() {
            
        }

        // Update is called once per frame
        void Update() {

        }

        IEnumerator DoWave() {
            yield return new WaitForSeconds(10);
        }

    }

    public enum BranchType {
        SimultaneousAll,  // Every SpawnDescription in this branch must have been spawned
        SimultaneousAny,  // Any of the SpawnDescriptions in this branch must have been spawned
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
        public int number;
        public float timeBetweenBursts = 1;
        public int burstsOf = 1;
        public float timeBetweenSpawns = 0.1f;

    }
}
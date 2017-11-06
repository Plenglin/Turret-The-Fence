using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace EnemySpawning {

    /// <summary>
    /// Determines what waves to spawn next.
    /// </summary>
    public class SimpleWaveManager : MonoBehaviour {

        public Wave[] waves;
        public EnemySpawnManager spawner;
        public Text waveStartNotifier;
        public Text waveCounter;
        public int currentWave;

        // Use this for initialization
        void Start() {
            StartCoroutine(WaveCoroutine());
        }

        IEnumerator WaveCoroutine() {
            while (currentWave < waves.Length) {
                waveStartNotifier.enabled = true;
                yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Return));
                waveStartNotifier.enabled = false;
                spawner.wave = waves[currentWave];
                waveCounter.text = string.Format("Wave: {0}", currentWave + 1);
                yield return spawner.DoWave();
                currentWave++;
            }
            Debug.Log("All waves completed. Well done.");
        }
    }
}
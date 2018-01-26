using EnemySpawning;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AdaptiveWaveManager : MonoBehaviour {

    public int currentWave = 0;
    public float currentDifficulty = 1.0f;
    public float difficultyMultiplier = 1.4f;

    public Text waveStartNotifier;
    public Text waveCounter;

    public EnemySpawnManager spawner;

    private StatisticTracker stats;

	// Use this for initialization
	void Start () {
        stats = GameObject.FindGameObjectWithTag("Player").GetComponent<StatisticTracker>();
        StartCoroutine(WaveCoroutine());
	}
	
	// Update is called once per frame
	IEnumerator WaveCoroutine() {
        while (true) {
            waveStartNotifier.enabled = true;
            yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Return));
            waveStartNotifier.enabled = false;
            spawner.wave = generateWave();
            waveCounter.text = string.Format("Wave: {0}\nDifficulty: {1:0.00}", currentWave + 1, difficultyMultiplier);
            yield return spawner.DoWave();
            currentWave++;
            currentDifficulty *= stats.GetDifficultyCoefficient(difficultyMultiplier);
        }
    }

    Wave generateWave() {
        Wave wave = new Wave();
        
        if (currentDifficulty > 1.25) {
            //wave.spawns.Add()
        }
        return wave;
    }

}

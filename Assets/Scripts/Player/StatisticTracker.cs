using CompleteProject;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatisticTracker : MonoBehaviour {

    private PlayerHealth health;

    private int startHealth;

    private int moneyDropped;
    private int moneyCollected;

    private float waveStart;
    private float waveEnd;
    private float lastEnemyKilled;

	// Use this for initialization
	void Start () {
        health = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHealth>();
    }

    public void OnWaveBegin() {
        Debug.Log("Wave begin");
        waveStart = Time.time;
        startHealth = health.currentHealth;
    }

    public void OnWaveEnd() {
        Debug.Log("Wave end");
        waveEnd = Time.time;
    }

    public void OnLastEnemyKilled() {
        Debug.Log("Last Enemy killed");
        lastEnemyKilled = Time.time;
        float coeff = GetDifficultyCoefficient(1.25f);
        Debug.Log("Coefficient this round: " + coeff);
    }

    public void OnMoneyDropped(int amount) {
        moneyDropped += amount;
    }

    public void OnMoneyCollected(int amount) {
        moneyCollected += amount;
    }

    public float GetDifficultyCoefficient(float k) {
        float healthFactor = 1 - (float) health.currentHealth / startHealth;
        float moneyFactor = (float) moneyCollected / moneyDropped + 0.1f;
        float timeFactor = (waveEnd - waveStart) / (lastEnemyKilled - waveStart);
        timeFactor = timeFactor < 0.8f ? 0.8f : timeFactor;
        Debug.Log(healthFactor);
        Debug.Log(moneyFactor);
        Debug.Log(timeFactor);
        return 1 + k * healthFactor * moneyFactor * timeFactor;
    }

}

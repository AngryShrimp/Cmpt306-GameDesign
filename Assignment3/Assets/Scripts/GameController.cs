/*
 * Keenan Johnstone - 11119412 - kbj182
 * Oct 21st 2016 - CMPT306
 */
using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour
{
    public GameObject enemy;
    public Transform[] spawner = new Transform[4];
    public float startTime, spawnRate, spawnRateCap;
    public float spawnIncrease, spawnIncreaseRate;

    public GUIText scoreText;
    private int score;

    void Start()
    {
        score = 0;
        UpdateScore();
        InvokeRepeating("SpawnWaves", startTime, spawnRate);
        StartCoroutine(IncreaseDifficulty());
    }

    void SpawnWaves()
    {
        Vector3 spawnPosition = spawner[Random.Range(1,4)].position;
        Quaternion spawnRotation = new Quaternion(0, 0, 0, 0);
        Instantiate(enemy, spawnPosition, spawnRotation);
    }

    IEnumerator IncreaseDifficulty()
    {
        yield return new WaitForSeconds(spawnIncreaseRate);
        if(spawnRate > spawnRateCap)
        {
            spawnRate -= spawnIncrease;
            StartCoroutine(IncreaseDifficulty());
        }
    }

    public void AddScore(int value)
    {
        score += value;
        UpdateScore();
    }

    void UpdateScore()
    {
        scoreText.text = "Score: " + score;
    }
}

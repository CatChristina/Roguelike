using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject[] enemies;
    private GameObject[] spawnLocations;

    public TMP_Text waveTMP;
    private int wave;
    private int enemiesPerWave;

    private void Start()
    {
        spawnLocations = GameObject.FindGameObjectsWithTag("EnemySpawn");
        currentWaveEnemies = new List<GameObject>();

        enemiesPerWave = 10;
        wave = 1;

        SpawnEnemies();
        InvokeRepeating(nameof(CheckEnemyCount), 10, 10);
    }

    private List<GameObject> currentWaveEnemies;

    // Spawns enemies at random predetermined locations
    private void SpawnEnemies()
    {
        for (int i = 0; i < enemiesPerWave; i++)
        {
            currentWaveEnemies.Add(Instantiate(enemies[Random.Range(0, enemies.Length)], spawnLocations[Random.Range(0, spawnLocations.Length)].transform.position, Quaternion.identity));
        }

        //InvokeRepeating(nameof(CheckEnemyCount), 10, 10);
        Debug.Log("Spawning Enemies.");
    }

    // Check if all enemies are dead and if so, start the next wave
    private void CheckEnemyCount()
    {
        currentWaveEnemies.RemoveAll(enemy => enemy == null); // Remove all enemies that have been destroyed from the list

        if (currentWaveEnemies.Count <= 0)
        {
            NextWave();
            //CancelInvoke(nameof(CheckEnemyCount));
        }
    }

    // Start the next wave
    private void NextWave()
    {
        wave++;
        waveTMP.text = "Wave: " + wave.ToString();
        enemiesPerWave = Mathf.CeilToInt(enemiesPerWave * 1.1f) + 5;
        SpawnEnemies();
        Debug.Log("Wave " + wave + " has started!");
    }
}

using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject[] enemies;
    private GameObject[] spawnLocations;

    public TMP_Text time;
    private int wave;
    private int enemiesPerWave;

    private void Start()
    {
        spawnLocations = GameObject.FindGameObjectsWithTag("EnemySpawn");
        currentWaveEnemies = new List<GameObject>();

        enemiesPerWave = 10;
        wave = 1;

        SpawnEnemies();
    }

    private void FixedUpdate()
    {

    }

    private void Update()
    {
        //timeSlider.value -= Time.deltaTime * Time.timeScale;      Unused
    }

    private List<GameObject> currentWaveEnemies;

    // Spawns enemies at random predetermined locations
    private void SpawnEnemies()
    {
        for (int i = 0; i < enemiesPerWave; i++)
        {
            currentWaveEnemies.Add(Instantiate(enemies[Random.Range(0, enemies.Length)], spawnLocations[Random.Range(0, spawnLocations.Length)].transform.position, Quaternion.identity));
        }

        InvokeRepeating(nameof(CheckEnemyCount), 10, 10);
    }

    private void CheckEnemyCount()
    {
        if (currentWaveEnemies.Count <= 0)
        {
            NextWave();
            CancelInvoke(nameof(CheckEnemyCount));
        }
    }

    private void NextWave()
    {
        wave++;
        enemiesPerWave = Mathf.CeilToInt(enemiesPerWave * 1.1f) + 5;

        SpawnEnemies();
    }
}

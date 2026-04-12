using UnityEngine;
using TMPro;



public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab;
    public TextMeshProUGUI waveText;
    public Transform[] spawnPoints;

    public float timeBetweenWaves = 20f;
    private float countdown;

    public int waveNumber = 1;

    void Start()
    {
        countdown = timeBetweenWaves;
    }

    void Update()
    {
        countdown -= Time.deltaTime;

        if (countdown <= 0f)
        {
            StartCoroutine(SpawnWave());
            countdown = timeBetweenWaves;
        }
    }

    System.Collections.IEnumerator SpawnWave()
    {
        Debug.Log("Wave: " + waveNumber);

        for (int i = 0; i < waveNumber + 2; i++)
        {
            SpawnEnemy();
            yield return new WaitForSeconds(0.5f);
        }

        waveNumber++;
        //UI
        waveText.text = "Wave: " + waveNumber;
    }

    void SpawnEnemy()
    {
        Transform spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];
        Instantiate(enemyPrefab, spawnPoint.position, Quaternion.identity);
    }
}
using System.Collections;
using TMPro;
using UnityEngine;

public class WaveSpawner : MonoBehaviour
{
    public Transform[] enemyPrefab;
    public Transform bossPrefab;
    public Transform spawnPoint;
    public TextMeshProUGUI waveCountdownText;
    public float timeBetweenWaves = 5f;
    public float waveTimeLimit = 20f;
    public float bossTimeLimit = 60f;
    public GameObject gameoverUI;
    public static bool isGameOver;

    private float countdown = 2f;
    private int waveIndex = 0;
    private int enemyCount = 10;
    private int activeEnemies = 0;
    private bool isBossWave = false;
    private float waveTimer = 0f;

    private void Start()
    {
        isGameOver = false;
    }

    void Update()
    {
        if (isGameOver) return;
        UpdateWaveTimer();
    }

    void UpdateWaveTimer()
    {
        if (activeEnemies == 0)
        {
            if (waveTimer > 5f)
            {
                waveTimer = 5f;
            }
            countdown = waveTimer;
        }

        waveTimer -= Time.deltaTime;
        countdown -= Time.deltaTime;

        if (waveTimer <= 0f)
        {
            if (activeEnemies > 0)
            {
                GameOver();
                return;
            }
            else
            {
                StartCoroutine(SpawnWave());
                countdown = timeBetweenWaves;
                return;
            }
        }

        waveCountdownText.text = string.Format("{0} Time: {1:00.00}", isBossWave ? "Boss" : "Wave", waveTimer);

        if (countdown <= 0f && activeEnemies == 0)
        {
            StartCoroutine(SpawnWave());
            countdown = timeBetweenWaves;
        }
    }

    IEnumerator SpawnWave()
    {
        waveIndex++;
        PlayerStats.rounds++;

        if (waveIndex % 5 == 0)
        {
            isBossWave = true;
            waveTimer = bossTimeLimit;
            SpawnBoss();
        }
        else
        {
            isBossWave = false;
            waveTimer = waveTimeLimit;
            int enemyIndex = (waveIndex - 1) % enemyPrefab.Length;
            for (int i = 0; i < enemyCount; i++)
            {
                SpawnEnemy(enemyIndex);
                yield return new WaitForSeconds(0.3f);
            }
            enemyCount++;
        }
    }

    void SpawnEnemy(int enemyIndex)
    {
        Transform selectedEnemyPrefab = enemyPrefab[enemyIndex];
        GameObject enemy = Instantiate(selectedEnemyPrefab, spawnPoint.position, spawnPoint.rotation).gameObject;
        Enemy enemyScript = enemy.GetComponent<Enemy>();
        if (enemyScript != null)
        {
            enemyScript.Initialize(waveIndex);
            enemyScript.OnEnemyDestroyed += EnemyDestroyed;
        }
        activeEnemies++;
    }

    void SpawnBoss()
    {
        GameObject boss = Instantiate(bossPrefab, spawnPoint.position, spawnPoint.rotation).gameObject;
        Enemy bossScript = boss.GetComponent<Enemy>();
        if (bossScript != null)
        {
            bossScript.Initialize(waveIndex);
            bossScript.OnEnemyDestroyed += EnemyDestroyed;
        }
        activeEnemies++;
    }

    void EnemyDestroyed()
    {
        activeEnemies--;
    }

    void GameOver()
    {
        isGameOver = true;
        gameoverUI.SetActive(true);
    }
}
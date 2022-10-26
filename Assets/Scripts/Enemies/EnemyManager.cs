using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class EnemyManager 
{
    public static List<GameObject> enemiesToSpawn;

    public static int currentWaveAmount;

    public static bool enemiesSpawned = false;
    public static bool spawning = false;

    private static int enemiesCurrentlySpawned = 0;

    public static int enemiesToSpawnCount;

    public static void InitializeComponents(List<GameObject> enemyList, int roundDifficulty)
    {
        enemiesToSpawn = new List<GameObject>();
        currentWaveAmount = 0;
        enemiesCurrentlySpawned = 0;
        spawning = true;

        UiStatManager.ChangeAmount("Round", EnemySpawner.RoundsPassed.ToString());
        UiStatManager.ChangeAmount("RoundDifficulty", roundDifficulty.ToString());
        SelectEnemyToSpawn(enemyList, roundDifficulty);
    }

    private static void SelectEnemyToSpawn(List<GameObject> enemyList, int roundDifficulty) //working but really inefficiently, LIKE... REALLY INEFFICIENTLY.
    {
        RandomizeOrder(enemyList);
        if (currentWaveAmount < roundDifficulty)
        {
            EnemyStats.EnemyRarity enemyRarity = CheckEnemyRarity();

            if (enemyList[0].GetComponent<Enemy>().EnemyStats.Rarity == enemyRarity)
            {
                enemiesToSpawn.Add(Resources.Load<GameObject>($"Enemies/{enemyList[0].name}"));
                currentWaveAmount += enemyList[0].GetComponent<Enemy>().EnemyStats.DifficultyLvl;
            }

            SelectEnemyToSpawn(enemyList, roundDifficulty);
        }

        enemiesToSpawnCount = enemiesToSpawn.Count;
        UiStatManager.ChangeAmount("EnemyCount", enemiesToSpawnCount.ToString());
        UiStatManager.ChangeAmount("CurrentEnemies", enemiesToSpawnCount.ToString());
    }

    public static void SpawnEnemies(List<GameObject> enemyList, float spawnRange, int roundDifficulty, int waveAmount, Transform parentTransform)
    {
        for (int i = 0; i < waveAmount; i++)
        {

            if (enemiesCurrentlySpawned < enemiesToSpawn.Count)
            {
                float rand = Random.Range(-spawnRange, spawnRange);
                GameObject.Instantiate(enemiesToSpawn[enemiesCurrentlySpawned], new Vector3(rand, parentTransform.position.y, 0), Quaternion.identity);
                enemiesCurrentlySpawned++;
            }
            else spawning = false;
        }

        enemiesSpawned = true;
    }

    public static int SetWaveSpawnAmount(int roundDifficulty)
    {
        int waveAmount = 0;
        if(roundDifficulty < 10)
        {
            waveAmount = roundDifficulty / 2;
        }
        else if(roundDifficulty < 18)
        {
            waveAmount = roundDifficulty / 3;
        }
        else
        {
            waveAmount = roundDifficulty / 4;
        }
        return waveAmount;
    }

    public static void CheckEnemyList()
    {
        if (enemiesToSpawnCount == 0)
        {
            GameState.currentGameState = CurrentGameState.PickUpgradeState;
            enemiesSpawned = true;
        }
    }

    private static EnemyStats.EnemyRarity CheckEnemyRarity()
    {
        float randValue = Random.value;
        if(randValue <= 0.6f)
        {
            return EnemyStats.EnemyRarity.common;
        }
        else if(randValue > 0.6f && randValue <= 0.85f )
        {
            return EnemyStats.EnemyRarity.uncommon;
        }
        else if(randValue > 0.85f && randValue <= 0.95f)
        {
            return EnemyStats.EnemyRarity.rare;
        }
        else if (randValue > 0.95f && randValue <= 0.99f)
        {
            return EnemyStats.EnemyRarity.veryRare;
        }
        else 
        {
            return EnemyStats.EnemyRarity.UltraRare;
        }
    }

    private static void RandomizeOrder(List<GameObject> enemyList)
    {
        for (int i = 0; i < enemyList.Count - 1; i++)
        {
            GameObject temp = enemyList[i];
            int rand = Random.Range(i, enemyList.Count);
            enemyList[i] = enemyList[rand];
            enemyList[rand] = temp;
        }
    }
}

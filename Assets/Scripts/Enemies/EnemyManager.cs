using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class EnemyManager 
{
    public static List<GameObject> enemiesToSpawn;

    private static int maxWaveAmount;

    private static int currentRoundDifficulty;

    public static bool enemiesSpawned = false;

    // Add enemy wave mechanic and increment difficulty level after rounds
    // Add time between gameplay and upgrade.

    public static void SpawnEnemies(List<GameObject> enemyList, float spawnRange, int roundsPassed, int roundDifficulty, Transform parentTransform)
    {
        InitializeComponents();
        SelectEnemyToSpawn(enemyList, roundDifficulty);
        foreach (var enemy in enemiesToSpawn)
        {
            float rand = Random.Range(-spawnRange, spawnRange);
            GameObject.Instantiate(enemy, new Vector3(rand, parentTransform.position.y, 0), Quaternion.identity);
        }
        UiStatManager.ChangeAmount("EnemyCount", enemiesToSpawn.Count.ToString());
        UiStatManager.ChangeAmount("CurrentEnemies", enemiesToSpawn.Count.ToString());
        enemiesSpawned = true;
    }

    private static void SelectEnemyToSpawn(List<GameObject> enemyList, int roundDifficulty)
    {
        RandomizeOrder(enemyList);
        if(currentRoundDifficulty < roundDifficulty)
        {
            EnemyStats.EnemyRarity enemyRarity = CheckEnemyRarity();
            
            if (enemyList[0].GetComponent<Enemy>().EnemyStats.Rarity == enemyRarity)
            {
                Debug.Log("Added " + enemyRarity + " enemy");
                enemiesToSpawn.Add(Resources.Load<GameObject>($"Enemies/{enemyList[0].name}"));
                currentRoundDifficulty += enemyList[0].GetComponent<Enemy>().EnemyStats.DifficultyLvl;
                SelectEnemyToSpawn(enemyList, roundDifficulty);
            }
            else
            {
                SelectEnemyToSpawn(enemyList, roundDifficulty);
            }
        }
    }

    public static void CheckEnemyList()
    {
        if (enemiesToSpawn.Count == 0 || enemiesToSpawn == null)
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

    private static void InitializeComponents()
    {
        enemiesToSpawn = new List<GameObject>();
        currentRoundDifficulty = 0;
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

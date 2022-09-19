using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class EnemyManager 
{
    //Missing checking rarity, make "Rounds" and with that comes progression through roundDifficulty & roundsPassed.

    public static List<GameObject> enemiesToSpawn;

    private static int maxWaveAmount;

    private static int currentRoundDifficulty;

    public static bool enemiesSpawned = false;


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
        int randEnemy = Random.Range(0, enemyList.Count);
        if(currentRoundDifficulty < roundDifficulty)
        {
            enemiesToSpawn.Add(Resources.Load<GameObject>($"Enemies/{enemyList[randEnemy].name}"));
            currentRoundDifficulty += enemyList[randEnemy].GetComponent<Enemy>().EnemyStats.DifficultyLvl;
            SelectEnemyToSpawn(enemyList, roundDifficulty);
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

    private static void InitializeComponents()
    {
        enemiesToSpawn = new List<GameObject>();
        currentRoundDifficulty = 0;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private List<GameObject> enemyList;
    [SerializeField] private float spawnRange;

    private static int roundDifficulty = 6;
    private static int roundsPassed = 1;

    public List<GameObject> EnemyList { get => enemyList; set => enemyList = value; }
    public static int RoundsPassed { get => roundsPassed; set => roundsPassed = value; }

    void Start()
    {
        
    }

    void Update()
    {
        if(GameState.currentGameState == CurrentGameState.PlayingState && !EnemyManager.enemiesSpawned)
        {
            EvenRoundDifficulty();
            EnemyManager.InitializeComponents(enemyList, roundDifficulty);
            StartCoroutine(spawnWave());
            EnemyManager.enemiesSpawned = true;
        }

    }

    public static void UpdateRoundDifficulty()
    {
        roundsPassed++;
        roundDifficulty += roundsPassed;
    }

    private IEnumerator spawnWave()
    {
        int waveAmount = EnemyManager.SetWaveSpawnAmount(roundDifficulty);
        while (EnemyManager.spawning == true)
        {
            EnemyManager.SpawnEnemies(spawnRange, waveAmount, transform);
            yield return new WaitForSeconds(3f);
        }
        yield return null;
    }

    private void EvenRoundDifficulty()
    {
        int evenRoundDifficulty = roundDifficulty % 2;
        if(evenRoundDifficulty == 1)
        {
            roundDifficulty--;
        }
    }
}

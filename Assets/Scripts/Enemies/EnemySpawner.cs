using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private List<GameObject> enemyList;
    [SerializeField] private float spawnRange;

    private int roundDifficulty = 5;

    private int roundsPassed = 0;

    public List<GameObject> EnemyList { get => enemyList; set => enemyList = value; }
    public int RoundsPassed { get => roundsPassed; set => roundsPassed = value; }

    void Start()
    {
        
    }

    void Update()
    {
        if(GameState.currentGameState == CurrentGameState.PlayingState && !EnemyManager.enemiesSpawned)
        {
            EnemyManager.SpawnEnemies(enemyList, spawnRange, roundsPassed, roundDifficulty, transform);
            EnemyManager.enemiesSpawned = true;
        }

#if UNITY_EDITOR
        if (Input.GetKeyDown(KeyCode.J))
        {
            EnemyManager.SpawnEnemies(enemyList, spawnRange, roundsPassed, roundDifficulty, transform);
        }
#endif
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CurrentGameState {PlayingState, PickUpgradeState}

public static class GameState
{
    public static CurrentGameState currentGameState;
}

public class GameManager : MonoBehaviour
{
    [SerializeField] private List<Upgrade> upgradeCards;
    private void Start()
    {
        GameState.currentGameState = CurrentGameState.PickUpgradeState;
        UpgradeManager.InitializeUpgrades(upgradeCards);
        UpgradeManager.DraftUpgrades(upgradeCards);
    }

    private void Update()
    {
        if (GameState.currentGameState == CurrentGameState.PickUpgradeState && EnemyManager.enemiesSpawned)
        {
            StartCoroutine(GameStateTransition());
        }
    }

    private IEnumerator GameStateTransition()
    {
        EnemyManager.enemiesSpawned = false;
        EnemySpawner.UpdateRoundDifficulty();
        yield return new WaitForSeconds(1.5f);
        UpgradeManager.DraftUpgrades(upgradeCards);
    }
}

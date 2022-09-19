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
            UpgradeManager.DraftUpgrades(upgradeCards);
            EnemyManager.enemiesSpawned = false;
        }
#if UNITY_EDITOR
        if (Input.GetKeyDown(KeyCode.H))
    {
            UpgradeManager.DraftUpgrades(upgradeCards);
            UpgradeManager.count++;
    }
#endif

    }
}

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

    private List<Upgrade> commonCards;
    private List<Upgrade> rareCards;
    private List<Upgrade> mythicCards;
    private List<Upgrade> legendaryCards;

    private void Start()
    {
        SeparateCardsByRarity();
        GameState.currentGameState = CurrentGameState.PickUpgradeState;
        UpgradeManager.initializeComponents(commonCards, rareCards, legendaryCards);
        UpgradeManager.InitializeUpgrades(upgradeCards);
        UpgradeManager.DraftUpgrades();
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
        UpgradeManager.DraftUpgrades();
    }

    private void SeparateCardsByRarity()
    {
        commonCards = new List<Upgrade>();
        rareCards = new List<Upgrade>();
        mythicCards = new List<Upgrade>();
        legendaryCards = new List<Upgrade>();

        foreach (Upgrade upgrade in upgradeCards)
        {
            switch (upgrade.UpgradeStats.Rarity)
            {
                case CardRarities.common:
                    commonCards.Add(upgrade);
                    break;
                case CardRarities.rare:
                    rareCards.Add(upgrade);
                    break;
                case CardRarities.legendary:
                    legendaryCards.Add(upgrade);
                    break;
                case CardRarities.mythic:
                    mythicCards.Add(upgrade);
                    break;
            }
        }
    }
}

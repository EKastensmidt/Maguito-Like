using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class UpgradeManager 
{
    public static int count = 0;
    public static List<Transform> upgradeSlots;

    public static List<CardRarities> draftedRarities;
    private static List<Upgrade> _commonUpgrades;
    private static List<Upgrade> _rareUpgrades;


    public static void initializeComponents(List<Upgrade> commonUpgrades, List<Upgrade> rareUpgrades, List<Upgrade> legendaryUpgrades)
    {
        draftedRarities = new List<CardRarities>();
        _commonUpgrades = new List<Upgrade>(commonUpgrades);
        _rareUpgrades = new List<Upgrade>(rareUpgrades);
        //List<Upgrade> _legendaryUpgrades = new List<Upgrade>(legendaryUpgrades);
    }

    public static void InitializeUpgrades(List<Upgrade> upgrades)
    {
        foreach (var upgrade in upgrades)
        {
            upgrade.UpgradeStats.Execute();
        }
    }
    public static void InitializeUpgradeSlots(List<Transform> upgradeSlots)
    {
        UpgradeManager.upgradeSlots = upgradeSlots;
    }

    //When Legendary Cards in game: Remove commented lines.

    public static void DraftUpgrades() 
    {
        RandomizeOrder(_commonUpgrades);
        RandomizeOrder(_rareUpgrades);
        CheckAllowedTimes(_commonUpgrades);
        CheckAllowedTimes(_rareUpgrades);

        DraftCardRarities();

        for (int i = 0; i < upgradeSlots.Count; i++)
        {
            switch (draftedRarities[i])
            {
                case CardRarities.common:
                    GameObject.Instantiate(_commonUpgrades[i], upgradeSlots[i]);
                    break;
                case CardRarities.rare:
                    GameObject.Instantiate(_rareUpgrades[i], upgradeSlots[i]);
                    break;
                case CardRarities.legendary:
                    //GameObject.Instantiate(_legendaryUpgrades[i], upgradeSlots[i]);
                    //_legendaryUpgrades.RemoveAt(i);
                    break;
            }
        }
    }

    private static void DraftCardRarities()
    {
        draftedRarities = new List<CardRarities>();
        for (int i = 0; i < upgradeSlots.Count; i++)
        {
            float randValue = Random.value;
            Debug.Log(randValue);
            if (randValue <= 0.70f)
            {
                draftedRarities.Add(CardRarities.common);
            }
            else if (randValue > 0.70f && randValue <= 0.95f)
            {
                draftedRarities.Add(CardRarities.rare);
            }
            else
            {
                draftedRarities.Add(CardRarities.rare);
                //draftedRarities.Add(CardRarities.legendary);
            }
        }
    }

    public static void OnExecute(Upgrade upgrade)
    {
        upgrade.UpgradeStats.AllowedTimesCount--;
        DeleteInstantiatedCards();
        GameState.currentGameState = CurrentGameState.PlayingState;
    }

    private static void DeleteInstantiatedCards()
    {
        for (int i = 0; i < upgradeSlots.Count; i++)
        {
            int childs = upgradeSlots[i].childCount;
            for (int j = 0; j < childs; j++)
            {
                GameObject.Destroy(upgradeSlots[i].GetChild(j).gameObject);
            }
        }
    }

    private static void CheckAllowedTimes(List<Upgrade> upgrades)
    {
        for (int i = 0; i < upgrades.Count; i++)
        {
            if (upgrades[i].UpgradeStats.AllowedTimesCount <= 0)
            {
                upgrades.Remove(upgrades[i]);
            }
        }
    }

    private static void RandomizeOrder(List<Upgrade> upgrades)
    {
        for (int i = 0; i < upgrades.Count - 1; i++)
        {
            Upgrade temp = upgrades[i];
            int rand = Random.Range(i, upgrades.Count);
            upgrades[i] = upgrades[rand];
            upgrades[rand] = temp;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class UpgradeManager 
{
    public static int count = 0;
    public static List<Transform> upgradeSlots;

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

    public static void DraftUpgrades(List<Upgrade> upgrades) //Need to check for card rarity
    {
        RandomizeOrder(upgrades);
        for (int i = 0; i < upgrades.Count; i++)
        {
            if (upgrades[i].UpgradeStats.AllowedTimesCount <= 0)
            {
                upgrades.Remove(upgrades[i]);
            }
        }

        for (int i = 0; i < upgradeSlots.Count; i++)
        {
            GameObject.Instantiate(upgrades[i], upgradeSlots[i]);   
        }
    }

    private static void CheckCardRarity()
    {

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

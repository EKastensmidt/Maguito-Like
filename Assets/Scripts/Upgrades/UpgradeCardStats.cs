using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Upgrade", menuName = "Scriptable Object/UpgradeCard", order = 0)]
public class UpgradeCardStats : ScriptableObject
{
    [SerializeField] private string cardName;
    [SerializeField] private string cardText;
    [SerializeField] private CardRarities rarity;
    [SerializeField] private int upgradeAllowedTimes;

    private int allowedTimesCount;
    public string CardName { get => cardName; set => cardName = value; }
    public string CardText { get => cardText; set => cardText = value; }
    public CardRarities Rarity { get => rarity; set => rarity = value; }
    public int AllowedTimesCount { get => allowedTimesCount; set => allowedTimesCount = value; }

    public void Execute()
    {
        allowedTimesCount = upgradeAllowedTimes;
    }
}
public enum CardRarities { common, rare, mythic, legendary }

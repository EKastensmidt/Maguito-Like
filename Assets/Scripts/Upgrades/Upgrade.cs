using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Upgrade : MonoBehaviour
{
    //Each Upgrade is a prefab

    [SerializeField] private UpgradeCardStats upgradeStats;
    [SerializeField] protected PlayerStats playerStats;

    [SerializeField] private GameObject cardTitle, cardText;

    public UpgradeCardStats UpgradeStats { get => upgradeStats; set => upgradeStats = value; }

    public virtual void Start()
    {
        cardTitle.GetComponent<TextMeshProUGUI>().SetText(upgradeStats.CardName);
        cardText.GetComponent<TextMeshProUGUI>().SetText(upgradeStats.CardText);
    }

    public virtual void Update()
    {

    }

    public virtual void Execute()
    {
        UpgradeManager.OnExecute(this);
    }
}

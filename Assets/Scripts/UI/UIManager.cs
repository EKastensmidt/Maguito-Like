using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class UIManager : MonoBehaviour
{
    [SerializeField] private List<Transform> upgradeSlots;
    void Awake()
    {
        UiStatManager.GetUIStats();
        UpgradeManager.InitializeUpgradeSlots(upgradeSlots);
    }
}

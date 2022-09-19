using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public static class UiStatManager 
{
    private static List<UIStat> uiStats;
    public static List<UIStat> GetUIStats()
    {
        List<UIStat> uiStats = GameObject.FindObjectsOfType<UIStat>().ToList();
        UiStatManager.uiStats = uiStats;
        return uiStats;
    }

    public static void ChangeAmount(string stat, string amount)
    {
        if (uiStats == null)
            return;

        foreach (var uiStat in uiStats)
        {
            if(stat == uiStat.gameObject.name)
            {
                uiStat.TmpText.text = amount;
            }
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageUp : Upgrade
{
    public override void Execute()
    {
        playerStats.CurrentDamage += 2;
        UiStatManager.ChangeAmount("Damage", playerStats.CurrentDamage.ToString());
        base.Execute();
    }
}

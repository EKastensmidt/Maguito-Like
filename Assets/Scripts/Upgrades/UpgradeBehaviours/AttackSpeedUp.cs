using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackSpeedUp : Upgrade
{
    public override void Execute()
    {
        playerStats.CurrentAttackSpeed -= 0.1f;
        UiStatManager.ChangeAmount("AttackSpeed", playerStats.CurrentAttackSpeed.ToString());
        base.Execute();
    }
}

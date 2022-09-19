using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileSpeedUp : Upgrade
{
    public override void Execute()
    {
        playerStats.CurrentProjectileSpeed += 1;
        UiStatManager.ChangeAmount("ProjectileSpeed", playerStats.CurrentProjectileSpeed.ToString());
        base.Execute();
    }
}

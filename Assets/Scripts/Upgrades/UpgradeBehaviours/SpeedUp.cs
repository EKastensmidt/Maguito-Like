using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedUp : Upgrade
{
    public override void Execute()
    {
        playerStats.CurrentSpeed += 1;
        UiStatManager.ChangeAmount("Speed", playerStats.CurrentSpeed.ToString());
        base.Execute();
    }
}

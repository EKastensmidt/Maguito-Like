using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeStealUp : Upgrade
{
    public override void Execute()
    {
        playerStats.LifeSteal += 0.1f;
        base.Execute();
    }
}

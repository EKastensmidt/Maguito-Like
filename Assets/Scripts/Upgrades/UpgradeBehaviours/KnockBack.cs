using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnockBack : Upgrade
{
    public override void Execute()
    {
        playerStats.KnockBack += 2;
        base.Execute();
    }
}

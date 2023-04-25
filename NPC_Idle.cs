using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class NPC_Idle : _State<NPC01>
{
    float IdleTime;
    // Start is called before the first frame update
    public override void Enter(NPC01 target)
    {
        IdleTime = 1;
    }

    public override void Execute(NPC01 target)
    {
        IdleTime -= Time.deltaTime;
        if (IdleTime <= 0)
        {
            target.Change();
        }
    }

    public override void Exit(NPC01 target)
    {

    }
}

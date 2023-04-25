using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC01_Move : _State<NPC01>
{
    public override void Enter(NPC01 target)
    {
        target.index = Random.Range(0, 4);
    }

    public override void Execute(NPC01 target)
    {
        target.Move();
    }

    public override void Exit(NPC01 target)
    {
        
    }
}

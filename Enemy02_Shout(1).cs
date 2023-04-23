using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy02_Shout : _State<Enemy02>
{
    float ShoutTime;
    public override void Enter(Enemy02 target)
    {
        ShoutTime = 2.167f;
        target.ani.SetBool("Move", false);
        target.ani.SetBool("Shout", true);
        target.ani.SetBool("Attack1", false);
        target.ani.SetBool("Attack2", false);
        target.ani.SetBool("Hit", false);
        target.ani.SetBool("Die", false);
    }

    public override void Execute(Enemy02 target)
    {
        ShoutTime -= Time.deltaTime;
        if (ShoutTime <= 0)
        {
            target.ChageState(BossType.Attack);
        }
    }

    public override void Exit(Enemy02 target)
    {
        
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy02_Attack : _State<Enemy02>
{
    int temp;
    float AttackTime;
    public override void Enter(Enemy02 target)
    {
        temp = Random.Range(1, 3);
        if (temp == 1)
        {
            AttackTime = 3f;
            target.ani.SetBool("Move", false);
            target.ani.SetBool("Shout", false);
            target.ani.SetBool("Attack1", true);
            target.ani.SetBool("Attack2", false);
            target.ani.SetBool("Hit", false);
            target.ani.SetBool("Die", false);
        }
        else if (temp == 2)
        {
            AttackTime = 2;
            target.ani.SetBool("Move", false);
            target.ani.SetBool("Shout", false);
            target.ani.SetBool("Attack1", false);
            target.ani.SetBool("Attack2", true);
            target.ani.SetBool("Hit", false);
            target.ani.SetBool("Die", false);
        }
    }

    public override void Execute(Enemy02 target)
    {
        AttackTime -= Time.deltaTime;
        if (AttackTime <= 0)
        {
            target.ChageState(BossType.Idle);
        }
    }

    public override void Exit(Enemy02 target)
    {
        
    }
}

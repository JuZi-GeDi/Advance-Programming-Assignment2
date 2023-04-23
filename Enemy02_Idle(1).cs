using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy02_Idle : _State<Enemy02>
{
    //´ý»úÊ±¼ä
    float IdleTime;
    bool isFindPlayer = false;
    public override void Enter(Enemy02 target)
    {
        IdleTime = 5;
        target.ani.SetBool("Move", false);
        target.ani.SetBool("Shout", false);
        target.ani.SetBool("Attack1", false);
        target.ani.SetBool("Attack2", false);
        target.ani.SetBool("Hit", false);
        target.ani.SetBool("Die", false);
    }

    public override void Execute(Enemy02 target)
    {
        isFindPlayer = target.CanFindPlayer();
        if (isFindPlayer)
        {
            IdleTime -= Time.deltaTime;
            if (IdleTime <= 0)
            {
                target.ChageState(BossType.Run);
            }
        }
    }

    public override void Exit(Enemy02 target)
    {
        
    }
}

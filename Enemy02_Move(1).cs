using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy02_Move : _State<Enemy02>
{
    bool canAttack = true;
    public override void Enter(Enemy02 target)
    {
        target.ani.SetBool("Move", true);
        target.ani.SetBool("Shout", false);
        target.ani.SetBool("Attack1", false);
        target.ani.SetBool("Attack2", false);
        target.ani.SetBool("Hit", false);
        target.ani.SetBool("Die", false);
    }

    public override void Execute(Enemy02 target)
    {
        target.Move();
        //执行移动方法
        canAttack = target.CanShoutPlayer();
        if (canAttack)
        {
            target.ChageState(BossType.Shout);
        }
    }

    public override void Exit(Enemy02 target)
    {
        
    }
}

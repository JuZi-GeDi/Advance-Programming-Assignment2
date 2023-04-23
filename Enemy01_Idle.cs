using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy01_Idle : _State<Enemy01>
{
    //待机时间
    float IdleTime;
    public override void Enter(Enemy01 target)
    {
        target.ani.SetBool("Move", false);
        target.ani.SetBool("Attack", false);
        IdleTime = 1.333f;
    }

    public override void Execute(Enemy01 target)
    {
        IdleTime -= Time.deltaTime;
        if (IdleTime <= 0)
        {
            //切换到移动
            target.ChageState(EnemyType.Move);
        }
    }

    public override void Exit(Enemy01 target)
    {
        
    }
}

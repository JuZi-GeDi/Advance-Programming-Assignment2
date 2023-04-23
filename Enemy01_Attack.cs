using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//¹¥»÷×´Ì¬
public class Enemy01_Attack : _State<Enemy01>
{
    float AttackTime;//ÒÆ¶¯Ê±¼ä
    public override void Enter(Enemy01 target)
    {
        target.ani.SetBool("Move", false);
        target.ani.SetBool("Attack", true);
        AttackTime = 1.333f;
    }

    public override void Execute(Enemy01 target)
    {
        target.FaceToPlayer();
        AttackTime -= Time.deltaTime;
        if (AttackTime <= 0)
        {
            target.ChageState(EnemyType.Idle);
        }
    }

    public override void Exit(Enemy01 target)
    {
        
    }

}

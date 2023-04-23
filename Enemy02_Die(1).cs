using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy02_Die : _State<Enemy02>
{
    float temp;
    float HitTime;
    public override void Enter(Enemy02 target)
    {
        temp = -10;
        HitTime = 1.7f;
        if (target.HP > 0)
        {
            target.ani.SetBool("Move", false);
            target.ani.SetBool("Shout", false);
            target.ani.SetBool("Attack1", false);
            target.ani.SetBool("Attack2", false);
            target.ani.SetBool("Hit", true);
            target.ani.SetBool("Die", false);
        }
        else
        {
            target.ani.SetBool("Move", false);
            target.ani.SetBool("Shout", false);
            target.ani.SetBool("Attack1", false);
            target.ani.SetBool("Attack2", false);
            target.ani.SetBool("Hit", true);
            target.ani.SetBool("Die", false);
            target.ani.SetInteger("HP",0);
            for(int i = 0; i <= 3; i++)
            {
                target.Bodies[i].GetComponent<SkinnedMeshRenderer>().material = target.Death;
            }
        }
    }

    public override void Execute(Enemy02 target)
    {
        HitTime -= Time.deltaTime;
        temp += Time.deltaTime * 3;
        if (HitTime <= 0)
        {
            if (target.HP > 0)
            {
                target.ChageState(BossType.Idle);
            }
        }
        if (target.HP <= 0)
        {
            for (int i = 0; i <= 3; i++)
            {
                target.Bodies[i].GetComponent<SkinnedMeshRenderer>().materials[0].SetFloat("_Clipthreshold", temp);
            }
        }
        
    }

    public override void Exit(Enemy02 target)
    {
        
    }
}

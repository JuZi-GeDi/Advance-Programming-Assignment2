using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//�ƶ�״̬
public class Enemy01_Move : _State<Enemy01>
{
    public override void Enter(Enemy01 target)
    {
        target.ani.SetBool("Move", true);
        target.ani.SetBool("Attack", false);
    }

    public override void Execute(Enemy01 target)
    {
        //ִ���ƶ�����
        target.Move();
    }

    public override void Exit(Enemy01 target)
    {
        
    }

}

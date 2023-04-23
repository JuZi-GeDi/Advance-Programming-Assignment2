using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class Friend_FSM : FSM
{
    public GameObject[] Target;//用来存放敌人巡逻时的位置坐标，一般两个以上
    private Animator ani;//可以放在FSM里面，但是这里我还没确定下来后续的动画是不是都有5种。
    private NavMeshAgent agent;//我通过NavMesh导航
    private bool isCanAttack = true;
    private int i = 0;
    private float time = 2.0f;
    private float CD = 3f;

    private void Awake()
    {
        ani = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
        ChangeState(State.Patrol, () => { ani.SetTrigger("Walk"); });
    }
    public void Demage()
    {
        if (HP>0)
        {
            ChangeState(State.Damage, () => {
                ani.SetTrigger("GetHit");
                HP--;
                //Debug.Log("friend-1");
            });
        }
      
    }
   
    public override void StateIdle()
    {
        //下面转换追击玩家的逻辑是判断距离
        //if (distance < 5)
        //{
        //    ChangeState(State.Trace, () => { ani.SetTrigger("Walk"); });
        //}
    }
    public override void StatePatrol()
    {
        agent.speed = 2.0f;//AI导航速度为2.0F，一般每个状态下都需要编写不同的速度。
        time -= Time.deltaTime;
        if (time <= 0)
        {
            time = 2f;
            i = Random.Range(0, Target.Length);//计时器，再不同的巡逻点之间进行随机选择性巡逻
        }
        agent.SetDestination(Target[i].transform.position);//朝向目的移动
        //下面转换追击玩家的逻辑是判断距离
        if (distance < 5 && target != null)
        {
            ChangeState(State.Trace, () => { ani.SetTrigger("Walk"); });
        }
    }


    public override void StateTrace()
    {
        if (target == null)
        {
            ChangeState(State.Patrol, () => { ani.SetTrigger("Walk"); });
        }
        else
        {
            agent.speed = 3f;//追击状态下速度升到3f
            agent.SetDestination(target.transform.position);//敌人开始朝着玩家的位置
            if (distance < 2)//判断距离小于2，开始攻击
            {
                ChangeState(State.Attack, null);
            }
        }
    }

    public override void StateAttack()
    {
        if (target == null)
        {
            ChangeState(State.Patrol, () => { ani.SetTrigger("Walk"); });
        }
        else
        {
            if (isCanAttack)
            {
                int j = Random.Range(0, 2);//随机数判断采用哪个攻击动画
                agent.speed = 0f;//必须在每个动画设置速度，不然会保持前一个状态下的速度
                gameObject.transform.LookAt(target.transform.position);//朝向玩家攻击
                if (j == 0) ani.SetTrigger("Attack1");
                else ani.SetTrigger("Attack2");
                isCanAttack = false;

                target.GetComponent<Enemy2_FSM>().Demage();

            }
            else
            {
                CD -= Time.deltaTime;
                if (CD <= 0)
                {
                    isCanAttack = true;
                    CD = 2;
                }
            }
        }
    }
    public override void StateDamage()
    {
        if (HP > 0)
        {
            if (target == null)
            {
                ChangeState(State.Patrol, () => { ani.SetTrigger("Walk"); });
            }
            else
            {
                if (distance <= 5)
                {
                    if (distance <= 2)
                    {
                        ChangeState(State.Attack, null);
                    }
                    else
                    {
                        ChangeState(State.Trace, () => { ani.SetTrigger("Walk"); });
                    }
                }
                else
                {
                    ChangeState(State.Patrol, () => { ani.SetTrigger("Walk"); });
                }
            }
        }
        else
        {
            ChangeState(State.Dead, () => { ani.Play("Die");/* ani.SetTrigger("Die");*/ });
        }
    }

    public override void StateDead()
    {
        ani.SetTrigger("Die");
        target.GetComponent<Enemy2_FSM>().target = null;

        this.enabled = false;
    }
}


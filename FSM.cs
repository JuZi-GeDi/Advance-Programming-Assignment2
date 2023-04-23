using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
//枚举出5种状态下的类型
public enum State
{
    Idle, Patrol, Trace, Attack, Damage, Dead,
}

public class FSM : MonoBehaviour//FSM框架
{
    public State CurrentState;//当前框架
    public Transform target;//获取玩家位置信息
    public float distance;//判断AI敌人自身与玩家之间的距离
    public float HP;
    public virtual void Start()
    {
        HP = 5;
    }

    public virtual void Update()
    {
        if (target!=null)
        {
            //位置信息需要实时获取，所以放在update中
            distance = Vector3.Distance(target.transform.position, transform.position);
        }
      
        switch (CurrentState)//switch的用法就是当前状态是哪个就执行状态下的方法。
        {//注意：因为switch是放在update状态下的所以一定要明白方法会不断地调用~
            case State.Idle:
                StateIdle();
                break;
            case State.Patrol:
                StatePatrol();
                break;
            case State.Trace:
                StateTrace();
                break;
            case State.Attack:
                StateAttack();
                break;
            case State.Damage:
                StateDamage();
                break;
            case State.Dead:
                StateDead();
                break;
        }
    }

    public void ChangeState(State newState, UnityEngine.Events.UnityAction action)
    {
        CurrentState = newState;
        action?.Invoke();
    }
    //注：接下来所有的方法都添加virtual，因为需要根据继承对象的不同需求定制状态内的方法
    public virtual void StateIdle()//站立状态
    {

    }
    public virtual void StatePatrol()//巡逻状态
    {

    }
    public virtual void StateTrace()//行走状态
    {

    }

    public virtual void StateAttack()//攻击状态
    {

    }

    public virtual void StateDamage()//受伤状态
    {

    }

    public virtual void StateDead()//死亡状态
    {

    }
}

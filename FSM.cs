using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
//ö�ٳ�5��״̬�µ�����
public enum State
{
    Idle, Patrol, Trace, Attack, Damage, Dead,
}

public class FSM : MonoBehaviour//FSM���
{
    public State CurrentState;//��ǰ���
    public Transform target;//��ȡ���λ����Ϣ
    public float distance;//�ж�AI�������������֮��ľ���
    public float HP;
    public virtual void Start()
    {
        HP = 5;
    }

    public virtual void Update()
    {
        if (target!=null)
        {
            //λ����Ϣ��Ҫʵʱ��ȡ�����Է���update��
            distance = Vector3.Distance(target.transform.position, transform.position);
        }
      
        switch (CurrentState)//switch���÷����ǵ�ǰ״̬���ĸ���ִ��״̬�µķ�����
        {//ע�⣺��Ϊswitch�Ƿ���update״̬�µ�����һ��Ҫ���׷����᲻�ϵص���~
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
    //ע�����������еķ��������virtual����Ϊ��Ҫ���ݼ̳ж���Ĳ�ͬ������״̬�ڵķ���
    public virtual void StateIdle()//վ��״̬
    {

    }
    public virtual void StatePatrol()//Ѳ��״̬
    {

    }
    public virtual void StateTrace()//����״̬
    {

    }

    public virtual void StateAttack()//����״̬
    {

    }

    public virtual void StateDamage()//����״̬
    {

    }

    public virtual void StateDead()//����״̬
    {

    }
}

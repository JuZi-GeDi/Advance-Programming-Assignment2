using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class Friend_FSM : FSM
{
    public GameObject[] Target;//������ŵ���Ѳ��ʱ��λ�����꣬һ����������
    private Animator ani;//���Է���FSM���棬���������һ�ûȷ�����������Ķ����ǲ��Ƕ���5�֡�
    private NavMeshAgent agent;//��ͨ��NavMesh����
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
        //����ת��׷����ҵ��߼����жϾ���
        //if (distance < 5)
        //{
        //    ChangeState(State.Trace, () => { ani.SetTrigger("Walk"); });
        //}
    }
    public override void StatePatrol()
    {
        agent.speed = 2.0f;//AI�����ٶ�Ϊ2.0F��һ��ÿ��״̬�¶���Ҫ��д��ͬ���ٶȡ�
        time -= Time.deltaTime;
        if (time <= 0)
        {
            time = 2f;
            i = Random.Range(0, Target.Length);//��ʱ�����ٲ�ͬ��Ѳ�ߵ�֮��������ѡ����Ѳ��
        }
        agent.SetDestination(Target[i].transform.position);//����Ŀ���ƶ�
        //����ת��׷����ҵ��߼����жϾ���
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
            agent.speed = 3f;//׷��״̬���ٶ�����3f
            agent.SetDestination(target.transform.position);//���˿�ʼ������ҵ�λ��
            if (distance < 2)//�жϾ���С��2����ʼ����
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
                int j = Random.Range(0, 2);//������жϲ����ĸ���������
                agent.speed = 0f;//������ÿ�����������ٶȣ���Ȼ�ᱣ��ǰһ��״̬�µ��ٶ�
                gameObject.transform.LookAt(target.transform.position);//������ҹ���
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


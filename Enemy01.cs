using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

//״̬ö��
public enum EnemyType
{
    Idle,
    Move,
    Attack,
    Die
}

public class Enemy01 : MonoBehaviour
{
    public Animator ani;
    //public Rigidbody rig;
    public NavMeshAgent agent;
    public StateMachine<Enemy01> machine;//״̬��
    public Dictionary<EnemyType, _State<Enemy01>> stateDic;//�洢״̬�ֵ�
    private Transform player;
    public Transform[] paths;
    public int HP;
    public GameObject[] Bodies;
    public Material Death;
    public TriggerController controller;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player").transform;
        ani = GetComponent<Animator>();
        //rig = GetComponent<Rigidbody>();
        agent = GetComponent<NavMeshAgent>();
        //�����ֵ�
        stateDic = new Dictionary<EnemyType, _State<Enemy01>>();
        stateDic.Add(EnemyType.Idle, new Enemy01_Idle());
        stateDic.Add(EnemyType.Move, new Enemy01_Move());
        stateDic.Add(EnemyType.Attack, new Enemy01_Attack());
        stateDic.Add(EnemyType.Die, new Enemy01_Die());
        //����״̬��
        machine = new StateMachine<Enemy01>(this);
        //����Ĭ��״̬
        machine.SetCurrent(stateDic[EnemyType.Idle]);
    }

    public void ChageState(EnemyType type)
    {
        if (stateDic.ContainsKey(type))
        {
            machine.ChageState(stateDic[type]);
        }
    }

    // Update is called once per frame
    void Update()
    {
        machine.OnUpdate();
    }

    private int index = 0;
    public void Move()
    {
        if(Vector3.Distance(gameObject.transform.position, player.position) <= 20 && Vector3.Distance(gameObject.transform.position, player.position) > 10)
        {
            ChageState(EnemyType.Move);
            agent.speed = 5.0f;//׷��״̬���ٶ�����5.0f
            agent.stoppingDistance = 4.5f;
            agent.SetDestination(player.position);//���˿�ʼ������ҵ�λ�� 
        }
        else if(Vector3.Distance(gameObject.transform.position, player.position) <= 10)
        {
            AttackPlayer();
            agent.speed = 0;
        }
        else if(Vector3.Distance(gameObject.transform.position, player.position) > 20)
        {

            agent.speed = 3.0f;//׷��״̬���ٶ�����5.0f
            agent.SetDestination(paths[index].position);//���˿�ʼ����Ѳ�ߵ�λ��
            agent.stoppingDistance = 0;
            if (Vector3.Distance(gameObject.transform.position, paths[index].position) <= 0.1f)
            {
                ChageState(EnemyType.Idle);
                index++;
                index %= paths.Length;
            }
        }
    }

    public void GetHit()
    {
        //����Ѿ�������״̬ ��������
        if(machine.currentState is Enemy01_Die)
        {
            return;
        }
        if (HP <= 0)
        {
            //�л�����״̬
            ChageState(EnemyType.Die);
            StartCoroutine("Wait");
        }
    }

    IEnumerator Wait()
    {
        yield return new WaitForSeconds(4);
        controller.EnemyNumber--;
        Destroy(gameObject);
    }

    public void AttackPlayer()
    {
        ChageState(EnemyType.Attack);
    }

    public void FaceToPlayer()
    {
        gameObject.transform.LookAt(player.position);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.tag == "Bullet")
        {
            HP -= 100;
            GetHit();
        }
    }
}

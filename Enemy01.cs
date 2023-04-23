using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

//状态枚举
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
    public StateMachine<Enemy01> machine;//状态基
    public Dictionary<EnemyType, _State<Enemy01>> stateDic;//存储状态字典
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
        //创建字典
        stateDic = new Dictionary<EnemyType, _State<Enemy01>>();
        stateDic.Add(EnemyType.Idle, new Enemy01_Idle());
        stateDic.Add(EnemyType.Move, new Enemy01_Move());
        stateDic.Add(EnemyType.Attack, new Enemy01_Attack());
        stateDic.Add(EnemyType.Die, new Enemy01_Die());
        //创建状态机
        machine = new StateMachine<Enemy01>(this);
        //设置默认状态
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
            agent.speed = 5.0f;//追击状态下速度升到5.0f
            agent.stoppingDistance = 4.5f;
            agent.SetDestination(player.position);//敌人开始朝着玩家的位置 
        }
        else if(Vector3.Distance(gameObject.transform.position, player.position) <= 10)
        {
            AttackPlayer();
            agent.speed = 0;
        }
        else if(Vector3.Distance(gameObject.transform.position, player.position) > 20)
        {

            agent.speed = 3.0f;//追击状态下速度升到5.0f
            agent.SetDestination(paths[index].position);//敌人开始朝着巡逻的位置
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
        //如果已经是死亡状态 不做处理
        if(machine.currentState is Enemy01_Die)
        {
            return;
        }
        if (HP <= 0)
        {
            //切换死亡状态
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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

//状态枚举
public enum BossType
{
    Idle,
    Run,
    Shout,
    Attack,
    Die
}

public class Enemy02 : MonoBehaviour
{
    public Animator ani;
    //public Rigidbody rig;
    public NavMeshAgent agent;
    public StateMachine<Enemy02> machine;//状态基
    public Dictionary<BossType, _State<Enemy02>> stateDic;//存储状态字典
    private Transform player;
    public int HP;
    public GameObject[] Bodies;
    public Material Death;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player").transform;
        ani = GetComponent<Animator>();
        //rig = GetComponent<Rigidbody>();
        agent = GetComponent<NavMeshAgent>();
        //创建字典
        stateDic = new Dictionary<BossType, _State<Enemy02>>();
        stateDic.Add(BossType.Idle, new Enemy02_Idle());
        stateDic.Add(BossType.Run, new Enemy02_Move());
        stateDic.Add(BossType.Shout, new Enemy02_Shout());
        stateDic.Add(BossType.Attack, new Enemy02_Attack());
        stateDic.Add(BossType.Die, new Enemy02_Die());
        //创建状态机
        machine = new StateMachine<Enemy02>(this);
        //设置默认状态
        machine.SetCurrent(stateDic[BossType.Idle]);
    }

    public void ChageState(BossType type)
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

    public bool CanFindPlayer()
    {
        if (Vector3.Distance(gameObject.transform.position, player.position) <= 50 && Vector3.Distance(gameObject.transform.position, player.position) > 8)
        {
            gameObject.transform.LookAt(player.position);
            return true;
        }
        return false;
    }

    public bool CanShoutPlayer()
    {
        if (Vector3.Distance(gameObject.transform.position, player.position) <= 8)
        {
            //AttackPlayer();
            agent.speed = 0;
            return true;
        }
        return false;
    }

    public void Move()
    {
        if (Vector3.Distance(gameObject.transform.position, player.position) <= 50 && Vector3.Distance(gameObject.transform.position, player.position) > 8)
        {
            ChageState(BossType.Run);
            agent.speed = 10.0f;//追击状态下速度升到5.0f
            agent.SetDestination(player.position);//敌人开始朝着玩家的位置 
        }
        else
        {
            agent.speed = 0;
            ChageState(BossType.Idle);
        }
    }

    public void GetHit()
    {
        //如果已经是死亡状态 不做处理
        if (machine.currentState is Enemy02_Die)
        {
            return;
        }
        else
        {
            //切换死亡状态
            ChageState(BossType.Die);
        }
    }

    IEnumerator Wait()
    {
        yield return new WaitForSeconds(4.5f);
        Destroy(gameObject);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.tag == "Bullet")
        {
            HP -= 500;
            GetHit();
            if (HP <= 0)
            {
                StartCoroutine("Wait");
            }
        }
    }
}

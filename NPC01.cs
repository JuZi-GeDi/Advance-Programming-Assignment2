using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

public enum NPCType
{
    Move,
    Idle,
}

public class NPC01 : MonoBehaviour
{
    public StateMachine<NPC01> machine;//״̬��
    public Dictionary<NPCType, _State<NPC01>> stateDic;//�洢״̬�ֵ�
    private Transform player;
    public Transform[] Path;
    public Transform[] Path2;
    public NavMeshAgent agent;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player").transform;
        //�����ֵ�
        stateDic = new Dictionary<NPCType, _State<NPC01>>();
        stateDic.Add(NPCType.Move, new NPC01_Move());
        stateDic.Add(NPCType.Idle, new NPC_Idle());
        //����״̬��
        machine = new StateMachine<NPC01>(this);
        //����Ĭ��״̬
        machine.SetCurrent(stateDic[NPCType.Move]);
    }

    public void ChageState(NPCType type)
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
        
        if (Vector3.Distance(gameObject.transform.position, player.position) <= 2)
        {
            //Debug.Log(1);
            if (Input.GetKeyDown(KeyCode.E))
            {
                //Debug.Log(2);
                Destroy(gameObject);
            }
        }
    }

    public void Change()
    {
        ChageState(NPCType.Move);
    }

    public int index;
    private bool isFind;
    public void Move()
    {
        if(Vector3.Distance(gameObject.transform.position, player.position) >= 10)
        {
            if (!isFind)
            {
                agent.speed = 7.0f;
                agent.SetDestination(Path[index].position);//���˿�ʼ����Ѳ�ߵ�λ��
                agent.stoppingDistance = 0;
                if (Vector3.Distance(gameObject.transform.position, Path[index].position) <= 0.1f)
                {
                    ChageState(NPCType.Idle);
                }
            }
        }
        else if(Vector3.Distance(gameObject.transform.position, player.position) < 5 || isFind)
        {
            isFind = true;
            agent.speed = 15.0f;
            agent.SetDestination(Path2[index].position);//���˿�ʼ�������ܵ�λ��
            agent.stoppingDistance = 0;
            if (Vector3.Distance(gameObject.transform.position, Path2[index].position) <= 0.1f)
            {
                //Debug.Log(3);
                Destroy(gameObject);
            }
        }
    }
}

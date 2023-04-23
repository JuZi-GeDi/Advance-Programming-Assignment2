using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMachine<T>
{
    private T target;//״̬ӵ����
    private _State<T> preState;//��һ��״̬
    public _State<T> currentState;//��ǰ״̬
    public StateMachine(T target)
    {
        this.target = target;
        preState = null;
        currentState = null;
    }
    //����Ĭ��״̬
    public void SetCurrent(_State<T> current)
    {
        this.currentState = current;
        //����״̬
        this.currentState.Enter(target);
    }
    //�ı�״̬
    public void ChageState(_State<T> current)
    {
        //�˳���ǰ״̬
        this.currentState.Exit(target);
        //����ǰ״̬��¼
        this.preState = this.currentState;
        //�����µ�״̬
        this.currentState = current;
        //����
        this.currentState.Enter(target);
    }

    public void OnUpdate()
    {
        if (this.currentState != null)
        {
            this.currentState.Execute(target);
        }
    }
}

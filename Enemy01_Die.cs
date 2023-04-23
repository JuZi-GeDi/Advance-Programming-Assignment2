using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//ËÀÍö×´Ì¬
public class Enemy01_Die : _State<Enemy01>
{
    float temp;
    public override void Enter(Enemy01 target)
    {
        temp = -2;
        target.ani.SetBool("Die", true);
        for (int i = 0; i < target.Bodies.Length; i++)
        {
            target.Bodies[i].GetComponent<SkinnedMeshRenderer>().material = target.Death;
        }
    }
    
    public override void Execute(Enemy01 target)
    {
        temp += Time.deltaTime * 3;
        target.GetComponent<BoxCollider>().enabled = false;
        for (int i = 0; i < target.Bodies.Length; i++)
        {
            target.Bodies[i].GetComponent<SkinnedMeshRenderer>().materials[0].SetFloat("_Clipthreshold", temp);
        }
            
    }

    public override void Exit(Enemy01 target)
    {
        
    }

}

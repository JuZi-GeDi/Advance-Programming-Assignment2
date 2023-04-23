using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Tree;
using UnityEngine;

public class TriggerController : MonoBehaviour
{
    public GameObject[] EnemyRoot;
    public int EnemyNumber;
    public GameObject[] AirWallRoot;
    // Start is called before the first frame update
    void Start()
    {
        EnemyNumber = EnemyRoot.Length;
    }

    // Update is called once per frame
    void Update()
    {
        if (EnemyNumber == 0)
        {
            for (int i = 0; i < AirWallRoot.Length; i++)
            {
                AirWallRoot[i].GetComponent<BoxCollider>().isTrigger = true;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if(EnemyRoot.Length != 0)
            {
                for (int i = 0; i < EnemyRoot.Length; i++)
                {
                    EnemyRoot[i].SetActive(true);
                }
                StartCoroutine("Wait");
            }
        }
    }

    IEnumerator Wait()
    {
        yield return new WaitForSeconds(1);
        for (int i = 0; i < AirWallRoot.Length; i++)
        {
            AirWallRoot[i].GetComponent<BoxCollider>().isTrigger = false;
        }
        
    }
}

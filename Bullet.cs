using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public Rigidbody RB;
    [Range(0, 100)]
    public float Speed = 10; // m/s
    [Header("Impact")]
    [Range(0, 100)]
    public float Force = 10;
    public float liveTime;
    // Start is called before the first frame update
    void Start()
    {
        RB.velocity = transform.forward * Speed;
    }

    // Update is called once per frame
    void Update()
    {
        liveTime += Time.deltaTime;
        if (liveTime >= 5)
        {
            Destroy(gameObject);
            liveTime = 0;
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        // Add explosive force to objects (if they have a rigidbody)
        Rigidbody rigidbody = collision.collider.GetComponent<Rigidbody>();
        if (rigidbody != null)
        {
            rigidbody.AddExplosionForce(Force, transform.position, 0.25f);
        }
        Destroy(gameObject);
    }
}

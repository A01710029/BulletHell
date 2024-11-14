using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    public float speed = 10f;
    public float launch = 20f;
    private Rigidbody rb; 
    
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        LaunchBullet();
        Destroy(gameObject, 2.5f);
    }

     void LaunchBullet()
    {
        // Apply force in the forward direction
        rb.AddForce(transform.forward * launch, ForceMode.Impulse);
    }

    void OnDestroy()
    {
        Debug.Log("Bullet Destroyed");
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletSpawner : MonoBehaviour
{
    public GameObject bullet; // Modelo de la bala
    public float interval = 0.5f; // Cada cuando se crean balas
    private float timer = 0f;
    
    // Start is called before the first frame update
    void Start()
    {
        timer = interval;
    }

    // Update is called once per frame
    void Update()
    {
        timer -= Time.deltaTime;

        if (timer <= 0f)
        {
            if (Time.time >= timer)
            {
                spawnBullet();
                timer = interval;
            }
        }
    }

    void spawnBullet()
    {
        Instantiate(bullet, transform.position, Quaternion.identity);
    }
}

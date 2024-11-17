using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletSpawner : MonoBehaviour
{
    public GameObject bullet; // Modelo de la bala
    public float interval = 0.5f; // Cada cuando se crean balas
    private float timer = 0f;

    public float rotationSpeed = 30f; // Cuántos grados se mueve
    public float rotationRange = 60f; // Ángulo máximo de la rotación
    private float currentRotation = 0f;
    private bool rotatingClockwise = true;
    private bool isRotating = false;
    
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
                SpawnSprinklerShot();
                timer = interval;
            }
        }

        if(isRotating)
        {
            RotateLauncher();
        }
    }

    void SpawnStraightShot()
    {
        Instantiate(bullet, transform.position, transform.rotation);
    }

    void SpawnFanShot()
    {
        for (int i = -5; i <= 5; i++)
        {
            GameObject fanBullet = Instantiate(bullet, transform.position, Quaternion.identity);
            fanBullet.transform.rotation = Quaternion.Euler(0, i * 60, 0); 
        }
    }

    void SpawnWaveShot()
    {
        GameObject waveBullet = Instantiate(bullet, transform.position, Quaternion.identity);
        float waveFrequency = 10f; 
        waveBullet.GetComponent<BulletController>().SetWaveMovement(waveFrequency);
    }

    void SpawnSprinklerShot()
    {
        isRotating = true;

        for (int i = -1; i <= 1; i++)
        {
            GameObject sprinklerBullet = Instantiate(bullet, transform.position, transform.rotation); 
        }
    }

    public void StopSprinklerShot()
    {
        isRotating = false;
        transform.parent.localRotation = Quaternion.Euler(0f, 0f, 0f); // Resetear posición
    }

    void RotateLauncher()
    {
        // Calcular dirección de rotación (para ciclo)
        float rotationStep = rotationSpeed * Time.deltaTime;
        if (rotatingClockwise)
        {
            currentRotation += rotationStep;
            if (currentRotation >= rotationRange)
            {
                rotatingClockwise = false;
            }
        }
        else
        {
            currentRotation -= rotationStep;
            if (currentRotation <= -rotationRange)
            {
                rotatingClockwise = true;
            }
        }

        // Rotar objeto padre (Tennis Machine)
        transform.parent.localRotation = Quaternion.Euler(0f, currentRotation, 0f);
    }
}

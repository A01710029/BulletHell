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

    // Para manejar cambios de patrón
    public delegate void BulletPattern();
    public BulletPattern[] bulletPatterns;
    private int currentPattern = 0; // Representar patrones como lista con index
    
    // Start is called before the first frame update
    void Start()
    {
        timer = interval;

        bulletPatterns = new BulletPattern[]
        {
            SpawnStraightShot,
            SpawnFanShot,
            SpawnWaveShot,
            SpawnSprinklerShot,
            SpawnSpinStraightShot,
            SpawnSpinFanShot
        };
    }

    // Update is called once per frame
    void Update()
    {
        timer -= Time.deltaTime;

        if (timer <= 0f)
        {
            if (Time.time >= timer)
            {
                bulletPatterns[currentPattern]?.Invoke();
                timer = interval;
            }
        }

        if(isRotating)
        {
            RotateLauncher();
        } else {
            transform.parent.localRotation = Quaternion.Euler(0f, 0f, 0f); // Resetear posición
        }
    }

    public void SwitchPattern()
    {
        int previousPattern = currentPattern;

        // Seleccionar patrón aleatorio sin repetir el anterior
        do
        {
            currentPattern = Random.Range(1, bulletPatterns.Length);
        } while (currentPattern == previousPattern);

        Debug.Log($"Cambio al patrón {currentPattern}");
    }

    void SpawnStraightShot()
    {
        isRotating = false;

        Instantiate(bullet, transform.position, transform.rotation);
    }

    void SpawnFanShot()
    {
        isRotating = false;
        
        for (int i = -5; i <= 5; i++)
        {
            GameObject fanBullet = Instantiate(bullet, transform.position, transform.rotation);
        }
    }

    void SpawnWaveShot()
    { 
        isRotating = true;

        for (int i = -3; i <= 3; i++)
        {
            GameObject waveBullet = Instantiate(bullet, transform.position, transform.rotation);
            float waveFrequency = 10f; 
            waveBullet.GetComponent<BulletController>().SetWaveMovement(waveFrequency);
        }
    }

    void SpawnSprinklerShot()
    {
        isRotating = true;

        for (int i = -1; i <= 1; i++)
        {
            GameObject sprinklerBullet = Instantiate(bullet, transform.position, transform.rotation); 
        }
    }

    void SpawnSpinStraightShot()
    {
        isRotating = true;

        Instantiate(bullet, transform.position, transform.rotation);
    }

    void SpawnSpinFanShot()
    {
        isRotating = true;
        
        for (int i = -5; i <= 5; i++)
        {
            GameObject fanBullet = Instantiate(bullet, transform.position, transform.rotation);
        }
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

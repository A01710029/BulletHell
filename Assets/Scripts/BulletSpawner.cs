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
    private bool isWaveLeft = true;
    private bool isTilted = false;

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
            SpawnStraightShot, //0
            SpawnFanShot, // 1
            SpawnWaveShot, // 2
            SpawnSprinklerShot, // 3
            SpawnSpinStraightShot, // 4
            SpawnSpinFanShot // 5
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
        } 
       

        if(isTilted)
        {
            TiltLauncher();
        } 

        if(!isRotating & !isTilted)
        {
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
        isTilted = false;

        Instantiate(bullet, transform.position, transform.rotation);
    }

    void SpawnFanShot()
    {
        isRotating = false;
        isTilted = false;

        float angleStep = 10f; // Ajustar ángulo entre las balas
        float leftAngle = isWaveLeft ? -5 * angleStep : 5 * angleStep; // Ángulo izquierdo
        float rightAngle = isWaveLeft ? 5 * angleStep : -5 * angleStep; // Ángulo derecho
        
        for (int i = -5; i <= 5; i++)
        {
            float angle = leftAngle + (i * angleStep);
            
             Quaternion bulletRotation = Quaternion.Euler(0, angle, 0) * transform.rotation;

             GameObject fanBullet = Instantiate(bullet, transform.position, bulletRotation);
        }

        isWaveLeft = !isWaveLeft;
    }

    void SpawnWaveShot()
    { 
        isRotating = false;
        isTilted = true;

        for (int i = -3; i <= 3; i++)
        {
            GameObject waveBullet = Instantiate(bullet, transform.position, transform.rotation);
            float waveFrequency = 10f; 
            waveBullet.GetComponent<BulletController>().SetWaveMovement(waveFrequency);
        }
    }

    void SpawnSprinklerShot()
    {
        isRotating = false;
        isTilted = true;

        for (int i = -2; i <= 2; i++)
        {
            GameObject sprinklerBullet = Instantiate(bullet, transform.position, transform.rotation); 
        }
    }

    void SpawnSpinStraightShot()
    {
        isRotating = true;
        isTilted = false;

        Instantiate(bullet, transform.position, transform.rotation);
    }

    void SpawnSpinFanShot()
    {
        isRotating = true;
        isTilted = false;
        
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

    void TiltLauncher()
    {
        // Ajustar ángulo de lanzamiento (y)
        float verticalTilt = Mathf.Cos(Time.time * 3f) * 15f + 10f;
        
        // Rotar objeto padre (Tennis Machine)
        transform.parent.localRotation = Quaternion.Euler(verticalTilt, 0f, 0f);
    }
}

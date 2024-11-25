using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    public float speed = 10f;
    public float launch = 20f;
    private Rigidbody rb; 
    private float waveFrequency;
    private bool isWave = false;
    
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        LaunchBullet();
        Destroy(gameObject, 8f);
    }

    void LaunchBullet()
    {
        // Para que las balas sean "lanzadas" con física hacia adelante
        rb.AddForce(transform.forward * launch, ForceMode.Impulse);
    }

    public void SetWaveMovement(float frequency)
    {
        isWave = true;
        waveFrequency = frequency;
    }

    void Update()
    {
        if (isWave)
        {
            float waveMovement = Mathf.Sin(Time.time * waveFrequency) * 20f;

            // Definir movimiento hacia adelante 
            Vector3 currentVelocity = rb.velocity;
            
            // Agregar movimiento de ola al eje x
            currentVelocity.x = waveMovement;

            // Mantener el movimiento hacia enfrente
            currentVelocity.z = currentVelocity.z;

            // Aplicar a Rigidbody
            rb.velocity = currentVelocity;
        }
    }
}

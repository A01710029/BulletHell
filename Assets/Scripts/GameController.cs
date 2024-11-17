using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public BulletSpawner spawner;
    private float switchTimer = 0f;

    public float standardSwitchInterval = 10f;  // Cambiar cada 10 segundos
    public float startAttackInterval = 7f; // Primer ataque más corto
    private float currentSwitchInterval;
    private bool startAttack = true;

    void Start()
    {
        currentSwitchInterval = startAttackInterval; // Para inicio del juego
    }

    // Update is called once per frame
    void Update()
    {
        switchTimer += Time.deltaTime;

        if (switchTimer >= currentSwitchInterval)
        {
            switchTimer = 0f;
            spawner.SwitchPattern();

            if (startAttack)
            {
                startAttack = false;
                currentSwitchInterval = standardSwitchInterval; // Cambiar a tiempo para el resto
            }
        }
    }
}

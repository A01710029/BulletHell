using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public BulletSpawner spawner;
    public float switchInterval = 10f; // Cambiar cada 10 segundos
    private float switchTimer = 0f;

    // Update is called once per frame
    void Update()
    {
        switchTimer += Time.deltaTime;

        if (switchTimer >= switchInterval)
        {
            switchTimer = 0f;
            spawner.SwitchPattern();
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BullletCounter : MonoBehaviour
{
    public TextMeshProUGUI counter;
    public GameObject[] bullets;
    
    // Update is called once per frame
    void Update()
    {
        // Encontrar todos los objetos de pelota
        bullets = GameObject.FindGameObjectsWithTag("BulletBall");

        // Actualizar texto con número de pelotas
        counter.text = "Launcher Bullets: " + bullets.Length.ToString();
    }
}

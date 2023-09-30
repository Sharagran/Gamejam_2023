using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class PlatformMovement : MonoBehaviour
{
    public float speed = 3.0f; // Geschwindigkeit der Bewegung
    public float leftBound = -1.0f; // Linke Grenze
    public float rightBound = 0.0f; // Rechte Grenze

    private Vector3 start;
    private bool movingRight = true;

    private void Start()
    {
        start = transform.position;
    }

    private void Update()
    {
        // Bewege die Plattform entsprechend der aktuellen Richtung
        float direction = movingRight ? 1.0f : -1.0f;
        transform.Translate(Vector3.right * direction * speed * Time.deltaTime);

        // Überprüfe, ob die Plattform die Grenze erreicht hat, und ändere die Richtung
        if (movingRight && transform.position.x - start.x >= rightBound)
        {
            movingRight = false;
        }
        else if (!movingRight && transform.position.x + start.x <= leftBound)
        {
            movingRight = true;
        }
    }
}
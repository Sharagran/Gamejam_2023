using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovLeftRight : MonoBehaviour
{
    public float speed = 2.0f; // Geschwindigkeit der Bewegung
    public float leftBound = -5.0f; // Linke Grenze
    public float rightBound = 5.0f; // Rechte Grenze

    private bool movingRight = true;

    private void Update()
    {
        // Bewege die Plattform entsprechend der aktuellen Richtung
        float direction = movingRight ? 1.0f : -1.0f;
        transform.Translate(Vector3.right * direction * speed * Time.deltaTime);

        // Überprüfe, ob die Plattform die Grenze erreicht hat, und ändere die Richtung
        if (movingRight && transform.position.x >= rightBound)
        {
            movingRight = false;
        }
        else if (!movingRight && transform.position.x <= leftBound)
        {
            movingRight = true;
        }
    }
}
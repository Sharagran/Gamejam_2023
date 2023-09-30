using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class MovUpDown : MonoBehaviour
{
    public float moveSpeed = 2.0f; // Geschwindigkeit der Bewegung
    public float moveDistance = 5.0f; // Entfernung, um die sich die Plattform bewegen soll


    private Vector3 initialPosition;
    private Vector3 targetPosition;
    private bool movingUp = true;

private void Start()
{
    initialPosition = transform.position;
    targetPosition = initialPosition + new Vector3(0, moveDistance, 0);
}

private void Update()
{
    // Berechne die Richtung und die Entfernung zur Zielposition
    Vector3 direction = movingUp ? Vector3.up : Vector3.down;
    float distance = Vector3.Distance(transform.position, targetPosition);

        // Bewege die Plattform entsprechend der aktuellen Richtung und Geschwindigkeit
        if (distance > 0.01f)
        {
            transform.position += direction * moveSpeed * Time.deltaTime;
        }
        else
        {
            // Ändere die Richtung und aktualisiere die Zielposition
            movingUp = !movingUp;
            targetPosition = initialPosition + (movingUp ? Vector3.up : Vector3.down) * moveDistance;
        }
    }
}
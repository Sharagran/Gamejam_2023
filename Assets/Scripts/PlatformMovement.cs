using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class PlatformMovement : MonoBehaviour
{
    public enum Direction
    {
        Left,
        Right
    }
    public Direction startDirection;

    public float speed = 3.0f; // Geschwindigkeit der Bewegung
    public float leftBound = -1.0f; // Linke Grenze
    public float rightBound = 0.0f; // Rechte Grenze
    

    private Vector3 direction;
    private Vector3 startPos;
    

    private void Start()
    {
        startPos = transform.position;

        direction = startDirection == Direction.Left ? Vector3.left : Vector3.right;
    }

    private void Update()
    {
        // Bewege die Plattform
        transform.position += speed * Time.deltaTime * direction;

        // Wenn die Plattform die Grenze erreicht hat, drehe die Bewegungsrichtung um
        if (transform.position.x <= startPos.x + leftBound)
        {
            direction = Vector3.right;
        }
        else if (transform.position.x >= startPos.x + rightBound)
        {
            direction = Vector3.left;
        }
    }

    private void OnDrawGizmosSelected()
    {
        Vector3 start = Application.isPlaying ? startPos : transform.position;

        // Zeichne die Grenzen der Plattform
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(start + new Vector3(leftBound / 2, 0, 0), new Vector3(leftBound, 1f, 1f));
        Gizmos.DrawWireCube(start + new Vector3(rightBound / 2, 0, 0), new Vector3(rightBound, 1f, 1f));
    }
}
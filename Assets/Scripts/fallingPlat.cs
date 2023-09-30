using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fallingPlat : MonoBehaviour
{

    private bool isFalling = false;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && !isFalling)
        {
            isFalling = true;

            Rigidbody2D rb = GetComponent<Rigidbody2D>();
            rb.bodyType = RigidbodyType2D.Dynamic;

            // Optional: Du kannst hier die Geschwindigkeit oder andere physikalische Eigenschaften der Plattform anpassen
        }
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (isFalling && collider.CompareTag("Player"))
        {
            Destroy(gameObject);
        }
    }
}
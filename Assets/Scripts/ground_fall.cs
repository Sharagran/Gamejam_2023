using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingPlatform : MonoBehaviour
{
    private bool isPlayerStanding = false;
    private float standTime = 0f;
    public float fallDelay = 2f; // Zeit in Sekunden, nach der die Plattform fallen soll

    private Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.bodyType = RigidbodyType2D.Static; // Beginne mit einer statischen Rigidbody
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player")) // Prüfe, ob der Spieler die Plattform berührt
        {
            isPlayerStanding = true;
        }
    }

    private void Update()
    {
        if (isPlayerStanding)
        {
            standTime += Time.deltaTime;

            if (standTime >= fallDelay)
            {
                // Ändere den Rigidbody-Typ, um die Plattform fallen zu lassen
                rb.bodyType = RigidbodyType2D.Dynamic;

                // Starte den Fallprozess
                // Hier kannst du die Geschwindigkeit oder andere physikalische Eigenschaften der Plattform anpassen
            }
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            isPlayerStanding = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            // Zerstöre das Objekt, nachdem der Spieler es 2 Sekunden lang berührt hat
            if (standTime >= fallDelay)
            {
                Destroy(gameObject);
            }
        }
    }
}

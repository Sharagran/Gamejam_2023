using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoulderMovement : MonoBehaviour
{
    public float speed = 1f; // Geschwindigkeit
    public bool invertControls = false; // Invertiere die Steuerung

    private Rigidbody2D _rigidbody2D;

    // Start is called before the first frame update
    void Start()
    {
        //_rigidbody2D = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {

        float moveSpeed = speed * Time.deltaTime;
        float rotateSpeed = speed * Time.deltaTime * 360f;

        if (invertControls)
        {
            transform.position += Vector3.left * moveSpeed;
            // rotate
            transform.Rotate(Vector3.forward * rotateSpeed);
        }
        else
        {
            transform.position += Vector3.right * moveSpeed;
            // rotate
            transform.Rotate(Vector3.back * rotateSpeed);
        }
    }
}

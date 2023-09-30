using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoulderMovement : MonoBehaviour
{
    public float speed = 1f; // Geschwindigkeit
    public bool invertControls = false; // Invertiere die Steuerung

    public bool keepAddingForce = false; // Füge weiterhin Kraft hinzu
    public bool keepRotating = false; // Rotiere weiterhin

    public float currentSpeed = 0;

    public bool isRolling
    {
        get { return speed > 0; }
    }

    private Rigidbody2D _rigidbody2D;

    // Start is called before the first frame update
    void Start()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _rigidbody2D.AddForce(Vector2.right * speed * Time.deltaTime);
    }

    // Update is called once per frame
    void Update()
    {
        currentSpeed = _rigidbody2D.velocity.x;

        float moveSpeed = speed * Time.deltaTime;
        float rotateSpeed = speed * Time.deltaTime * 360f;

        Vector2 moveDirectionVector = invertControls ? Vector2.left : Vector2.right;
        Vector3 rotateDirectionVector = invertControls ? Vector3.forward: Vector3.back;

        if (keepAddingForce)
            _rigidbody2D.AddForce(moveDirectionVector * moveSpeed);

        if (keepRotating)
            transform.Rotate(rotateDirectionVector * rotateSpeed);
    }
}

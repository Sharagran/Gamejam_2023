using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f; // Geschwindigkeit der Bewegung
    public float jumpForce = 5f; // Sprungkraft
    public float groundCheckDistance = 0.9f; // Radius des Kreises, der den Boden checkt

    private Rigidbody _rb;
    private Animator _animator;
    private SpriteRenderer _sr;

    private bool _isGrounded;
    private Vector2 _movementInput;


    private void Start()
    {
        _rb = GetComponent<Rigidbody>();
        _animator = GetComponent<Animator>();
        _sr = GetComponent<SpriteRenderer>();
    }

    private void FixedUpdate()
    {
        _rb.velocity = _movementInput * moveSpeed;
    }

    private void OnMove(InputValue value)
    {
        // Überprüfe, ob der Charakter den Boden berührt
        RaycastHit hit;
        Vector3 castOrigin = transform.position + new Vector3(0, 0.5f, 0);
        _isGrounded = Physics.Raycast(castOrigin, Vector3.down, out hit, groundCheckDistance, LayerMask.GetMask("Terrain"));

        Vector2 input = value.Get<Vector2>();

        // springen
        if (input.y > 0.5f && _isGrounded)
        {
            // _rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            Debug.Log("Jump");
            _animator.SetBool("IsJumping", true);
        }
        else if (!_isGrounded)
        {
            _animator.SetBool("IsJumping", false);
        }

        _movementInput = input;

        Debug.Log("IsGrounded: " + _isGrounded);
    }

    private void Update()
    {
        // turn player in right direction
        if (_movementInput.x > 0)
            _sr.flipX = true;
        else if (_movementInput.x < 0)
            _sr.flipX = false;

        // Animationen
        _animator.SetFloat("Speed", Mathf.Abs(_movementInput.x));


        // spacebar
/*        if (Input.GetKey(KeyCode.Space))
            _animator.SetBool("IsJumping", true);
        else 
            _animator.SetBool("IsJumping", false);*/
        //_animator.SetBool("IsGrounded", _isGrounded);
    }
}

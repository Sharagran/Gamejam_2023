using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [Header("Controls")]
    public float moveSpeed = 5f; // Geschwindigkeit der Bewegung
    public float jumpForce = 5f; // Sprungkraft
    public bool invertControls = false; // Invertiere die Steuerung

    [Header("Logic")]
    public float groundCheckDistance = 0.9f; // Radius des Kreises, der den Boden checkt

    private Rigidbody2D _rb;
    private Animator _animator;
    private SpriteRenderer _sr;

    private bool _isGrounded;
    private Vector2 _movementInput;

    private Vector2 _smoothedMovementInput;
    private Vector2 _smoothedMovementVelocity;


    private void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        _sr = GetComponent<SpriteRenderer>();
    }

    private void FixedUpdate()
    {
        _smoothedMovementInput = Vector2.SmoothDamp(_smoothedMovementInput, _movementInput, ref _smoothedMovementVelocity, 0.1f);

        //_rb.velocity = _smoothedMovementInput * moveSpeed;
        // apply x movement
        _rb.velocity = new Vector2(_smoothedMovementInput.x * moveSpeed, _rb.velocity.y);
    }

    private void OnMove(InputValue value)
    {
        // Überprüfe, ob der Charakter den Boden berührt
        RaycastHit hit;
        Vector3 castOrigin = transform.position + new Vector3(0, 0.5f, 0);
        _isGrounded = Physics.Raycast(castOrigin, Vector3.down, out hit, groundCheckDistance, LayerMask.GetMask("Terrain"));

        Vector2 input = value.Get<Vector2>();
        if(invertControls)
            input *= -1;
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
        _animator.SetFloat("SpeedX", Mathf.Abs(_movementInput.x));
        _animator.SetFloat("SpeedY", _rb.velocity.y);

        // jump
        if (Input.GetButtonDown("Jump"))
        {
            _rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            // triger jump animation
            _animator.SetTrigger("Jump");
            //_movementInput.y = jumpForce;
        }
    }
}

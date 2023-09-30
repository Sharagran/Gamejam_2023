using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [Header("Controls")]
    public float moveSpeed = 5f; // Geschwindigkeit der Bewegung
    public float jumpForce = 5f; // Sprungkraft
    public bool invertControls = false; // Invertiere die Steuerung

    [Header("Logic")]
    public float groundCheckDistance = 0.1f; // Radius des Kreises, der den Boden checkt

    [Header("Debug options")]
    public bool drawGroundCheck = false;

    private Rigidbody2D _rb;
    private Animator _animator;
    private SpriteRenderer _sr;
    private BoxCollider2D _col;

    private bool _isGrounded;
    private Vector2 _movementInput;

    private Vector2 _smoothedMovementInput;
    private Vector2 _smoothedMovementVelocity;


    private void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        _sr = GetComponent<SpriteRenderer>();
        _col = GetComponent<BoxCollider2D>();
    }

    private void FixedUpdate()
    {
        _smoothedMovementInput = Vector2.SmoothDamp(_smoothedMovementInput, _movementInput, ref _smoothedMovementVelocity, 0.1f);
        // apply x movement
        _rb.velocity = new Vector2(_smoothedMovementInput.x * moveSpeed, _rb.velocity.y);
    }

    private void OnMove(InputValue value)
    {
        Vector2 input = value.Get<Vector2>();
        if(invertControls)
            input *= -1;
        _movementInput = input;
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

        _isGrounded = Physics2D.BoxCast(_col.bounds.center, _col.bounds.size, 0, Vector2.down, groundCheckDistance, LayerMask.GetMask("Terrain"));

        // jump
        if (Input.GetButtonDown("Jump") && _isGrounded)
        {
            _rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            // triger jump animation
            _animator.SetTrigger("Jump");
        }


        //SlopeMovement();
    }

    private void SlopeMovement()
    {
        // make player able to walk up small slopes
        if (_isGrounded)
        {
            _rb.gravityScale = 1;
            _rb.drag = 5;
        }
        else
        {
            _rb.gravityScale = 3;
            _rb.drag = 0;
        }
    }

    private void OnDrawGizmos()
    {
        if (drawGroundCheck)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireCube(_col.bounds.center + new Vector3(0, -_col.bounds.extents.y, 0), _col.bounds.size);
        }
    }
}

using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Windows;

public class PlayerMovement : MonoBehaviour
{
    [Header("Controls")]
    public float moveSpeed = 5f; // Geschwindigkeit der Bewegung
    public float jumpForce = 5f; // Sprungkraft
    public bool invertControls = false; // Invertiere die Steuerung
    public float _jumpCooldown = 0.5f;

    [Header("Logic")]
    public float groundCheckDistance = 0.1f; // Radius des Kreises, der den Boden checkt

    [Header("Debug options")]
    public bool drawGroundCheck = false;
    public bool jumpCooldownIndicator = false;


    private bool _isGrounded;
    private bool _isJumping;

    // jump cooldown
    private bool _canJump = true;
    private void ResetJump() => _canJump = true;

    private Rigidbody2D _rb;
    private Animator _animator;
    private SpriteRenderer _sr;
    private BoxCollider2D _col;
    private Inverter _inverter;

    public bool gravityInverted
    {
        get { return _inverter.gravityInverted; }
    }

    private Vector2 _movementInput;

    private Vector2 _smoothedMovementInput;
    private Vector2 _smoothedMovementVelocity;


    private void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        _sr = GetComponent<SpriteRenderer>();
        _col = GetComponent<BoxCollider2D>();
        _inverter = GameObject.Find("GameController").GetComponent<Inverter>();
    }

    private void FixedUpdate()
    {
        if (invertControls)
            _movementInput *= -1;

        _smoothedMovementInput = Vector2.SmoothDamp(_smoothedMovementInput, _movementInput, ref _smoothedMovementVelocity, 0.1f);
        // apply x movement
        _rb.velocity = new Vector2(_smoothedMovementInput.x * moveSpeed, _rb.velocity.y);
    }

    private void OnMove(InputValue value)
    {
        Vector2 input = value.Get<Vector2>();
        _movementInput = input;
    }

    private void Update()
    {
        // turn player in right direction
        if (_movementInput.x > 0)
            _sr.flipX = true ^ gravityInverted;
        else if (_movementInput.x < 0)
            _sr.flipX = false ^ gravityInverted;

        // Animationen
        _animator.SetFloat("SpeedX", Mathf.Abs(_movementInput.x));
        _animator.SetFloat("SpeedY", _rb.velocity.y);

        _isGrounded = Physics2D.BoxCast(_col.bounds.center, _col.bounds.size, 0, gravityInverted ? Vector2.up : Vector2.down, groundCheckDistance, LayerMask.GetMask("Terrain"));
        _animator.SetBool("IsGrounded", _isGrounded);

        // jump
        if (UnityEngine.Input.GetButtonDown("Jump") && _isGrounded && _canJump)
        {
            OnJumpInputDown(jumpForce);

            // set jump cooldown
            _canJump = false;
            Invoke(nameof(ResetJump), _jumpCooldown);

        }


        //SlopeMovement();
    }

    public void OnJumpInputDown(float force)
    {
        _rb.AddForce(gravityInverted ? Vector2.down : Vector2.up * force, ForceMode2D.Impulse);
        // triger jump animation
        _animator.SetTrigger("Jump");

        AudioManager.instance.Play("jump");
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
        // only execute if game is running
        if (!Application.isPlaying)
            return;

        if (drawGroundCheck)
        {
            Gizmos.color = _isGrounded ? Color.green : Color.red;
            Gizmos.DrawWireCube(_col.bounds.center + new Vector3(0, gravityInverted ? groundCheckDistance : -groundCheckDistance, 0), _col.bounds.size);
        }

        if (jumpCooldownIndicator)
        {
            Gizmos.color = _canJump ? Color.green : Color.red;
            Gizmos.DrawSphere(_sr.bounds.center + new Vector3(0, 2f, 0), 0.25f);
        }
    }
}

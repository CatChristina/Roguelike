using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody rb;
    private Transform camReference;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        camReference = Camera.main.transform;

        _moveSpeed = _baseMoveSpeed;
    }

    private float horiz;
    private float vert;

    private void Update()
    {
        MovePlayer();
        Jump();
    }

    private void FixedUpdate()
    {
        GroundCheck();
    }

    [Header("Movement")]
    [SerializeField]
    private float _baseMoveSpeed = 100;
    public float _moveSpeed;
    public float _maxSpeed = 10;

    // Contols the basic movement of the player
    private void MovePlayer()
    {
        horiz = Input.GetAxisRaw("Horizontal");
        vert = Input.GetAxisRaw("Vertical");

        Vector3 camForward = camReference.transform.forward;
        Vector3 camRight = camReference.transform.right;

        camForward.y = 0;
        camForward = camForward.normalized;

        Vector3 moveDirection = (camRight * horiz + camForward * vert).normalized;

        rb.AddForce(moveDirection * _moveSpeed * Time.deltaTime * 100, ForceMode.Impulse);

        float tempY = rb.linearVelocity.y;

        rb.linearVelocity = new Vector3(rb.linearVelocity.x, 0, rb.linearVelocity.z);

        if (rb.linearVelocity.magnitude >= _maxSpeed)
        {
            rb.linearVelocity = Vector3.ClampMagnitude(rb.linearVelocity, _maxSpeed / 1.4f);
        }
        else if (horiz == 0 && vert == 0)
        {
            rb.linearVelocity = Vector3.ClampMagnitude(rb.linearVelocity, 0);
        }

        rb.linearVelocity = new Vector3(rb.linearVelocity.x, tempY, rb.linearVelocity.z);
    }

    [Header("Jump")]
    [SerializeField]
    private int jumpCount;
    public int _maxJumps = 1;
    private float _jumpHeight = 100;
    private float checkDelay;

    // Allows the player to jump
    private void Jump()
    {
        if (Input.GetButtonDown("Jump") && jumpCount > 0)
        {
            rb.linearVelocity = new Vector3(rb.linearVelocity.x, _jumpHeight / 15, rb.linearVelocity.z);
            jumpCount--;
            checkDelay = Time.time + 0.1f;
        }
    }

    private Ray _groundRay;

    // Checks if the player is close enough to the ground
    private bool GroundCheck()
    {
        if (Physics.Raycast(transform.position, -transform.up, 1.1f) && Time.time > checkDelay)
        {
            jumpCount = _maxJumps;
            return true;
        }
        else
        {
            return false;
        }
    }
}
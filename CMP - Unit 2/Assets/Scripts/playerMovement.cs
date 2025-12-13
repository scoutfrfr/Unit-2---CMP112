using UnityEngine;
using UnityEngine.InputSystem;
using static Unity.VisualScripting.Member;

public class playerMovement : MonoBehaviour
{
    // Declaring public variables
    private Rigidbody2D rb;
    public float playerSpeed;
    public float jumpHeight;


    // Declaring private variables
    private float movementX;
    private float movementY;

    private bool playerGrounded;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        movementX = Input.GetAxisRaw("Horizontal");

        // Checks if user is pressing space bar and that the player is grounded
        if (Input.GetKeyDown(KeyCode.Space) && playerGrounded == true)
        {
            // Jump mechanic 
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpHeight);
            playerGrounded = false; 
        }

        // Makes it so if player holds space they jump higher whereas if they just press space and release immediately the jump is shorter
        if (Input.GetKeyUp(KeyCode.Space) && rb.linearVelocity.y > 0f)
        {
            // Jump mechanic 
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, rb.linearVelocity.y * 0.5f);
            playerGrounded = false;
        }
    }

    private void FixedUpdate()
    {
        rb.linearVelocity = new Vector2(movementX * playerSpeed, rb.linearVelocity.y);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Checks object player collides with has the tag "Ground"
        if (collision.gameObject.tag == "Ground")
        {
            playerGrounded = true; // Sets player grounded to true so the player is able to jump
        }
    }
}

using UnityEngine;
using UnityEngine.InputSystem;
using static Unity.VisualScripting.Member;
using TMPro;

public class playerMovement : MonoBehaviour
{
    // Declaring public variables
    public float playerSpeed;
    public float jumpHeight;

    public TextMeshProUGUI coinCountText;
    public Transform spawnPoint;


    // Declaring private variables
    private Rigidbody2D rb;
    private Animator anim;

    private float movementX;
    private float movementY;

    private int coinCount;

    private bool playerGrounded;
    private bool facingRight;
    private bool inRespawn;

    // Audio Variables
    private AudioSource source;
    public AudioClip jumpSound;
    public AudioClip coinSound;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        source = GetComponent<AudioSource>();   
        coinCount = 0;
        SetCountText();
    }

    // Update is called once per frame
    void Update()
    {
        movementX = Input.GetAxisRaw("Horizontal");

        Flip(); // Calls flip function 

        // Checks if user is pressing space bar and that the player is grounded
        if (Input.GetKeyDown(KeyCode.Space) && playerGrounded == true)
        {
            // Jump mechanic 
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpHeight);
            playerGrounded = false;
            source.PlayOneShot(jumpSound, 1.0f);
        }

        // Makes it so if player holds space they jump higher whereas if they just press space and release immediately the jump is shorter
        if (Input.GetKeyUp(KeyCode.Space) && rb.linearVelocity.y > 0f)
        {
            // Jump mechanic 
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, rb.linearVelocity.y * 0.5f);
            playerGrounded = false;
        }

        while (inRespawn == true)
        {
            transform.position = spawnPoint.position;
            inRespawn = false;
        }

        // Player Sprite Animations
        if (playerGrounded == false) // Jumping animation
        {
            anim.SetBool("isJumping", true); 
        }
        else
        {
            anim.SetBool("isJumping", false);
        }

        if (playerGrounded == true && movementX != 0) // Running animation
        {
            anim.SetBool("isRunning", true);
        }
        else
        {
            anim.SetBool("isRunning", false);
        }
    }

    private void FixedUpdate()
    {
        rb.linearVelocity = new Vector2(movementX * playerSpeed, rb.linearVelocity.y);

    }

    void SetCountText()
    {
        coinCountText.text = "Coins: " + coinCount.ToString();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Checks object player collides with has the tag "Ground"
        if (collision.gameObject.tag == "Ground")
        {
            playerGrounded = true; // Sets player grounded to true so the player is able to jump
        }

        if (collision.gameObject.tag == "Respawn")
        {
            inRespawn = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Coin"))
        {
            other.gameObject.SetActive(false); // Deactivate pick up player collided with
            coinCount = coinCount + 1;
            SetCountText();
            source.PlayOneShot(coinSound, 1.0f);
        }
    }

    private void Flip() // Function to make player sprite flip depending on if they are going left or right
    {
        if (facingRight && movementX < 0f || !facingRight && movementX > 0f) // Checks if player is facing right and movementX is negative OR checks if player is NOT facing right and movementX is positive
        {
            facingRight = !facingRight;
            Vector3 localScale = transform.localScale;
            localScale.x *= -1f; // multiples players scale by -1 to "flip" the sprite
            transform.localScale = localScale;
        }
    }
    

    public void Die()
    {
        anim.SetBool("isRunning", false);
        anim.SetBool("isJumping", false);


        rb.linearVelocity = new Vector2(0, 0); // Stops player from moving when dead
    }
}

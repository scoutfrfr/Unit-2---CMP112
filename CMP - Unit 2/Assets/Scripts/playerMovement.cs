using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using static Unity.VisualScripting.Member;

public class playerMovement : MonoBehaviour
{
    // Declaring private components
    private Rigidbody2D rb;
    private Animator anim;
    [Header("Player Sprite")]
    public SpriteRenderer mySprite;

    [Header("Player Movement")] 
    public float playerSpeed;
    public float jumpHeight;
    private float movementX;
    private bool playerGrounded;
    private bool facingRight;

    [Header("Pickups")] 
    public TextMeshProUGUI coinCountText;
    private int coinCount;
    [HideInInspector]
    public bool keyCollected;

    [Header("Respawn")] 
    public Transform spawnPoint;
    private bool inRespawn;

    [Header("Sound effects")] 
    private AudioSource source;
    public AudioClip jumpSound;
    public AudioClip coinSound;
    public AudioClip keySound;

    [Header("Game Over Scene Selection")]
    public string gameOver;
    private bool loadNextLevelStart;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        source = GetComponent<AudioSource>();   
        coinCount = 0;
        SetCountText();
        keyCollected = false;
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

        if (other.gameObject.CompareTag("Key"))
        {
            other.gameObject.SetActive(false); // Deactivate pick up player collided with
            keyCollected = true;
            source.PlayOneShot(keySound, 1.0f);
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
        StartCoroutine(DeathDelay());

    }

    private IEnumerator DeathDelay()
    {
        yield return new WaitForSeconds(2);
        SceneManager.LoadScene(gameOver);
    }
}

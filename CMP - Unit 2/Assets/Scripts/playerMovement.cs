using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using static Unity.VisualScripting.Member;

public class playerMovement : MonoBehaviour
{
    // Components
    private Rigidbody2D rb;
    private Animator anim;

    [Header("Player Movement")] 
    public float playerSpeed;
    public float jumpHeight;
    private float movementX;
    private bool playerGrounded;
    private bool facingRight;

    [Header("Pickups")] 
    public TextMeshProUGUI coinCountText;
    private static int coinCount; // static so coinCount carries across scenes
    public TextMeshProUGUI keyCollectedText;
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
    public AudioClip playerDying;
    public AudioClip GameOverSound;

    [Header("Game Over Scene Selection")]
    public string gameOver;
    

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // Get Components
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        source = GetComponent<AudioSource>();   

        // Set text
        SetCountText(); 
        SetKeyText();

        // Set variables
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
            transform.position = spawnPoint.position; // while player is inRespawn transform their position to spawn point position
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
        // Left & Right movement
        rb.linearVelocity = new Vector2(movementX * playerSpeed, rb.linearVelocity.y); // makes the player move by using velocity, multiplying movementX (horizontal input) by the players speed. 

    }

    void SetCountText() // Sets coin count text to amount of coins player has collected
    {
        coinCountText.text = "Coins: " + coinCount.ToString();
    }

    void SetKeyText() // Sets key text to yes or no depending on if the key in the current scene has been collected
    {
        if (keyCollected == true)
        {
            keyCollectedText.text = "Key Collected: Yes";
        }
        
        if (keyCollected == false)
        {
            keyCollectedText.text = "Key Collected: No";
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Checks object player collides with has the tag "Ground"
        if (collision.gameObject.tag == "Ground")
        {
            playerGrounded = true; // Sets player grounded to true so the player is able to jump
        }

        if (collision.gameObject.tag == "Respawn") // Checks if player has collided with respawn tagged objects
        {
            inRespawn = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Coin")) // Checks if the player has collided with coin
        {
            other.gameObject.SetActive(false); // Deactivate pick up player collided with
            coinCount = coinCount + 1; // Adds one to coin count
            SetCountText(); // Sets coinCount text
            source.PlayOneShot(coinSound, 1.0f);
        }

        if (other.gameObject.CompareTag("Key")) // Checks if the player has collided with key
        {
            other.gameObject.SetActive(false); // Deactivate pick up player collided with
            keyCollected = true;
            SetKeyText();
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
        anim.SetBool("isRunning", false); // ensures is Running animation doesnt play
        anim.SetBool("isJumping", false); // ensures isJumping animation doesnt play
        source.PlayOneShot(playerDying, 1.0f); 
        coinCount = 0; // Sets coin amount to 0 when player dies

        rb.linearVelocity = new Vector2(0, 0); // Stops player from moving when dead
        StartCoroutine(DeathDelay()); 

    }

    private IEnumerator DeathDelay() 
    {
        source.PlayOneShot(GameOverSound, 1.0f); 
        yield return new WaitForSeconds(2); // waits for two seconds
        SceneManager.LoadScene(gameOver); // changes scene to gameOver scene
       

    }
}



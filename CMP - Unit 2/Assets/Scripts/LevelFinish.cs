using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelFinish : MonoBehaviour
{
    [Header("Sound Effects")]
    public AudioClip keyTurning;

    [Header("Level:")]
    public string levelName;

    // Private Variables
    private bool doorUnlocked;

    // Components
    private playerMovement player;
    private Animator anim;
    private AudioSource source;

    private void Start()
    {
        // Get Components
        player = FindFirstObjectByType<playerMovement>();
        anim = GetComponent<Animator>();
        source = GetComponent<AudioSource>();
    }

    private void Update()
    {
        if (doorUnlocked == true) // Loads scene if door is unlocked
        {
            SceneManager.LoadScene(levelName);
            doorUnlocked = false;
            anim.SetBool("doorOpen", false); // Stops door opening animation
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player")) // Checks if player has collided with object (Door)
        {
            if (player.keyCollected == true) // Unlocks door if key has been collected
            {
                source.PlayOneShot(keyTurning, 5.0f);
                anim.SetBool("doorOpen", true); // Plays door opening animation
                StartCoroutine(DelayBeforeNextLevel()); // Starts coroutine for a delay before next level
            }
        }
    }

    private IEnumerator DelayBeforeNextLevel() // Delay before next level starts - allows time for animation to play
    {
        yield return new WaitForSeconds(1); // Wait for one second
        doorUnlocked = true;
    }

}

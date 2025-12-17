using System.Collections;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelFinish : MonoBehaviour
{

    public string levelName;

    private bool doorUnlocked;
    private playerMovement player;
    private Animator anim;

    private void Start()
    {
        player = FindFirstObjectByType<playerMovement>();
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        if (doorUnlocked == true)
        {
            SceneManager.LoadScene(levelName);
            doorUnlocked = false;
            anim.SetBool("doorOpen", false);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (player.keyCollected == true)
            {
                anim.SetBool("doorOpen", true);
                StartCoroutine(DelayBeforeNextLevel());
            }
        }
    }

    private IEnumerator DelayBeforeNextLevel()
    {
        yield return new WaitForSeconds(1);
        doorUnlocked = true;
    }

}

using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelFinish : MonoBehaviour
{

    public string levelName;
    private bool doorUnlocked;
    private playerMovement player;

    private void Start()
    {
        player = FindFirstObjectByType<playerMovement>();
    }

    private void Update()
    {
        if (doorUnlocked == true)
        {
            SceneManager.LoadScene(levelName);
            doorUnlocked = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (player.keyCollected == true)
            {
                doorUnlocked = true;
            }
        }
    }

}

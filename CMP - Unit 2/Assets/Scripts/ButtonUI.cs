using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonUI : MonoBehaviour
{
    [Header ("Level Selection")]
    public string GameStartLevel;
    public string MainMenu;
    public void StartGameButton()
    {
        SceneManager.LoadScene(GameStartLevel);
    }

    public void RestartGameButton()
    {
        SceneManager.LoadScene(MainMenu);
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonUI : MonoBehaviour
{
    [Header ("Level Selection")]
    public string GameStartLevel;
    public string MainMenu;

    public void StartGameButton() // If start game button is pressed in main menu gameStartLevel scene plays
    {
        SceneManager.LoadScene(GameStartLevel);
    }

    public void RestartGameButton() // If restart game button is pressed in game over MainMenu scene plays
    {
        SceneManager.LoadScene(MainMenu);
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonUI : MonoBehaviour
{
    [Header ("Starting Level")]
    public string GameStartLevel;
    public void StartGameButton()
    {
        SceneManager.LoadScene(GameStartLevel);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public void QuitGame() {
        UnityEditor.EditorApplication.isPlaying = false;
        Application.Quit();
    }

    public void PlayGame()
    {
        SceneManager.LoadScene("Level1");
    }

}

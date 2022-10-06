using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    //Script for storing winner name between scenes

    public static string winnerName;
    public static string friendshipStatus;


    void Awake()
    {
        DontDestroyOnLoad(this);
    }
    
    public static void StartGame()
    {
        SceneManager.LoadScene(1);
    }

    public static void QuitGame()
    {
        Application.Quit();
    }
}

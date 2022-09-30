using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TurnManager : MonoBehaviour
{
    private static TurnManager instance;

    private bool gracePeriodActive;
    private bool timerActive;
    private float timeLeft;
    private int activeTeamIndex;

    [SerializeField] private float turnDuration;
    [SerializeField] private float gracePeriodDuration;
    [SerializeField] private string[] names;
    [SerializeField] private GameObject[] players;
    [SerializeField] private CinemachineVirtualCamera[] cameras;

    public delegate void OnTurnEnd();
    public static event OnTurnEnd onTurnEnd;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    void Start()
    {
        activeTeamIndex = 0;
        timerActive = true;
        timeLeft = turnDuration;
        //Initialize cameras
        for (int i = 0; i < cameras.Length; ++i)
        {
            cameras[i].Priority = 0;
        }
        cameras[activeTeamIndex].Priority = 1;


        if (players.Length != cameras.Length)
        {
            Debug.LogError("Wrong number of cameras for players!");
        }
    }

    void Update()
    {
        if (timerActive)
        {
            timeLeft -= Time.deltaTime;
            if(!gracePeriodActive)
            {
                if (timeLeft <= 0)
                {
                    SwapTurns();
                }
                UIManager.UpdateTimer(timeLeft);
            }
            else
            {
                if (timeLeft <= 0)
                {
                    gracePeriodActive = false;
                    timeLeft = turnDuration;
                    InputHandler.SetControlsActive(true);
                    UIManager.ClearText();
                    UIManager.EnableTimer();
                    UIManager.UpdateTimer(timeLeft);
                }
            }
        }
    }

    public static TurnManager GetInstance()
    {
        return instance;
    }

    public static PlayerController GetCurrentPlayer()
    {
        TurnManager tM = GetInstance();
        return tM.players[tM.activeTeamIndex].GetComponent<PlayerController>();
    }

    public static CinemachineVirtualCamera GetCurrentCamera()
    {
        TurnManager tM = GetInstance();
        return tM.cameras[tM.activeTeamIndex];
    }

    public static void SwapTurns()
    {
        TurnManager tM = GetInstance();
        GetCurrentPlayer().EndTurn();
        //Switching off old camera
        tM.cameras[tM.activeTeamIndex].Priority = 0;
        //The swap
        tM.activeTeamIndex++;
        tM.activeTeamIndex %= tM.players.Length;
        //Switching on next camera
        tM.cameras[tM.activeTeamIndex].Priority = 1;

        tM.gracePeriodActive = true;
        tM.timeLeft = tM.gracePeriodDuration;
        InputHandler.SetControlsActive(false);
        UIManager.DisableTimer();

        GetCurrentPlayer().RefreshTurn();
        onTurnEnd();
    }

    public static void EndGame()
    {
        TurnManager tM = GetInstance();

        MainMenu.winnerName = tM.names[tM.activeTeamIndex];
        MainMenu.friendshipStatus = "Friendship did not survive.";
        SceneManager.LoadScene(2);
    }

}

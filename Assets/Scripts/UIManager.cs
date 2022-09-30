using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    private static UIManager instance;

    [SerializeField] private Slider health;
    [SerializeField] private Slider style;
    [SerializeField] private TMP_Text messageBox;
    [SerializeField] private TMP_Text timer;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }

        TurnManager.onTurnEnd += UpdateTurn;
    }

    void Start()
    {
        UpdateBars();
    }

    static void UpdateTurn()
    {
        UpdateBars();
        DisplayText("New Turn!");
    }

    public static UIManager GetInstance()
    {
        return instance;
    }

    public static void UpdateBars()
    {
        UIManager UIM = GetInstance();
        PlayerController player = TurnManager.GetCurrentPlayer();
        UIM.health.value = player.GetHealth();
        UIM.style.value = player.GetStyle();
    }

    public static void UpdateTimer(float time)
    {
        UIManager UIM = GetInstance();
        float seconds = Mathf.CeilToInt(time % 60);
        UIM.timer.text = seconds.ToString();
    }

    public static void DisableTimer()
    {
        UIManager UIM = GetInstance();
        UIM.timer.enabled = false;

    }

    public static void EnableTimer()
    {
        UIManager UIM = GetInstance();
        UIM.timer.enabled = true;

    }

    public static void DisplayText(string message)
    {
        UIManager UIM = GetInstance();
        UIM.messageBox.text = message;
        UIM.messageBox.gameObject.SetActive(true);
    }

    public static void ClearText()
    {
        UIManager UIM = GetInstance();
        UIM.messageBox.text = "";
        UIM.messageBox.gameObject.SetActive(false);

    }

    public static void HideUI()
    {

    }

    public static void RevealUI()
    {

    }
}

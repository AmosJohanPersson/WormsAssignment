using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    //This class handles displaying and updating UI at the request of other scripts.

    private static UIManager instance;

    [SerializeField] private Slider water;
    [SerializeField] private Slider health;
    [SerializeField] private Slider style;
    [SerializeField] private TMP_Text messageBox;
    [SerializeField] private TMP_Text timer;
    [SerializeField] private PistolAmmoDisplay[] ammoDisplays;
    private bool useDualAmmoBars;

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
        useDualAmmoBars = false;
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

    public static void DisplayAmmo(int index, bool sprayGun = false, float water = 1)
    {
        UIManager UIM = GetInstance();
        if (sprayGun)
        {
            UIM.useDualAmmoBars = true;
            UIM.water.gameObject.SetActive(true);
            UIM.water.value = water;
        }

        UIM.ammoDisplays[index].Display();
    }

    public static void HideAmmo(int index)
    {
        UIManager UIM = GetInstance();
        if (UIM.useDualAmmoBars)
        {
            UIM.useDualAmmoBars = false;
            UIM.water.gameObject.SetActive(false);
        }

        UIM.ammoDisplays[index].Hide();

    }

    public static void UpdateAmmo(int index, int amount, float water = 0)
    {
        UIManager UIM = GetInstance();
        if (UIM.useDualAmmoBars)
        {
            UIM.water.value = water;
        }

        UIM.ammoDisplays[index].SetDisplayedAmmo(amount);
    }
}

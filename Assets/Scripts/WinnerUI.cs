using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class WinnerUI : MonoBehaviour
{
    public TMP_Text winner;
    public TMP_Text friendshipStatus;

    void Start()
    {
        winner.text = MainMenu.winnerName + " wins!";
        friendshipStatus.text = MainMenu.friendshipStatus;
    }
}

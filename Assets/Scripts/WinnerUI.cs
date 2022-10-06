using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class WinnerUI : MonoBehaviour
{
    //Fills in player name onto the victory screen
    public TMP_Text winner;
    public TMP_Text friendshipStatus;

    void Start()
    {
        winner.text = MainMenu.winnerName + " wins!";
        friendshipStatus.text = MainMenu.friendshipStatus;
    }
}

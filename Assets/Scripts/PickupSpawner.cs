using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupSpawner : MonoBehaviour
{
    //Implements respawn cooldown for pickups

    [SerializeField] private GameObject pickup;
    [SerializeField] private int respawnTime;

    private int respanwCounter;

    void Awake()
    {
        TurnManager.onTurnEnd += RespawnCountdown;
    }

    public void DisablePickup()
    {
        pickup.SetActive(false);
        respanwCounter = 0;
    }

    public void RespawnCountdown()
    {
        respanwCounter++;
        if (respanwCounter >= respawnTime)
        {
            Respawn();
        }
    }

    public void Respawn()
    {
        if (!pickup.activeInHierarchy)
        {
            pickup.SetActive(true);
        }
    }

}

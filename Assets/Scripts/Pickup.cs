using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour
{
    [SerializeField] private float healing;

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            PlayerController player = other.GetComponent<PlayerController>();
            if (!player.AtFullHealth())
            {
                other.GetComponent<PlayerController>().GainHealth(healing);
                transform.parent.GetComponent<PickupSpawner>().DisablePickup();
                UIManager.UpdateBars();
            }
        }
    }
}

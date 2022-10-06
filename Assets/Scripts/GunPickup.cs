using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunPickup : MonoBehaviour
{
    //Pickup script for the gun pickups

    [SerializeField] private GameObject gunPrefab;
    [SerializeField] private GameObject spawner;

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            PlayerController player = other.GetComponent<PlayerController>();
            GameObject gun = Instantiate(gunPrefab, player.transform);
            player.AddWeapon(gun.GetComponent<WeaponController>());

            spawner.GetComponent<PickupSpawner>().DisablePickup();
        }
    }
}

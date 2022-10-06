using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletExplosion : MonoBehaviour
{
    //Script for explosion collider on "stinky fish" weapon
    private bool damageIsDealt = false;
    public float styleDamage;

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject != TurnManager.GetCurrentPlayer().gameObject)
        {
            if (other.gameObject.tag == "Player" && !damageIsDealt)
            {
                other.GetComponent<PlayerController>().ScoreStyle(-styleDamage);
                damageIsDealt = true;
            }
        }
    }
}

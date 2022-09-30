using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletExplosion : MonoBehaviour
{
    private bool damageIsDealt = false;
    public float styleDamage;

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject != TurnManager.GetCurrentPlayer().gameObject)
        {
            if (other.gameObject.tag == "Player" && !damageIsDealt)
            {
                other.GetComponent<PlayerController>().ScoreStyle(-styleDamage);
            }
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoopTriggers : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            var player = other.GetComponent<PlayerController>();
            if (player == TurnManager.GetCurrentPlayer())
            {
                GetGroupController().JumpScore(player);
            }
        }
        else if (other.tag == "Bullet")
        {
            GetGroupController().Trickshot(other.GetComponent<BulletController>());
        }
    }

    TriggerGroupController GetGroupController()
    {
        return this.transform.parent.gameObject.GetComponent<TriggerGroupController>();
    }
}

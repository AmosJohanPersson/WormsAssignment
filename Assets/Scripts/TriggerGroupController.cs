using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerGroupController : MonoBehaviour
{
    public float jumpBonus;
    public float shotBonus;
    public float cooldown;

    private bool jumpEnabled = true;
    private bool trickshotEnabled = true;

    void Awake()
    {
        TurnManager.onTurnEnd += RefreshCooldowns;
    }

    public void JumpScore(PlayerController player)
    {
        if (jumpEnabled)
        {
            player.ScoreStyle(jumpBonus);
            jumpEnabled = false;
        }
    }

    public void Trickshot(BulletController bullet)
    {
        if (trickshotEnabled)
        {
            bullet.AddScore(shotBonus);
            trickshotEnabled = false;
        }
    }

    public void RefreshCooldowns()
    {
        trickshotEnabled = true;
        jumpEnabled = true;
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour
{
    // Quasi-abstract parent class for all weapon types. Implements aiming, get player, and listening to onTurnEnd event. 

    public Transform muzzle;

    public GameObject gun;

    void Start()
    {
        TurnManager.onTurnEnd += CleanUp;
        Reload();
    }

    public PlayerController GetPlayer()
    {
        return transform.parent.GetComponent<PlayerController>();
    }

    public virtual void FacePoint(Vector3 target)
    {
        gun.transform.LookAt(target);
    }

    public virtual void SetEnabled(bool state)
    {
        gun.SetActive(state);
    }
    
    public virtual void Shoot()
    {
    }
    public virtual void Reload()
    {
    }
    public virtual void CleanUp()
    {
    }
    public virtual void HoldBehaviour()
    {
    }

    public virtual void HideUI()
    {

    }

    public virtual void Initialize()
    {

    }
}

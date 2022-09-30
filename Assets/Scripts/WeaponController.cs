using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour
{
    public Transform muzzle;

    public GameObject gun;

    void Start()
    {
        TurnManager.onTurnEnd += CleanUp;
        Reload();
        CleanUp();
    }

    public PlayerController GetPlayer()
    {
        return transform.parent.GetComponent<PlayerController>();
    }

    public virtual void FacePoint(Vector3 target)
    {
        gun.transform.LookAt(target);
    }

    public void SetEnabled(bool state)
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
}

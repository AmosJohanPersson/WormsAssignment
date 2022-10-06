using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PistolController : WeaponController
{
    //Defines shooting, reloading and ammo for the default pistol. 

    public int ammoMax;
    private int ammoNow;

    public GameObject bulletPrefab;
    public LineRenderer lineRenderer;
    public PistolAmmoDisplay ammoDisplay;

    private const int UI_INDEX = 0;
    public float damage;
    public float styleBonus;

    public override void Shoot()
    {
        if (ammoNow > 0)
        {
            float style = 0;
            if (GetPlayer().IsJumping())
            {
                style = styleBonus;
            }

            GameObject bullet = Instantiate(bulletPrefab, muzzle.position, muzzle.rotation);
            bullet.GetComponent<BulletController>().Initialize(damage, style);

            ammoNow--;
            UIManager.UpdateAmmo(UI_INDEX, ammoNow);
        }
    }

    public override void Reload()
    {
        ammoNow = ammoMax;
        UIManager.DisplayAmmo(UI_INDEX);
        UIManager.UpdateAmmo(UI_INDEX, ammoNow);
    }


    public override void FacePoint(Vector3 target)
    {
        //Linerenderer is added from parent class version
        gun.transform.LookAt(target);
        lineRenderer.SetPosition(0, muzzle.position);
        lineRenderer.SetPosition(1, target - (transform.position - muzzle.position));
    }

    public override void CleanUp()
    {
        lineRenderer.SetPosition(0, muzzle.position);
        lineRenderer.SetPosition(1, muzzle.position);
    }

    public override void SetEnabled(bool state)
    {
        gun.SetActive(state);
        if (state)
        {
            UIManager.DisplayAmmo(UI_INDEX);
            UIManager.UpdateAmmo(UI_INDEX, ammoNow);
        }
        else
        {
            UIManager.HideAmmo(UI_INDEX);
        }
    }

    public override void HideUI()
    {
        UIManager.HideAmmo(UI_INDEX);

    }
}



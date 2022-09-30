using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PistolController : WeaponController
{
    public int ammoMax;
    private int ammoNow;

    public GameObject bulletPrefab;
    public LineRenderer lineRenderer;

    public float damage;
    public float styleBonus;

    public override void Shoot()
    {
        if (ammoNow > 0)
        {
            ammoNow--;
            GameObject bullet = Instantiate(bulletPrefab, muzzle.position, muzzle.rotation);
            float style = 0;
            if (GetPlayer().IsJumping())
            {
                style = styleBonus;
            }
            bullet.GetComponent<BulletController>().Initialize(damage, style);
        }
    }

    public override void Reload()
    {
        ammoNow = ammoMax;
    }

    public override void FacePoint(Vector3 target)
    {
        gun.transform.LookAt(target);
        lineRenderer.SetPosition(0, muzzle.position);
        lineRenderer.SetPosition(1, target - (transform.position - muzzle.position));
    }

    public override void CleanUp()
    {
        lineRenderer.SetPosition(0, muzzle.position);
        lineRenderer.SetPosition(1, muzzle.position);
    }
}



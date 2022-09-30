using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SprayGunController : WeaponController
{
    public GameObject bulletPrefab;

    [SerializeField] private float ammoMax;
    [SerializeField] private float damage;
    [SerializeField] private float styleBonus;
    [SerializeField] private float firingDelay;
    [SerializeField] private float totalAmmoTanks;


    private float ammoNow;
    private float secondsSinceFire;

    void Start()
    {
        secondsSinceFire = firingDelay;
        ammoNow = ammoMax;
    }

    public override void Shoot()
    {
        if (ammoNow > 0)
        {
            ammoNow--;
            float style = 0;
            if (GetPlayer().IsJumping())
            {
                style = styleBonus;
            }

            GameObject bullet = Instantiate(bulletPrefab, muzzle.position, muzzle.rotation);
            bullet.GetComponent<BulletController>().Initialize(damage, style);
        }
    }

    public override void Reload()
    {
        if (ammoNow < ammoMax)
        {
            ammoNow = ammoMax;
            totalAmmoTanks--;
            if (totalAmmoTanks <= 0)
            {
                GetPlayer().RemoveWeapon(this);
            }
        }
    }

    public override void HoldBehaviour()
    {
        secondsSinceFire += Time.deltaTime;
        if (secondsSinceFire >= firingDelay)
        {
            Shoot();
            secondsSinceFire = 0;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SprayGunController : WeaponController
{
    //Defines shooting, reloading and ammo for the water spraying gun. 

    public GameObject bulletPrefab;

    [SerializeField] private float ammoMax;
    [SerializeField] private float damage;
    [SerializeField] private float styleBonus;
    [SerializeField] private float firingDelay;
    [SerializeField] private int totalAmmoTanks;
    [SerializeField] private Slider ammoPerTurnUI;
    [SerializeField] private PistolAmmoDisplay ammoTanksUI;

    private const int UI_INDEX = 1;
    private float ammoNow;
    private float secondsSinceFire;
    private bool emptyTank;

    public override void Initialize()
    {
        Debug.Log("init");
        secondsSinceFire = firingDelay;
        ammoNow = ammoMax;
        emptyTank = false;

    }

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
            UIManager.UpdateAmmo(UI_INDEX, totalAmmoTanks, ammoNow / ammoMax);
        }
        else if (!emptyTank)
        {
            //out of ammo for this turn
            emptyTank = true;
            totalAmmoTanks--;
            UIManager.UpdateAmmo(UI_INDEX, totalAmmoTanks, ammoNow / ammoMax);
        }
    }

    public override void Reload()
    {
        if (ammoNow < ammoMax)
        {
            emptyTank = false;
            ammoNow = ammoMax;


            if (totalAmmoTanks <= 0)
            {
                GetPlayer().RemoveWeapon(this);
            }
            else
            {
                UIManager.DisplayAmmo(UI_INDEX, sprayGun: true, water: ammoNow / ammoMax);
                UIManager.UpdateAmmo(UI_INDEX, totalAmmoTanks, ammoNow / ammoMax);
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


    public override void SetEnabled(bool state)
    {
        gun.SetActive(state);
        if (state)
        {
            UIManager.DisplayAmmo(UI_INDEX, sprayGun: true, water: ammoNow / ammoMax);
            UIManager.UpdateAmmo(UI_INDEX, totalAmmoTanks, ammoNow / ammoMax);
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

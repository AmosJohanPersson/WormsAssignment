using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishGunController : WeaponController
{
    //Defines shooting, reloading and ammo for the fish "gun". 

    public GameObject bulletPrefab;

    [SerializeField] private int totalAmmoTanks;
    [SerializeField] private PistolAmmoDisplay ammoTanksUI;

    [SerializeField] private float styleDamage;

    [SerializeField] private float maxCharge;
    [SerializeField] private float chargeSpeed;
    [SerializeField] private float upChargeFactor;
    [SerializeField] private float forwardChargeFactor;

    [SerializeField] private float upForce;
    [SerializeField] private float forwardForce;
    [SerializeField] private float groundY;

    [SerializeField] private LineRenderer lineRenderer;

    private const int UI_INDEX = 2;
    private float charge;
    private bool isFishAtHome = true;

    public override void Shoot()
    {
        if (isFishAtHome)
        {
            GameObject bullet = Instantiate(bulletPrefab, gun.transform.position, gun.transform.rotation);
            Vector3 force = GetLaunchForce();
            bullet.GetComponent<FishBullet>().Initialize(force, styleDamage, this);
            isFishAtHome = false;
            gun.SetActive(false);
            charge = 0;

            totalAmmoTanks--;
            UIManager.DisplayAmmo(UI_INDEX);
            UIManager.UpdateAmmo(UI_INDEX, totalAmmoTanks);
        }
    }

    public override void Reload()
    {
        if (!isFishAtHome)
        {
            isFishAtHome = true;
            if (totalAmmoTanks <= 0)
            {
                GetPlayer().RemoveWeapon(this);
            }
            else
            {
                UIManager.UpdateAmmo(UI_INDEX, totalAmmoTanks);
            }
        }

    }

    public override void FacePoint(Vector3 target)
    {
        if (isFishAtHome)
        {
            gun.transform.LookAt(target);
            PaintTrajectory();
        }
        else
        {
            CleanUp();
        }
    }

    public void PaintTrajectory()
    {
        //Calculates how far along the ground the fish will fly given the current force and paints a line accordingly
        float mass = bulletPrefab.GetComponent<Rigidbody>().mass;
        Vector3 force = GetLaunchForce();
        Vector3 velocity = (force / mass) * Time.fixedDeltaTime;
        float flightTime = (2 * velocity.y) / -Physics.gravity.y;

        Vector3 home = new Vector3(gun.transform.position.x, groundY, gun.transform.position.z);
        Vector3 target = home + new Vector3(velocity.x * flightTime, groundY, velocity.z * flightTime);

        lineRenderer.SetPosition(0, home);
        lineRenderer.SetPosition(1, target);
    }

    private Vector3 GetLaunchForce()
    {
        float up = upForce + charge * upChargeFactor;
        float forward = forwardForce + charge * forwardChargeFactor;
        return gun.transform.forward * forward + gun.transform.up * up;
    }

    public override void HoldBehaviour()
    {
        charge += chargeSpeed * Time.deltaTime;
        if (charge > maxCharge)
        {
            charge = maxCharge;
        }
    }

    public override void CleanUp()
    {
        lineRenderer.SetPosition(0, gun.transform.position);
        lineRenderer.SetPosition(1, gun.transform.position);
    }

    public override void SetEnabled(bool state)
    {
        gun.SetActive(state);
        if (state)
        {
            UIManager.DisplayAmmo(UI_INDEX);
            UIManager.UpdateAmmo(UI_INDEX, totalAmmoTanks);
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



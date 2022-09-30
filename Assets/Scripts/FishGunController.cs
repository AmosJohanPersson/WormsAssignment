using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishGunController : WeaponController
{
    public GameObject bulletPrefab;

    [SerializeField] private float totalAmmoTanks;
    [SerializeField] private float styleDamage;

    [SerializeField] private float maxCharge;
    [SerializeField] private float chargeSpeed;
    [SerializeField] private float upChargeFactor;
    [SerializeField] private float forwardChargeFactor;

    [SerializeField] private float upForce;
    [SerializeField] private float forwardForce;
    [SerializeField] private float groundY;

    [SerializeField] private LineRenderer lineRenderer;

    private float charge;
    private bool isFishAtHome = true;
    private Vector3 fishStartPosition;

    public override void Shoot()
    {
        if (isFishAtHome)
        {
            Debug.Log("home");
            GameObject bullet = Instantiate(bulletPrefab, gun.transform.position, gun.transform.rotation);
            Vector3 force = GetLaunchForce();
            bullet.GetComponent<FishBullet>().Initialize(force, styleDamage, this);
            isFishAtHome = false;
            gun.SetActive(isFishAtHome);
            charge = 0;
        }
    }

    public override void Reload()
    {
        if (!isFishAtHome)
        {
            isFishAtHome = true;

            totalAmmoTanks--;
            if (totalAmmoTanks <= 0)
            {
                GetPlayer().RemoveWeapon(this);
            }
        }

    }

    public override void FacePoint(Vector3 target)
    {
        if (isFishAtHome)
        {

            Debug.Log("home");
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
}



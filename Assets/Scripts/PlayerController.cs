using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //This class implements an action abstraction layer for the player, and directly implements movement. All actions governing the player or the weapons pass through this class. 

    Rigidbody rigid;

    public float mSpeed;
    public float swerve;
    public float jumpHeight;
    public float jumpThrust;

    private bool hasFired;
    private bool isJumping;

    private float health;
    private float style;
    private int currentWeaponIndex;

    [SerializeField] private float maxHealth;
    [SerializeField] private float maxStyle;
    [SerializeField] private List<WeaponController> weapons;

    void Start()
    {
        rigid = GetComponent<Rigidbody>();
        health = maxHealth;
        style = 0;
        isJumping = false;
        hasFired = false;
        currentWeaponIndex = 0;
        GetCurrentWeapon().SetEnabled(true);
    }

    public void Move(float forward, float rotationAngle)
    {
        float speed = mSpeed;
        float turnSpeed = swerve;
        if (isJumping)
        {
            speed /= 2;
        }

        rigid.MoveRotation(rigid.rotation * Quaternion.Euler(0, rotationAngle * turnSpeed, 0));
        rigid.MovePosition(transform.position + forward * speed * transform.forward);
    }

    public void Jump()
    {
        if (!isJumping)
        {
            rigid.AddForce(transform.up * jumpHeight);
            rigid.AddForce(transform.forward * jumpThrust);
            isJumping = true;
        }
    }

    public bool IsJumping()
    {
        return isJumping;
    }

    public void Fire()
    {
        hasFired = true;
        GetCurrentWeapon().Shoot();
    }

    public void HoldDownFire()
    {
        GetCurrentWeapon().HoldBehaviour();
    }

    public void Aim(Vector3 point)
    {
        GetCurrentWeapon().FacePoint(point);
    }

    public void TakeDamage(float amount)
    {
        health -= amount;
        if (health <= 0)
        {
            TurnManager.EndGame();
        }
    }

    public void GainHealth(float amount)
    {
        health += amount;
        if (health >= maxHealth)
        {
            health = maxHealth;
        }
    }

    public bool AtFullHealth()
    {
        return health >= maxHealth;
    }

    public void ScoreStyle(float amount)
    {
        style += amount;
        UIManager.UpdateBars();
        if (style >= maxStyle)
        {
            TurnManager.EndGame();
        }
    }

    public void EndTurn()
    {
        gameObject.layer = LayerMask.NameToLayer("Default");
        GetCurrentWeapon().HideUI();
    }

    public void RefreshTurn()
    {
        GetCurrentWeapon().Reload();
        hasFired = false;
        gameObject.layer = LayerMask.NameToLayer("Ignore Raycast");
    }

    public float GetHealth()
    {
        return health;
    }

    public float GetStyle()
    {
        return style;
    }

    public WeaponController GetCurrentWeapon()
    {
        return weapons[currentWeaponIndex];
    }

    public void ToggleWeapons(bool forceSwap = false)
    {
        if(!hasFired | forceSwap)
        {
            if (weapons.Count > 1)
            {
                //Switching off old weapon
                weapons[currentWeaponIndex].CleanUp();
                weapons[currentWeaponIndex].SetEnabled(false);
                //The swap
                currentWeaponIndex++;
                currentWeaponIndex %= weapons.Count;
                //Switching on next weapon
                weapons[currentWeaponIndex].SetEnabled(true);
            }
        }
    }

    public void AddWeapon(WeaponController controller)
    {
        var toggleDistance = weapons.Count - currentWeaponIndex;
        weapons.Add(controller);
        controller.Initialize();
        if (!hasFired)
        {
            //swap to new weapon
            for (int i = 0; i < toggleDistance; i++)
            {
                ToggleWeapons(forceSwap: true);
            }
        }
    }

    public void RemoveWeapon(WeaponController controller)
    {
        if (weapons.Count > 1)
        {
            if (controller == GetCurrentWeapon())
            {
                weapons[currentWeaponIndex].CleanUp();
                //Switching on weapon 0 (pistol)
                currentWeaponIndex = 0;
                weapons[currentWeaponIndex].SetEnabled(true);
            }

            weapons.Remove(controller);
            Destroy(controller.gameObject);
        }

    }

    void OnCollisionEnter(Collision hit)
    {
        if (hit.gameObject.tag == "Ground")
        {
            isJumping = false;
        }
    }
}

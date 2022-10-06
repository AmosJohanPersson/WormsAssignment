using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SprayBulletController : BulletController
{
    //Spray bullets use gravity and need force applied to them at initialization
    [SerializeField] private float upForce;
    [SerializeField] private float forwardForce;

    public override void Initialize(float dmg, float styleBonus)
    {
        rigid = GetComponent<Rigidbody>();
        rigid.AddForce(transform.forward * forwardForce + transform.up * upForce);
        style = styleBonus;
        damage = dmg;
    }
}

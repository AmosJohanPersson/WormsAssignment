using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SprayBulletController : BulletController
{
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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishBullet : BulletController
{
    //Fish gun bullet controller that allows ballistic arc and spawning an explosion on inpact
    public FishGunController controller;

    public void Initialize(Vector3 force, float styleBonus, FishGunController weaponController)
    {
        rigid = GetComponent<Rigidbody>();
        controller = weaponController;
        style = styleBonus;
        rigid.AddForce(force);
    }

    void OnTriggerEnter(Collider other)
    {
        PlayerController cPlayer = TurnManager.GetCurrentPlayer();
        if (other.gameObject != cPlayer.gameObject && other.gameObject.tag != "Hoop")
        {
            Splash(rigid.position, rigid.rotation);
            BulletManager.Expire(this.gameObject);
        }
    }

    public override void Splash(Vector3 position, Quaternion rotation)
    {
        GameObject splash = Instantiate(onHitEffect, position, rotation);
        BulletManager.CleanWDelay(splash, 2f);
        splash.GetComponent<BulletExplosion>().styleDamage = style;
    }
}

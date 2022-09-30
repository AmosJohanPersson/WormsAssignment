using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    public Rigidbody rigid;
    public GameObject onHitEffect;
    public float style;
    public float damage;

    [SerializeField] private float speed; 

    public virtual void Initialize(float dmg, float styleBonus)
    {
        rigid = GetComponent<Rigidbody>();
        style = styleBonus;
        damage = dmg;
    }

    public void AddScore(float amount)
    {
        style += amount;
    }

    void Update()
    {
        rigid.MovePosition(rigid.position + transform.forward * speed * Time.deltaTime);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject != TurnManager.GetCurrentPlayer().gameObject && other.gameObject.tag != "Hoop")
        {
            Splash(rigid.position, rigid.rotation);
            BulletManager.Expire(this.gameObject);

            if (other.gameObject.tag == "Player")
            {
                other.GetComponent<PlayerController>().TakeDamage(damage);
                TurnManager.GetCurrentPlayer().ScoreStyle(style);
            }
        }
    }

    public virtual void Splash(Vector3 position, Quaternion rotation)
    {
        GameObject splash = Instantiate(onHitEffect, position, rotation);
        BulletManager.CleanWDelay(splash, 2f);
    }
}

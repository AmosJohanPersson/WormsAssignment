using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletManager : MonoBehaviour
{
    //Allows bullets to avoid destroying themselves
    private static BulletManager instance;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    public static BulletManager GetInstance()
    {
        return instance;
    }

    public static void Expire(GameObject bullet)
    {
        Destroy(bullet);
    }

    public static void CleanWDelay(GameObject gObject, float delayInS)
    {
        Destroy(gObject, delayInS);
    }
}

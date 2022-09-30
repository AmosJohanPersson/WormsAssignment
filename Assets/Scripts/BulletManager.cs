using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletManager : MonoBehaviour
{
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
        GetInstance().StartCoroutine(GetInstance().InternalCleanWDelay(gObject, delayInS));
    }

    IEnumerator InternalCleanWDelay(GameObject gObject, float delay)
    {
        yield return new WaitForSeconds(delay);
        Destroy(gObject);
    }
}

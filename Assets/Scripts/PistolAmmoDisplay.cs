using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PistolAmmoDisplay : MonoBehaviour
{
    //Displays ammo images for all weapons. Each icon has two images: filled image on top of an "empty" image.

    [SerializeField] private GameObject[] ammoRects;
    [SerializeField] private GameObject[] emptyRects;

    public void Display()
    {
        foreach (GameObject rect in emptyRects)
        {
            rect.SetActive(true);
        }
    }
    public void Hide()
    {
        foreach (GameObject rect in emptyRects)
        {
            rect.SetActive(false);
        }
        foreach (GameObject rect in ammoRects)
        {
            rect.SetActive(false);
        }
    }

    public void SetDisplayedAmmo(int amount)
    {
        Display();

        int i = 0;
        foreach (GameObject rect in ammoRects)
        {
            if (i < amount)
            {
                rect.SetActive(true);
            }
            else
            {
                rect.SetActive(false);
            }
            i++;
        }
    }
}

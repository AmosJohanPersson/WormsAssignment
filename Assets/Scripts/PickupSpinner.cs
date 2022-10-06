using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupSpinner : MonoBehaviour
{
    //Spins and bobs gameobjects

    [SerializeField] private Vector3 angles;
    [SerializeField] private float bobHeight;
    [SerializeField] private float bobSpeed;

    [SerializeField] private float offsetSpread;

    private Vector3 startPoint;
    private Vector3 endPoint;
    private float offset;

    void Start()
    {
        startPoint = transform.position;
        endPoint = startPoint + transform.up * bobHeight;
        offset = Random.Range(0f, offsetSpread);
    }

    void Update()
    {
        //spinning
        transform.Rotate(angles * Time.deltaTime);
        //bobbing
        float time = Mathf.Sin(Mathf.PingPong(Time.time * bobSpeed + offset, 1));
        transform.position = Vector3.Lerp(startPoint, endPoint, time);
    }
}

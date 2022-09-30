using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PivotTesting : MonoBehaviour
{
    public Transform muzzle;
    public LineRenderer lineRenderer;

    public void FacePoint(Vector3 target)
    {
        transform.LookAt(target);
        lineRenderer.SetPosition(0, muzzle.position);
        lineRenderer.SetPosition(1, target);
    }
}

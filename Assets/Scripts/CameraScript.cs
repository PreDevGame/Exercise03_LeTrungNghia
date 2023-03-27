using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public  class CameraScript : MonoBehaviour
{
    [Header("Camera")]  
    public Transform CameraTarget;
    public float Speed = 10.0f;
    public Vector3 Dist;
    public Transform LookTarget;

    void FixedUpdate()
    {
        Vector3 fPos = CameraTarget.position + Dist;
        Vector3 bPos = Vector3.Lerp(transform.position, fPos, Speed * Time.deltaTime);
        transform.position = bPos;
        transform.LookAt(LookTarget.position);
    }
}

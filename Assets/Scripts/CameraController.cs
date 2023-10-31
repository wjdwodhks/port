using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform target;

    void Update()
    {
        Vector3 targetPos = new Vector3(Mathf.Clamp(target.position.x, -13.85f, 14.71f), transform.position.y, transform.position.z);
        this.transform.position = targetPos;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float rotateSpeed;

    internal void Update()
    {
        Vector3 target = new(transform.position.x, 0f, transform.position.z);
        target = Vector3.Lerp(Vector3.zero, target, 0.25f);
        transform.LookAt(target);
        float rotateDeg = -rotateSpeed * Time.deltaTime * Input.GetAxis("Horizontal");
        transform.RotateAround(Vector3.zero, Vector3.up, rotateDeg);

        float dotPitch = Vector3.Dot(transform.position.normalized, Vector3.up);
        float deltaY = Input.GetAxis("Vertical");
        if (!(dotPitch < 0.1f && deltaY < 0f) && !(dotPitch > 0.9f && deltaY > 0f))
        {
            rotateDeg = rotateSpeed * Time.deltaTime * deltaY;
            transform.RotateAround(Vector3.zero, transform.right, rotateDeg);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Camera))]
public class CameraController : MonoBehaviour
{
    [SerializeField, Tooltip("The object being tracked by the camera")]
    private Transform target = default;
    [SerializeField, Tooltip("The speed at which the camera follows the target")]
    private float moveSpeed = 3.0f;
    [SerializeField, Tooltip("How far off the target the camera aims for")]
    private Vector3 targetOffset = new Vector3(0, 2.0f, -10.0f);

    private void LateUpdate()
    {
        //Lerp towards the target position plus the target offset value
        if (target)
        {
            transform.position = Vector3.Lerp(transform.position, target.position + targetOffset, Time.deltaTime * moveSpeed);
        }
    }
}

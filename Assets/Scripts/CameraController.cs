using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Camera))]
public class CameraController : MonoBehaviour
{
   
    [SerializeField, Tooltip("The speed at which the camera follows the target")]
    private float moveSpeed = 3.0f;

    private bool isControlling = true;
    Vector3 oldPos = new Vector3();
    Vector3 velocity = new Vector3();


    public static CameraController instance = default;

    private void Awake()
    {
        instance = this;
        oldPos = transform.position;
    }

    private void LateUpdate()
    {
        if (isControlling)
        {
            //Lerp to the right
            transform.position = Vector3.Lerp(transform.position, transform.position + transform.right, Time.deltaTime * moveSpeed);
        }


        velocity = (transform.position - oldPos) / Time.deltaTime;
        oldPos = transform.position;

    }

    public Vector3 GetVelocity()
    {
        return velocity;
    }

    public void PauseCamera()
    {
        isControlling = false;
    }

    public void UnPauseCamera()
    {
        isControlling = true;
    }
}

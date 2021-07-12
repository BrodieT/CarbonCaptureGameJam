using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class ProjectileController : MonoBehaviour
{

    private float movementSpeed = 5.0f;

    private Rigidbody2D rigidBody = default; //used for projectile movement
    private Vector2 direction = new Vector2();
    bool isInit = false;

    // Start is called before the first frame update
    void Start()
    {
        //Find the rigidbody on this object - doesnt need a TryGetComponent as Rigidbody2D is a required component
        rigidBody = GetComponent<Rigidbody2D>();
    }

    public void InitialiseProjectile(Vector2 dir, float speed)
    {
        direction = dir;
        movementSpeed = speed;
        isInit = true;
    }


    private void FixedUpdate()
    {
        if (isInit)
        {
            //Move the character to the right horizontally and apply the vertical velocity
            rigidBody.velocity = movementSpeed * direction * Time.fixedDeltaTime;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Ow");
    }
}

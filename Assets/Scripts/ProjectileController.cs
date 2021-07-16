using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Rigidbody))]
public class ProjectileController : MonoBehaviour
{

    private float movementSpeed = 5.0f;

    private Rigidbody rigidBody = default; //used for projectile movement
    private Vector2 direction = new Vector2();
    bool isInit = false;

    [SerializeField]
    private float lifespan = 2.0f;

    GameObject parent = default;

    // Start is called before the first frame update
    void Start()
    {
        //Find the rigidbody on this object - doesnt need a TryGetComponent as Rigidbody is a required component
        rigidBody = GetComponent<Rigidbody>();
    }

    public void InitialiseProjectile(Vector2 dir, float speed, GameObject p)
    {
        direction = dir;
        movementSpeed = speed;
        isInit = true;
        parent = p;
        Invoke("Cleanup", lifespan);
    }


    private void FixedUpdate()
    {
        
        if (isInit)
        {
            //Move the character to the right horizontally and apply the vertical velocity
            rigidBody.velocity = movementSpeed * direction * Time.fixedDeltaTime;
        }
        if (parent == null)
        {
            Cleanup();
        }
    }

    private void Cleanup()
    {
        Destroy(gameObject);

    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.TryGetComponent<HealthManager>(out HealthManager health))
        {
            health.OnHit();
        }
        else
        {
            EffectsHandler.Instance.SpawnPew(transform.position);
        }

        Destroy(gameObject);
    }
}

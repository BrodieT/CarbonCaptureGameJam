using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.CompareTag("Player"))
        {
            if (other.transform.TryGetComponent<HealthManager>(out HealthManager hp))
            {
                hp.OnHit();
            }


        }
        Destroy(gameObject);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.CompareTag("Player"))
        {
            if (collision.transform.TryGetComponent<HealthManager>(out HealthManager hp))
            {
                hp.OnHit();
            }

           
        }
        Destroy(gameObject);
    }
}

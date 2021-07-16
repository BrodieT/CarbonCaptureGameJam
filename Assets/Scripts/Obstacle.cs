using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
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

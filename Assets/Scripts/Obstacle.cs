using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if(other.TryGetComponent<HealthManager>(out HealthManager hp))
            {
                hp.OnHit();
            }

            Destroy(gameObject);
        }
    }
}

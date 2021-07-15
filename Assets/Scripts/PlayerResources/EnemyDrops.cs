using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDrops : MonoBehaviour
{
    public delegate void OnCollect();
    public static event OnCollect collectCO2Delegate;


    void OnCollected()
    {
        if(collectCO2Delegate != null)
        {
            collectCO2Delegate();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.GetComponent<PlayerController>())
        {
            OnCollected();
            Destroy(gameObject);
        }
    }
}

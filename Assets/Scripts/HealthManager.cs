using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class HealthManager : MonoBehaviour
{
    [SerializeField]
    public int numberOfHits = 3;

    public UnityEvent onHit = default;
    public UnityEvent onDie = default;

    public virtual void OnHit()
    {
        numberOfHits--;
        if(numberOfHits <= 0)
        {
            OnDie();
        }
        else
        {
            onHit.Invoke();
        }
    }

    public void ResetPlayerHealth()
    {
        numberOfHits = 3;
    }
    public virtual void PickupHealth()
    {
        numberOfHits++;
    }

    public virtual void OnDie()
    {
        onDie.Invoke();
    }
}

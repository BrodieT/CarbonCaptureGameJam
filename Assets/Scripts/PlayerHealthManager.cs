using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealthManager : HealthManager
{
    [Header("UI")]
    [SerializeField]
    GameObject healthSegment = default;
    [SerializeField]
    GameObject healthHolder = default;


    int numberOfSegments = 3;

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < numberOfHits; i++)
        {
            AddSegment();
        }

        onHit.AddListener(() => RemoveSegment());
    }



    public void AddSegment()
    {
        Instantiate(healthSegment, healthHolder.transform);
    }

    
    public void RemoveSegment()
    {
        if(healthHolder.transform.childCount > 0)
            Destroy(healthHolder.transform.GetChild(0).gameObject);
    }
}

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


    // Start is called before the first frame update
    void Start()
    {
        if(!healthHolder)
            healthHolder = UIHandler.Instance.playerHealthbar;


        for (int i = 0; i < numberOfHits; i++)
        {
            AddSegment();
        }

        onHit.AddListener(() => RemoveSegment());
    }



    public void AddSegment()
    {
        if(healthSegment && healthHolder)
            Instantiate(healthSegment, healthHolder.transform);
    }


    public void RemoveSegment()
    {
        if (healthSegment && healthHolder)
        {
            if (healthHolder.transform.childCount > 0)
                Destroy(healthHolder.transform.GetChild(0).gameObject);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectables : MonoBehaviour
{
    public static Collectables Instance = default;

    // Start is called before the first frame update
    void Start()
    {
        Instance = this;
    }

    private void Awake()
    {
        EnemyDrops.collectCO2Delegate += CollectedCO2;
    }

    private void OnDestroy()
    {
        EnemyDrops.collectCO2Delegate -= CollectedCO2;
    }

    void CollectedCO2()
    {
        Debug.Log("COLLECTED");
    }
}

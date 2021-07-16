using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectsHandler : MonoBehaviour
{
    public static EffectsHandler Instance = default;

    [SerializeField]
    List<GameObject> allParticleEffects = new List<GameObject>();

    [SerializeField]
    GameObject pew = default;

    [SerializeField]
    GameObject explosion = default;

    GameObject newEffect = default;
    // Start is called before the first frame update
    void Start()
    {
        Instance = this;
    }

    public void SpawnEffect(Vector3 pos)
    {
        newEffect = Instantiate(allParticleEffects[Random.Range(0, allParticleEffects.Count)]);
        newEffect.transform.position = pos;
    }

    public void SpawnPew(Vector3 pos)
    {
        newEffect = Instantiate(pew);
        newEffect.transform.position = pos;
    }

    public void SpawnExplosion(Vector3 pos)
    {
        newEffect = Instantiate(explosion);
        newEffect.transform.position = pos;
    }
}

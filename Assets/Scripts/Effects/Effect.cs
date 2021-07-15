using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Effect : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Invoke("DestroyEffect", 3);
    }

    void DestroyEffect()
    {
        Destroy(this.gameObject);
    }
}

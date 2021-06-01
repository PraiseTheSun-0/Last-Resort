using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ps_destroyOnFinish : MonoBehaviour
{

    private ParticleSystem ps;
    // Start is called before the first frame update
    void Start()
    {
        ps = GetComponentInChildren<ParticleSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        if(ps.isStopped)
        {
            Destroy(gameObject);
        }
    }
}

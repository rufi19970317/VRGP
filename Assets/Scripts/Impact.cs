using System.Buffers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Impact : MonoBehaviour
{
    private ParticleSystem particle;
    private MemoryPool memoryPool;

    // Start is called before the first frame update
    void Awake()
    {
        particle = GetComponent<ParticleSystem>();
    }

    public void Setup(MemoryPool pool)
    {
        memoryPool = pool;
    }

    // Update is called once per frame
    void Update()
    {
        if(!particle.isPlaying)
        {
            memoryPool.DeactivatePoolItem(gameObject);
        }
    }
}

using System.Diagnostics.Tracing;
using UnityEngine;

/*
타격 이펙트 제어 
*/
public class Impact : MonoBehaviour
{
    private ParticleSystem particle;

    private MemoryPool memoryPool;


    private void Awake()
    {
        particle = GetComponent<ParticleSystem>();
    }

    public void SetUp(MemoryPool pool)
    {
        memoryPool = pool;
    }

    private void Update()
    {
        // 파티클이 재생중이 아니면 삭제 
        if (particle.isPlaying == false)
        {
            memoryPool.DeactivatePoolItem(gameObject);
        }
    }




}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;




/*
타격 이펙트 관리
*/

public enum ImpactType { Normal = 0, Obstacle, }
public class ImpactMemoryPool : MonoBehaviour
{
    [SerializeField] private GameObject[] impactPrefab;     // 피격 이펙트
    private MemoryPool[] memoryPool;                        // 피격 이펙트 메모리풀

    private void Awake()
    {
        // 피격 이펙트가 여러종류면 종류별로 MemoryPool 생성
        memoryPool = new MemoryPool[impactPrefab.Length];

        for (int i = 0; i < impactPrefab.Length; i++)
        {
            memoryPool[i] = new MemoryPool(impactPrefab[i]);
        }
    }


    // 부딪힌 오브젝트에 정보에 따라 다르게 처리해주는 메소드 
    public void SpawnImpact(RaycastHit hit)
    {
        if (hit.transform.CompareTag("ImpactNormal"))
        {
            //
        }
    }
}

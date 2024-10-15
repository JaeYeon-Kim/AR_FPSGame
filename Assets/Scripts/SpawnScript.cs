using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
타겟을 생성하는 스크립트 
*/
public class SpawnScript : MonoBehaviour
{
    public Transform[] spawnPoints;     // 생성할 위치 
    public GameObject[] balloons;       // 생성할 오브젝트 프리팹

    [SerializeField] private float respawnTime = 7f;


    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(StartSpawning());
    }

    IEnumerator StartSpawning()
    {
        yield return new WaitForSeconds(respawnTime);

        for (int i = 0; i < 3; i++)
        {
            Instantiate(balloons[i], spawnPoints[i].position, Quaternion.Euler(180f, 0f, 0f));
        }

        StartCoroutine(StartSpawning());
    }

}

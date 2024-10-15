using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BalloonScript : MonoBehaviour
{

    [SerializeField] private float moveSpeed = 0.3f;            // 물체가 이동하는 속도 

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.down * Time.deltaTime * moveSpeed);   // 하늘로 날려보내는 코드 
    }
}

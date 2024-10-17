using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
테스트 용으로 적의 현재 상태를 나타내는 스크립트 
*/
public class Enemy : MonoBehaviour
{
    [SerializeField] private float hp;  // 적의 hp


    // 외부에서 호출하여 적 개체가 데미지를 입도록 하는 메소드 
    public void GetDamage(float damage)
    {
        hp -= damage;

        // 만약 체력이 0이하로 떨어질 경우 사망 후 개체 삭제 
        if (hp <= 0)
        {
            Debug.Log("개체 사망");
            Destroy(this.gameObject);
        }
    }
}

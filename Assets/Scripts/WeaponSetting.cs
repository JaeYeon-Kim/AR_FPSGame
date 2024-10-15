using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// 무기의 정보를 관리하는 Setting 스크립트 
[System.Serializable]       // 구조체를 inspector에 노출 가능 
public struct WeaponSetting
{
    public float attackRate;        // 공격 속도 
    public float attackDistance;    // 공격 사거리 
    public bool isAutomaticAttack;      // 연속 공격 여부 
}

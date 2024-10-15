using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/*
플레이어의 전반적인 처리 담당 하는 스크립트 
*/
public class PlayerController : MonoBehaviour
{
    private AudioSource audioSource;        // 사운드 재생 제어 
    private Weapon weapon;      // 무기를 이용한 공격 제어 
    private PlayerAnimatorController animtor;


    private void Awake()
    {
        animtor = GetComponent<PlayerAnimatorController>();
        audioSource = GetComponent<AudioSource>();
        weapon = GetComponentInChildren<Weapon>();
    }

    private void Update()
    {
        UpdateWeaponAction();
    }

    public void UpdateWeaponAction()
    {
        if (Input.GetMouseButtonDown(0))
        {
            weapon.StartWeaponAction();
        }
        else if (Input.GetMouseButtonUp(0))
        {
            weapon.StopWeaponAction();
        }
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;


/*
무기 사운드 관련 스크립트 
*/
public class Weapon : MonoBehaviour
{
    [Header("Audio Clips")]
    [SerializeField] private AudioClip audioClipTakeOutWeapon;      // 무기 장착 사운드

    [Header("Weapon Setting")]
    [SerializeField]
    private WeaponSetting weaponSetting;        // 무기 설정 
    private float lastAttackTime = 0;   // 마지막 발사시간 체크용


    private AudioSource audioSource;            // 사운드 재생 컴포넌트 
    private PlayerAnimatorController animator;  // 애니메이션 재생 제어 


    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        animator = GetComponentInParent<PlayerAnimatorController>();
    }

    private void OnEnable()
    {
        // 무기 장착 사운드 재생
        PlaySound(audioClipTakeOutWeapon);
    }

    public void StartWeaponAction()
    {
        if (weaponSetting.isAutomaticAttack == true)
        {
            StartCoroutine("OnAttackLoop");
        }

        else
        {
            OnAttack();
        }
    }

    public void StopWeaponAction()
    {
        StopCoroutine("OnAttackLoop");
    }

    private IEnumerator OnAttackLoop()
    {
        // 실제 공격을 수행하는 OnAttack() 메소드를 매 프레임 실행 
        while (true)
        {
            OnAttack();

            yield return null;
        }
    }

    private void OnAttack()
    {
        if (Time.time - lastAttackTime > weaponSetting.attackRate)
        {
            // 공격 주기가 되어야 공격할 수 있도록 하기 위해 현재 시간 저장
            lastAttackTime = Time.time;

            // 무기 애니메이션 재생(같은 애니메이션 반복 시 애니메이션을 끊고 처음부터 다시 재생)
            animator.Play("Fire", -1, 0);
        }
    }

    private void PlaySound(AudioClip clip)
    {
        audioSource.Stop();     // 기존에 재생중인 사운드 정지 
        audioSource.clip = clip;        // 새로운 사운드 clip으로 교체 
        audioSource.Play();     // 사운드 재생 
    }
}

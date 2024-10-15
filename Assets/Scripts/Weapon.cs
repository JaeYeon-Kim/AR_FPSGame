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
    [Header("Fire Effects")]
    [SerializeField]
    private GameObject muzzleFlashEffect;       // 총구 이펙트 (On / Off)


    [Header("Audio Clips")]
    [SerializeField] private AudioClip audioClipTakeOutWeapon;      // 무기 장착 사운드
    [SerializeField]
    private AudioClip audioClipFire;        // 공격 사운드 

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
        // 총구 이펙트 오브젝트 비활성화 
        muzzleFlashEffect.SetActive(false);
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



    private void OnAttack()
    {
        if (Time.time - lastAttackTime > weaponSetting.attackRate)
        {
            // 공격 주기가 되어야 공격할 수 있도록 하기 위해 현재 시간 저장
            lastAttackTime = Time.time;

            // 무기 애니메이션 재생(같은 애니메이션 반복 시 애니메이션을 끊고 처음부터 다시 재생)
            animator.Play("Fire", -1, 0);

            // 총구 이펙트 재생
            StartCoroutine("OnMuzzleFlashEffect");

            // 공격 사운드 재생 
            PlaySound(audioClipFire);
        }
    }

    private void PlaySound(AudioClip clip)
    {
        audioSource.Stop();     // 기존에 재생중인 사운드 정지 
        audioSource.clip = clip;        // 새로운 사운드 clip으로 교체 
        audioSource.Play();     // 사운드 재생 
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

    // 총구 이펙트 활성화 코루틴: 무기의 공격속도 보다 빠르게 muzzleFlashEffect를 잠깐 활성화한 후 비활성화 한다.
    private IEnumerator OnMuzzleFlashEffect()
    {
        muzzleFlashEffect.SetActive(true);

        yield return new WaitForSeconds(weaponSetting.attackRate * 0.3f);

        muzzleFlashEffect.SetActive(false);
    }
}

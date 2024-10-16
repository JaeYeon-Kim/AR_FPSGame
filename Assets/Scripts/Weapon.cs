using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;


/*
무기 관련 스크립트 
*/
[System.Serializable]
public class AmmoEvent : UnityEngine.Events.UnityEvent<int, int> { }



public class Weapon : MonoBehaviour
{
    [HideInInspector]
    public AmmoEvent onAmmoEvent = new AmmoEvent();


    [Header("Fire Effects")]
    [SerializeField]
    private GameObject muzzleFlashEffect;       // 총구 이펙트 (On / Off)


    [Header("Audio Clips")]
    [SerializeField] private AudioClip audioClipTakeOutWeapon;      // 무기 장착 사운드
    [SerializeField] private AudioClip audioClipFire;        // 공격 사운드 

    [SerializeField] private AudioClip audioClipReload;     // 재장전 사운드 

    [Header("Weapon Setting")]
    [SerializeField] private WeaponSetting weaponSetting;        // 무기 설정 
    private float lastAttackTime = 0;   // 마지막 발사시간 체크용
    private bool isReload = false;  // 재장전 중인지 체크 

    private AudioSource audioSource;            // 사운드 재생 컴포넌트 
    private PlayerAnimatorController animator;  // 애니메이션 재생 제어 

    // 외부에서 필요한 무기의 정보를 열람할 수 있도록 하는 프로퍼티 
    public WeaponName WeaponName => weaponSetting.weaponName;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        animator = GetComponentInParent<PlayerAnimatorController>();

        // 처음 탄수는 최대로 설정 
        weaponSetting.currentAmmo = weaponSetting.maxAmmo;
    }

    private void OnEnable()
    {
        // 무기 장착 사운드 재생
        PlaySound(audioClipTakeOutWeapon);
        // 총구 이펙트 오브젝트 비활성화 
        muzzleFlashEffect.SetActive(false);

        // 무기가 활성화 될때 해당 무기의 탄 수 정보를 갱신 한다. 
        onAmmoEvent.Invoke(weaponSetting.currentAmmo, weaponSetting.maxAmmo);
    }

    public void StartWeaponAction()
    {
        // 재장전 중에는 무기 관련 Action 금지 
        if (isReload == true) return;

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

    public void StartReload()
    {
        // 현재 재장전 중이면 재장전 불가능
        if (isReload == true) return;

        // 무기 액션 도중에 'R키를 눌러 재장전을 시도하면 무기 액션 종료 후 재장전
        StopWeaponAction();

        StartCoroutine("OnReload");
    }



    private void OnAttack()
    {
        if (Time.time - lastAttackTime > weaponSetting.attackRate)
        {
            // 공격 주기가 되어야 공격할 수 있도록 하기 위해 현재 시간 저장
            lastAttackTime = Time.time;

            // 탄수가 없으면 공격이 불가능하도록 설정 
            if (weaponSetting.currentAmmo <= 0)
            {
                return;
            }

            // 공격시 현재의 탄알을 1감소 시킴
            weaponSetting.currentAmmo--;

            // 공격으로 탄수가 1감소 했으므로 UI 업데이트를 위해 이벤트 호출 
            onAmmoEvent.Invoke(weaponSetting.currentAmmo, weaponSetting.maxAmmo);

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

    // 재장전 관련 코루틴
    private IEnumerator OnReload()
    {
        isReload = true;

        // 재장전 애니메이션, 사운드 재생
        animator.OnReload();
        PlaySound(audioClipReload);

        while (true)
        {
            // 사운드가 재생중이 아니고, 현재 애니메이션이 Idle이면 재장전 애니메이션이 종료되었다는 뜻 
            if (audioSource.isPlaying == false && animator.CurrentAnimationIs("Idle"))
            {
                isReload = false;

                // 현재 탄수를 최대로 설정하고, 바뀐 탄수 정보를 UI에 업데이트
                weaponSetting.currentAmmo = weaponSetting.maxAmmo;
                onAmmoEvent.Invoke(weaponSetting.currentAmmo, weaponSetting.maxAmmo);

                yield break;
            }

            yield return null;
        }
    }
}

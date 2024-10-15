using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/*
무기 사운드 관련 스크립트 
*/
public class Weapon : MonoBehaviour
{
    [Header("Audio Clips")]
    [SerializeField] private AudioClip audioClipTakeOutWeapon;      // 무기 장착 사운드

    private AudioSource audioSource;


    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void OnEnable()
    {
        // 무기 장착 사운드 재생
        PlaySound(audioClipTakeOutWeapon);
    }

    private void PlaySound(AudioClip clip)
    {
        audioSource.Stop();     // 기존에 재생중인 사운드 정지 
        audioSource.clip = clip;        // 새로운 사운드 clip으로 교체 
        audioSource.Play();     // 사운드 재생 
    }
}

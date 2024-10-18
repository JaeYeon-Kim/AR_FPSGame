using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimatorController : MonoBehaviour
{
    private Animator animator;

    private void Awake()
    {
        animator = GetComponentInChildren<Animator>();
    }


    // 재장전 애니메이션 활성화
    public void OnReload()
    {
        animator.SetTrigger("onReload");
    }

    // 외부에서 호출하는 스크립트
    public void Play(string stateName, int layer, float normalizedTime)
    {
        animator.Play(stateName, layer, normalizedTime);
    }


    // name으로 받아온 Animation이 현재 재생중인지 확인하고 그에 맞게 bool값 return
    public bool CurrentAnimationIs(string name)
    {
        return animator.GetCurrentAnimatorStateInfo(0).IsName(name);
    }
}

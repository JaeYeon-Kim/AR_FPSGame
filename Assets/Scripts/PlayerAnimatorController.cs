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

    // 외부에서 호출하는 스크립트
    public void Play(string stateName, int layer, float normalizedTime)
    {
        animator.Play(stateName, layer, normalizedTime);
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using Microsoft.Unity.VisualStudio.Editor;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

/*
플레이어의 정보를 UI로 출력하는 스크립트 
*/
public class PlayerUI : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private Weapon weapon; // 현재 정보가 출력되는 무기 

    [Header("Weapon Base")]
    [SerializeField] private TextMeshProUGUI textWeaponName;    // 무기 이름 
    [SerializeField] private UnityEngine.UI.Image imageWeaponIcon;         // 무기 아이콘

    [SerializeField] private Sprite[] spriteWeaponIcons;    // 무기 아이콘에 사용되는 sprite 배열

    [Header("Ammo")]
    [SerializeField] private TextMeshProUGUI textAmmo;      // 현재 / 최대 탄수 출력 Text  

    private void Awake()
    {
        SetupWeapon();

        // 이벤트 클래스에 메소드 등록
        weapon.onAmmoEvent.AddListener(UpdateAmmoHUD);
    }

    private void SetupWeapon()
    {
        textWeaponName.text = weapon.WeaponName.ToString();
        imageWeaponIcon.sprite = spriteWeaponIcons[(int)weapon.WeaponName];
    }

    private void UpdateAmmoHUD(int currentAmmo, int maxAmmo)
    {
        textAmmo.text = $"<size=40>{currentAmmo}/</size>{maxAmmo}";
    }
}

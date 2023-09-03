using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum WeaponName { Rifle, }

[System.Serializable]
public struct WeaponSet
{
    public WeaponName weaponName;   // 무기이름
    public int currentMagazine;
    public int maxMagazine;
    public int currentAmmo;         // 현재 탄약 수
    public int maxAmmo;             // 최대 탄약 수
    public int attackRate;          // 공격 속도
    public int attackDistance;      // 공격 사거리
    public bool isAutomaticAttack;  // 연속 공격 여부
}

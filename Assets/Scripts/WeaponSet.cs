using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum WeaponName { Rifle, }

[System.Serializable]
public struct WeaponSet
{
    public WeaponName weaponName;   // �����̸�
    public int currentMagazine;
    public int maxMagazine;
    public int currentAmmo;         // ���� ź�� ��
    public int maxAmmo;             // �ִ� ź�� ��
    public int attackRate;          // ���� �ӵ�
    public int attackDistance;      // ���� ��Ÿ�
    public bool isAutomaticAttack;  // ���� ���� ����
}

using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUI : MonoBehaviour
{
    [Header("Components")]
    [SerializeField]
    private WeaponAssultRifle weapon;

    [Header("Ammo")]
    [SerializeField]
    private TextMeshProUGUI textAmmo;

    [Header("Magazine")]
    [SerializeField]
    private TextMeshProUGUI textMagazine;


    [SerializeField]
    bool[] canUseSkill = new bool[4];

    [SerializeField]
    Image[] skillImage = new Image[4];

    private void Awake()
    {
        weapon.onAmmoEvent.AddListener(UpdateAmmoHUD);
        weapon.onMagazineEvent.AddListener(UpdateMagazineHUD);
    }

    private void UpdateAmmoHUD(int currentAmmo, int maxAmmo)
    {
        textAmmo.text = $"{currentAmmo}/{maxAmmo}";
    }

    private void UpdateMagazineHUD(int currentMagazine)
    {
        textMagazine.text = $"<size=40>{currentMagazine}";
    }

    public PlayerSkill.Skill UseSkill(PlayerSkill.Skill skill)
    {
        if (canUseSkill[(int)skill])
        {
            canUseSkill[(int)skill] = !canUseSkill[(int)skill];
            skillImage[(int)skill].color = Color.gray;
            return skill;
        }

        else
        {
            return skill;
        }
    }
}

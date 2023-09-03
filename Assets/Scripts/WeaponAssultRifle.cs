using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class AmmoEvent : UnityEngine.Events.UnityEvent<int, int> { }
[System.Serializable]
public class MagazineEvent : UnityEngine.Events.UnityEvent<int> { }

public class WeaponAssultRifle : MonoBehaviour
{
    [HideInInspector]
    public AmmoEvent onAmmoEvent = new AmmoEvent();
    [HideInInspector]
    public MagazineEvent onMagazineEvent = new MagazineEvent();

    [SerializeField]
    private Transform bulletSpawnPoint;

    [Header("Weapon Setting")]
    [SerializeField]
    private WeaponSet weaponSet;

    private float lastAttackTime = 0;

    bool isReload = false;

    public WeaponName WeaponName => weaponSet.weaponName;
    public int CurrentMagazine => weaponSet.currentMagazine;
    public int MaxMagazine => weaponSet.maxMagazine;

    private ImpactMemoryPool impactMemoryPool;
    private Camera mainCamera;

    // Start is called before the first frame update
    void Start()
    {
        weaponSet.currentAmmo = weaponSet.maxAmmo;
        weaponSet.currentMagazine = weaponSet.maxMagazine;


        onAmmoEvent.Invoke(weaponSet.currentAmmo, weaponSet.maxAmmo);
        onMagazineEvent.Invoke(weaponSet.currentMagazine);

        impactMemoryPool = GetComponent<ImpactMemoryPool>();
        mainCamera = Camera.main;
    }

    public void StartWeaponAction(int type = 0)
    {
        if (isReload) return;

        if ( type == 0 )
        {
            if ( weaponSet.isAutomaticAttack )
            {
                StartCoroutine("OnAttackLoop");
            }
            else
            {
                OnAttack();
            }
        }
    }

    public void StopWeaponAction(int type = 0)
    {
        if (type == 0)
        {
            StopCoroutine("OnAttackLoop");
        }
    }

    public void StartReload()
    {
        if (isReload || weaponSet.currentMagazine <= 0) return;

        StopWeaponAction();
        StartCoroutine("OnReload");
    }

    private IEnumerator OnReload()
    {
        isReload = true;
        
        yield return new WaitForSecondsRealtime(1f);

        weaponSet.currentMagazine--;
        onMagazineEvent.Invoke(weaponSet.currentMagazine);

        weaponSet.currentAmmo = weaponSet.maxAmmo;
        onAmmoEvent.Invoke(weaponSet.currentAmmo, weaponSet.maxAmmo);

        isReload = false;

        yield return null;
    }

    private IEnumerator OnAttackLoop()
    {
        while ( true )
        {
            OnAttack();

            yield return null;
        }
    }

    public void OnAttack()
    {
        if (weaponSet.currentAmmo <= 0)
            return;


        if (Time.time - lastAttackTime > weaponSet.attackRate)
        {
            weaponSet.currentAmmo--;
            onAmmoEvent.Invoke(weaponSet.currentAmmo, weaponSet.maxAmmo);

            lastAttackTime = Time.time;

            TwoStepRayCast();
        }
    }

    private void TwoStepRayCast()
    {
        Ray ray;
        RaycastHit hit;
        Vector3 targetPoint = Vector3.zero;

        ray = mainCamera.ViewportPointToRay(Vector2.one * 0.5f);

        if (Physics.Raycast(ray, out hit, weaponSet.attackDistance))
            targetPoint = hit.point;
        else
            targetPoint = ray.origin + ray.direction * weaponSet.attackDistance;

        Debug.DrawRay(ray.origin, ray.direction * weaponSet.attackDistance, Color.red);

        Vector3 attackDirection = (targetPoint - bulletSpawnPoint.position).normalized;
        if (Physics.Raycast(bulletSpawnPoint.position, attackDirection, out hit, weaponSet.attackDistance))
            impactMemoryPool.SpawnImpact(hit);

        Debug.DrawRay(bulletSpawnPoint.position, attackDirection*weaponSet.attackDistance, Color.blue);
    }
}

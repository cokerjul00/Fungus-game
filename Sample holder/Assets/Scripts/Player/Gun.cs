using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Gun : MonoBehaviour
{
    public Transform LazerCutterbulletSpawnPoint;
    public Transform PulseRiflebulletSpawnPoint;

    public GameObject bulletPrefab;
    public GameObject KnifePrefab;
    public GameObject lazerCutter;
    public GameObject pulseRifle;
    public GameObject FlameThrower;
    public GameObject FlameRadius;
    public GameObject ForceGun;

    public bool flameactive;
    public float bulletSpeed;
    public AudioSource gun;
    public AudioClip lazerCutterClip;
    public AudioClip pulseRifleClip;
    public AudioClip ForceGunFireClip;
    public float knifespeed;
    public int SaveEquip;
    public bool MelleSwap;
    public GameObject ForcebulletPrefab;
    public Transform ForceGunProjectileSpawn;
    public float BulletLifeSpan;
    public float FlameTimer = 5f;
    public float FlameCooldown = 20f;
    public bool FireFuel = true;

    public bool isFiring = false;
    Coroutine automaticFireCoroutine;

    public float fireRate = 0.1f;

    public int weaponSelected = 0;

    PlayerControls controls;

    Gamepad gamepad;

    // Start is called before the first frame update
    void Start()
    {
        weaponSelected = 0;
        SetActiveWeapon();
        KnifePrefab.SetActive(false);
    }

    private void Update()
    {
        if(FlameCooldown <= 0)
        {
            FireFuel = true;
            FlameTimer = 2;
            FlameCooldown = 10;
        }

        if (FlameTimer <= 0)
        {
            if (FireFuel)
            {FireFuel = false;
                FlameRadius.SetActive(false);

            }

            FlameCooldown -= Time.deltaTime;
        }

        if (flameactive == true && isFiring && FireFuel)
        {
            FlameRadius.SetActive(true);
            if (FlameTimer >= 0)
                FlameTimer -= Time.deltaTime;
        }

        if (flameactive == false || isFiring == false)
        {
            FlameRadius.SetActive(false);
            
        }


        if (knifespeed >= 0 && MelleSwap == true)
        {
            knifespeed -= Time.deltaTime;
        }
        else if (knifespeed <= 0 && MelleSwap == true)
        {
            KnifePrefab.SetActive(false);
            weaponSelected = SaveEquip;
            SetActiveWeapon();
            MelleSwap = false;
        }

    }

    private void Awake()
    {
        controls = new PlayerControls();

        // Use .canceled event to detect when the Shoot button is released
        controls.Gameplay.Shoot.started += ctx => PlayerShoot();
        controls.Gameplay.Shoot.canceled += ctx => StopPlayerShoot();
        controls.Gameplay.WeaponSwaping.started += ctx => WeaponSwap();
        controls.Gameplay.Melee.started += ctx => melee();
    }

    void melee()
    {
        MelleSwap = true;
        SaveEquip = weaponSelected;
        KnifePrefab.SetActive(true);
        knifespeed = .1f;
        weaponSelected = 0;
        SetActiveWeapon();
        
    }

    void WeaponSwap()
    {
        // Change weapon to the next one
        weaponSelected++;
        if (weaponSelected >= 5)
        {
            weaponSelected = 0;
            
        }
        SetActiveWeapon();
    }

    void SetActiveWeapon()
    {
        switch (weaponSelected)
        {
            case 0:
                FlameThrower.SetActive(false);
                lazerCutter.SetActive(false);
                pulseRifle.SetActive(false);
                flameactive = false;
                ForceGun.SetActive(false);
                break;
            case 1:
                FlameThrower.SetActive(false);
                lazerCutter.SetActive(true);
                pulseRifle.SetActive(false);
                flameactive = false;
                ForceGun.SetActive(false);
                break;
            case 2:
                FlameThrower.SetActive(false);
                lazerCutter.SetActive(false);
                pulseRifle.SetActive(true);
                flameactive = false;
                ForceGun.SetActive(false);
                break;
            case 3:
                FlameThrower.SetActive(true);
                lazerCutter.SetActive(false);
                pulseRifle.SetActive(false);
                flameactive = true;
                ForceGun.SetActive(false);
                break;
            case 4:
                FlameThrower.SetActive(false);
                lazerCutter.SetActive(false);
                pulseRifle.SetActive(false);
                flameactive = false;
                ForceGun.SetActive(true);
                break;
        }
    }

    IEnumerator ShootFullyAutomatic()
    {
        while (isFiring)
        {
            var bullet = Instantiate(bulletPrefab, PulseRiflebulletSpawnPoint.position, PulseRiflebulletSpawnPoint.rotation);
            bullet.GetComponent<Rigidbody>().velocity = PulseRiflebulletSpawnPoint.up * bulletSpeed;
            yield return new WaitForSeconds(fireRate);
            Destroy(bullet, BulletLifeSpan);

            gun.clip = pulseRifleClip;
            gun.Play();
        }
        automaticFireCoroutine = null;
    }

    void PlayerShoot()
    {
        if (weaponSelected == 4)
        {
            var bullet = Instantiate(ForcebulletPrefab, ForceGunProjectileSpawn.position, ForceGunProjectileSpawn.rotation);
            bullet.GetComponent<Rigidbody>().velocity = ForceGunProjectileSpawn.right * bulletSpeed;
            Destroy(bullet, BulletLifeSpan);
            gun.clip = ForceGunFireClip;
            gun.Play();

        }


        if (weaponSelected == 3)
        {
            if (!isFiring)
            {
                isFiring = true;
            }

        }


        if (weaponSelected == 2)
        {
            // Start automatic firing
            if (!isFiring)
            {
                isFiring = true;
                automaticFireCoroutine = StartCoroutine(ShootFullyAutomatic());
            }
        }
        if (weaponSelected == 1)
        {
            var bullet = Instantiate(bulletPrefab, LazerCutterbulletSpawnPoint.position, LazerCutterbulletSpawnPoint.rotation);
            bullet.GetComponent<Rigidbody>().velocity = LazerCutterbulletSpawnPoint.up * bulletSpeed;
            
            gun.clip = lazerCutterClip;
            gun.Play();
            Destroy(bullet, BulletLifeSpan);
        }
    }

    void StopPlayerShoot()
    {

        if (weaponSelected == 3)
        {
            isFiring = false;
        }

        if (weaponSelected == 2)
        {
            // Stop automatic firing
            isFiring = false;
            if (automaticFireCoroutine != null)
            {
                StopCoroutine(automaticFireCoroutine);
                automaticFireCoroutine = null;
            }
        }
    }

    private void OnEnable()
    {
        controls.Gameplay.Enable();
    }

    private void OnDisable()
    {
        controls.Gameplay.Disable();
    }
}

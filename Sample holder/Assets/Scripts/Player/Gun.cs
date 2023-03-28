using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Gun : MonoBehaviour
{
    public Transform LazerCutterbulletSpawnPoint;
    public Transform PulseRiflebulletSpawnPoint;
    public Transform meleePoint;
    public GameObject bulletPrefab;
    public GameObject KnifePrefab;
    public GameObject lazerCutter;
    public GameObject pulseRifle;
    public GameObject FlameThrower;
    public GameObject FlameRadius;
    public bool flameactive;
    public float bulletSpeed;
    public AudioSource gun;
    public AudioClip lazerCutterClip;
    public AudioClip pulseRifleClip;

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
    }

    private void Update()
    {
        if (flameactive == true && isFiring)
        {
            FlameRadius.SetActive(true);
        }

        if (flameactive == false || isFiring == false)
        {
            FlameRadius.SetActive(false);
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
        Instantiate(KnifePrefab, meleePoint.position, meleePoint.rotation);
        Destroy(gameObject);
    }

    void WeaponSwap()
    {
        // Change weapon to the next one
        weaponSelected++;
        if (weaponSelected >= 4)
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
                break;
            case 1:
                FlameThrower.SetActive(false);
                lazerCutter.SetActive(true);
                pulseRifle.SetActive(false);
                flameactive = false;
                break;
            case 2:
                FlameThrower.SetActive(false);
                lazerCutter.SetActive(false);
                pulseRifle.SetActive(true);
                flameactive = false;
                break;
            case 3:
                FlameThrower.SetActive(true);
                lazerCutter.SetActive(false);
                pulseRifle.SetActive(false);
                flameactive = true;
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
            gun.clip = pulseRifleClip;
            gun.Play();
        }
        automaticFireCoroutine = null;
    }

    void PlayerShoot()
    {

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

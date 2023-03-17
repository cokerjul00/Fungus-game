using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Gun : MonoBehaviour
{
    public Transform bulletSpawnPoint;
    public GameObject bulletPrefab;
    public float bulletSpeed;
    public AudioSource Lasercutter;
    public AudioClip lasercutterclip;

    bool isFiring = false;
    Coroutine automaticFireCoroutine;

    public float fireRate = 0.1f;

    public int WeaponSelected = 0;

    PlayerControls controls;

    Gamepad gamepad;

    // Start is called before the first frame update
    void Start()
    {
        WeaponSelected = 0;
    }

    private void Awake()
    {
        controls = new PlayerControls();

        // Use .canceled event to detect when the Shoot button is released
        controls.Gameplay.Shoot.started += ctx => PlayerShoot();
        controls.Gameplay.Shoot.canceled += ctx => StopPlayerShoot();
        controls.Gameplay.WeaponSwaping.started += ctx => WeaponSwap();
    }

    void WeaponSwap()
    {
        // Change weapon to the next one
        WeaponSelected++;
        if (WeaponSelected >= 6)
        {
            WeaponSelected = 0;
        }
        if (WeaponSelected == 2 && isFiring)
        {
            // Stop automatic firing when switching to the grenade launcher
            isFiring = false;
            if (automaticFireCoroutine != null)
            {
                StopCoroutine(automaticFireCoroutine);
                automaticFireCoroutine = null;
            }
        }
    }

    IEnumerator ShootFullyAutomatic()
    {
        while (isFiring)
        {
            var bullet = Instantiate(bulletPrefab, bulletSpawnPoint.position, bulletSpawnPoint.rotation);
            bullet.GetComponent<Rigidbody>().velocity = bulletSpawnPoint.up * bulletSpeed;
            yield return new WaitForSeconds(fireRate);
        }
        automaticFireCoroutine = null;
    }

    void PlayerShoot()
    {
        if (WeaponSelected == 2)
        {
            // Start automatic firing
            if (!isFiring)
            {
                isFiring = true;
                automaticFireCoroutine = StartCoroutine(ShootFullyAutomatic());
            }
        }
        else if (WeaponSelected == 1)
        {
            var bullet = Instantiate(bulletPrefab, bulletSpawnPoint.position, bulletSpawnPoint.rotation);
            bullet.GetComponent<Rigidbody>().velocity = bulletSpawnPoint.up * bulletSpeed;
            Lasercutter.clip = lasercutterclip;
            Lasercutter.Play();
        }
    }

    void StopPlayerShoot()
    {
        if (WeaponSelected == 2)
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

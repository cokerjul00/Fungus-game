using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    public Transform bulletSpawnPoint;
    public GameObject bulletPrefab;
    public float bulletSpeed;
    public AudioSource Lasercutter;
    public AudioClip lasercutterclip;

    PlayerControls controls;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void Awake()
    {

        controls = new PlayerControls();

        controls.Gameplay.Shoot.performed += ctx => PlayerShoot();

    }



    void PlayerShoot()
    {
            var bullet = Instantiate(bulletPrefab, bulletSpawnPoint.position, bulletSpawnPoint.rotation);
            bullet.GetComponent<Rigidbody>().velocity = bulletSpawnPoint.up * bulletSpeed;

        Lasercutter.clip = lasercutterclip;
        Lasercutter.Play();
       
    }



    // Update is called once per frame
    void Update()
    {

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

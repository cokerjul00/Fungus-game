
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LobsterClaw : MonoBehaviour
{
    public float knifeDamage;
    public float Damage = 2;
    public float DamageTimer;
    public float PlayerDamageTimer;
    public float Health = 10f;
    public GameObject Player;
    public float speed;
    public float thrust = 1.0f;
    public Rigidbody rb;
    public GameObject SporeBlastPrefab;
    public Transform SporeSpawn;
    public float sporeSpeed;
    public float fireRate = 1f;
    public bool CanShoot = false;
    public float BulletLifeSpan = 2;


    private float distance;





    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();


    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Fire")
        {

            if (DamageTimer <= 0)
            {
                Health -= Damage;
                DamageTimer = 0.1f;
            }
        }
        if (CanShoot == true)
        {
            var Spore = Instantiate(SporeBlastPrefab, SporeSpawn.position, SporeSpawn.rotation);
            Spore.GetComponent<Rigidbody>().velocity = SporeSpawn.up * sporeSpeed;
            fireRate = 1f;
            CanShoot = false;
            Destroy(Spore, BulletLifeSpan);
        }

    }



    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Bullet")
        {
            Destroy(collision.gameObject);
            Health--;

        }



    }
   

    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {

            if (PlayerDamageTimer <= 0)
            {
                collision.gameObject.GetComponent<Player>().Health -= 25;
                PlayerDamageTimer = 1;
            }


        }



    }

    


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Knife")
        {

            Health -= knifeDamage;

        }

        if (other.gameObject.tag == "Force")
        {
            Debug.Log("hello");
            rb.AddRelativeForce(Vector3.right * thrust);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(CanShoot == false && fireRate >= 0)
        {
            fireRate -= Time.deltaTime;
            
        }
        if(fireRate <= 0 && CanShoot == false)
        {
            CanShoot = true;
        }

        distance = Vector2.Distance(transform.position, Player.transform.position);
        Vector2 direction = Player.transform.position - transform.position;

        transform.position = Vector2.MoveTowards(transform.position, Player.transform.position, speed * Time.deltaTime);

        if (Health <= 0)
        {
            Destroy(gameObject);
        }

        if (DamageTimer >= 0)
        {
            DamageTimer -= Time.deltaTime;
        }

        if (PlayerDamageTimer >= 0)
        {
            PlayerDamageTimer -= Time.deltaTime;
        }
    }
}

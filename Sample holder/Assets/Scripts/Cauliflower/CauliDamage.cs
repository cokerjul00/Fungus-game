using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CauliDamage : MonoBehaviour
{

    public int damage;
    public Player player;



    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            player.TakeDamage(damage);
        }
    }



}

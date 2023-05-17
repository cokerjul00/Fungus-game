using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SporeScript : MonoBehaviour
{
    public float SporeHealth = 10;


    // Start is called before the first frame update
    void Start()
    {

    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Destroy(gameObject);

        }

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Knife")
        {
            Destroy(gameObject);

        }
        if (other.gameObject.tag == "Fire")
        {
            Destroy(gameObject);

        }
    }

    // Update is called once per frame
    void Update()
    {
    
    }
}


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CauliMove : MonoBehaviour
{


    public float Health = 10f;
    public GameObject Player;
    public float speed;

    private float distance;





    // Start is called before the first frame update
    void Start()
    {

    }



    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Bullet")
        {
            Health--;
        }
    }

    // Update is called once per frame
    void Update()
    {
        distance = Vector2.Distance(transform.position, Player.transform.position);
        Vector2 direction = Player.transform.position - transform.position;

        transform.position = Vector2.MoveTowards(transform.position, Player.transform.position, speed * Time.deltaTime);

        if (Health <= 0)
        {
            Destroy(gameObject);
        }
    }
}

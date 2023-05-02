using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PolyMovement : MonoBehaviour
{
    public bool SporeDeploy;

    public ParticleSystem Spores;

    public Transform PolyEnemySpawn;
    public Transform PolyEnemySpawn1;

    public GameObject EnemyPrefab;


    // Start is called before the first frame update
    void Start()
    {
        SporeDeploy = false;
        Spores.Stop();
    }

    // Update is called once per frame
    void Update()
    {
       
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {

            Instantiate(EnemyPrefab, PolyEnemySpawn.position, PolyEnemySpawn.rotation);

            Instantiate(EnemyPrefab, PolyEnemySpawn1.position, PolyEnemySpawn1.rotation);
        }
      
    }

    private void OnTriggerExit(Collider other)
    {
       
        

    }



}

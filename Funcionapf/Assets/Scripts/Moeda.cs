using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Moeda : MonoBehaviour
{
    

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
       // Network.instance.SendData("pos;" + $"{transform.position.x}.{transform.position.y}.{transform.position.z}");
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("player1"))
        {
            Pontos.moeda1 += 10;
            transform.position = new Vector3(transform.position.x,10f,transform.position.z);
            Network.instance.SendData("pos;" + $"{transform.position.x}.{transform.position.y}.{transform.position.z}");

        }

        if (col.gameObject.CompareTag("enemy"))
        {
            Pontos.moeda2 += 10;
            transform.position = new Vector3(transform.position.x,10f,transform.position.z);
            Network.instance.SendData("pos;" + $"{transform.position.x}.{transform.position.y}.{transform.position.z}");

        }
    }

    
}


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chegada : MonoBehaviour
{
    public int voltasp, voltase ;
    public GameObject[] moedas;

    

    // Start is called before the first frame update
    void Start()
    {
      
        voltasp = 0;
        voltase = 0;
    }

    // Update is called once per frame
    void Update()
    {
        Network.instance.SendData("voltas;" + $"{voltasp}.{voltase}");
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("player1"))
        {
            voltasp++;
            
            AtualizarMoedas();

        }

        if (col.gameObject.CompareTag("enemy"))
        {
            voltase++;
            
            AtualizarMoedas();

        }
    }

    public void AtualizarMoedas()
    {
        for(int i = 0; i< 7; i++)
        {
            moedas[i].transform.position = new Vector3(Random.Range(-8.06f,8), Random.Range(2.30f,4),1);
            Network.instance.SendData("pos;" + $"{moedas[i].transform.position.x}.{moedas[i].transform.position.y}.{moedas[i].transform.position.z}");
        }
    }
}

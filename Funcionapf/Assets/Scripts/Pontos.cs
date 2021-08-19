using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Pontos : MonoBehaviour
{

    public Text p1;
    public Text p2;


    public static int moeda1;
    public static int moeda2;


    void Start()
    {
        moeda1 = 0;
        moeda2 = 0;


    }

    // Update is called once per frame
    void Update()
    {
        p1.text = "Your coins : " + moeda1;
        p2.text = "Enemy coins : " + moeda2;

        

        Network.instance.SendData("pontos;" + $"{moeda1}.{moeda2}");
        

        
    }
}

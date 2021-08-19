using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        transform.position = new Vector3(-7.57f,-0.55f,0);
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Network.instance.posicao;
        transform.transform.eulerAngles = Network.instance.rotacao;
    }

    private void LateUpdate()
    {
        
    }
}

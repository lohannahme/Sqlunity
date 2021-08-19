using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    float dfact = 0.95f;

    float acelera = 10;
    float desAcelera = 1.5f;

    float accInput = 0;
    float steInput = 0;

    float rotAng = 0;

    Rigidbody2D rigid;

    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
    }
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Network.instance.SendData("pos;" + $"{transform.position.x}.{transform.position.y}.{transform.position.z}");
        Network.instance.SendData("rot;" + $"{transform.eulerAngles.x}.{transform.eulerAngles.y}.{transform.eulerAngles.z}");

    }

    void FixedUpdate()
    {
        Aceleracao();

        NaoDerrapar();

        Direcao();
    }

    void NaoDerrapar()
    {
        Vector2 fowardv = transform.up * Vector2.Dot(rigid.velocity, transform.up);
        Vector2 rightv = transform.right * Vector2.Dot(rigid.velocity, transform.right);

        rigid.velocity = fowardv + rightv * dfact;
    }

    void Aceleracao()
    {
        Vector2 acelVect = transform.up * accInput * acelera;

        rigid.AddForce(acelVect, ForceMode2D.Force);
    }

    void Direcao()
    {
        rotAng -= steInput * desAcelera;

        rigid.MoveRotation(rotAng);
    }

    public void SetInputVec(Vector2 inputVect)
    {
        steInput = inputVect.x;
        accInput = inputVect.y;
    }
}

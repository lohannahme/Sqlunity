using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Register : MonoBehaviour
{
    [Header("Setup")]
    [SerializeField]
    private InputField username;
    [Header("Setup2")]
    [SerializeField]
    private InputField password;



    // Start is called before the first frame update
    void Start()
    {


    }
    public void Registrar()
    {
        if (string.IsNullOrEmpty(username.text))
        {
            username.text = "Username can't be empty";
        }
        if (string.IsNullOrEmpty(password.text))
        {
            password.text = "Password can't be empty";
        }

        if (Network.instance == null)
        {
            return;

        }
      

       Network.instance.ConnectGameServer(username.text, password.text, true); 
    }



    // Update is called once per frame
    void Update()
    {

    }
}

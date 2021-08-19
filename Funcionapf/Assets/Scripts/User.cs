using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class User 
{
    private string username;
    private string password;

    public void SetUsername(string name)
    {
        username = name;
    }

    public string GetUsername()
    {
        return username;
    }

    public void SetPassword(string _password)
    {
        password = _password ;
    }

    public string GetPassword()
    {
        return password;
    }
}

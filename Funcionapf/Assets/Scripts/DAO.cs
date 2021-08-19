using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Data.Sql;
using System.Data.SqlClient;
using UnityEngine.UI;

public class DAO : MonoBehaviour
{
    private string cs;
    public Text text;
    public Text texto;


    private void Start()
    {
        cs = @"Data Source =(local), 1433; Initial Catalog = EdTrab; User ID =sa; Password =adorolilika1;";
    }

    public void Connected()
    {
        SqlConnection SqlConn = new SqlConnection(cs);
        SqlConn.Open();
        SqlCommand cmd = new SqlCommand("INSERT INTO PLAYERS (login, password)" + " VALUES('" +text.text+ "','" + texto.text + "')", SqlConn);
        cmd.ExecuteNonQuery();
        

        SqlConn.Close();

    }

    


}


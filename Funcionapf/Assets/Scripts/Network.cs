using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Net.Sockets;
using System.IO;
using System;
using System.Text;
using UnityEngine.SceneManagement;


public enum Networkstatus
{
    RIGHT,
    ERROR,

}

public class Network : MonoBehaviour
{
    public static Network instance;

    [Header("Network Settings")]
    public string ServerIP = "127.0.0.1";
    public int ServerPort = 5500;
    public bool isConnected;

    public TcpClient PlayerSocket;
    public NetworkStream myStream;
    public StreamReader myReader;
    public StreamWriter myWriter;

    private byte[] asnycBuff;
    public bool shouldHandleData;

    string username;
    string password;
    private bool register;
    private bool changescene = false;

    public string rank;

    public int index;

    public Vector3 posicao;
    public Vector3 rotacao;
    

    public int pontos1, pontos2, voltas1, voltas2;

    private void Awake()
    {
        
            DontDestroyOnLoad(gameObject);
            changescene = false;
            instance = this;
      

    }


    private void Update()
    {

        if (changescene)
        {
            SendData(username);
            SceneManager.LoadScene(1);
            changescene = false;
        }
    }

    public void ConnectGameServer(string username, string password, bool register)
    {
        if (PlayerSocket != null)
        {
            if (PlayerSocket.Connected || isConnected)
            {

                return;
            }
            PlayerSocket.Close();
            PlayerSocket = null;

        }
        PlayerSocket = new TcpClient();// 
        PlayerSocket.ReceiveBufferSize = 4096;
        PlayerSocket.SendBufferSize = 4096;
        PlayerSocket.NoDelay = false;
        Array.Resize(ref asnycBuff, 8192);
        this.username = username;
        this.password = password;
        this.register = register;
        rank = string.Empty;

        PlayerSocket.BeginConnect(ServerIP, ServerPort, new AsyncCallback(ConnectCallback), PlayerSocket); // callback chamado

        isConnected = true;
        MenuManager.instance._menu = MenuManager.Menu.Home;




    }

    public void SendData(string message) // essa string vai pro onreceivedata no servidor
    {
        byte[] byts = Encoding.ASCII.GetBytes(message);
        myStream.WriteAsync(byts, 0, byts.Length);
    }

    void ConnectCallback(IAsyncResult result)
    {
        if (PlayerSocket != null)
        {
            PlayerSocket.EndConnect(result);
            if (PlayerSocket.Connected == false)
            {
                isConnected = false;
                Debug.Log("Client disconnect");
                return;

            }
            else
            {
                PlayerSocket.NoDelay = true;
                myStream = PlayerSocket.GetStream();
                char c = register ? 'r' : 'l';
                SendData($"{c};{username};{password}");
                changescene = false;
                myStream.BeginRead(asnycBuff, 0, 8192, OnStart, null);
            }
        }

    }
    void OnStart(IAsyncResult result)
    {
        int tamanhoMessage = myStream.EndRead(result);
        byte[] myBytes = null;
        Array.Resize(ref myBytes, tamanhoMessage);
        Buffer.BlockCopy(asnycBuff, 0, myBytes, 0, tamanhoMessage);

        if (tamanhoMessage == 0)
        {
            Debug.Log("You got disconnected from the server. Login problem");
            PlayerSocket.Close();
            return;
        }


        string sa = Encoding.ASCII.GetString(myBytes);
        Debug.Log(sa);

        if (sa.Contains(Networkstatus.RIGHT.ToString()))
        {
            if (PlayerSocket == null)
                return;
            
            string[] exemplo = sa.Split(';');
            Debug.Log(exemplo[1]);

            index = int.Parse(exemplo[1]);

            myStream.BeginRead(asnycBuff, 0, 8192, OnReceive, null);
            changescene = true;
        }
        else
        {
            myStream.BeginRead(asnycBuff, 0, 8192, OnStart, null);
        }

    }

    void OnReceive(IAsyncResult result)
    {
        if (PlayerSocket != null)
        {
            if (PlayerSocket == null)
                return;

            int tamanhoMessage = myStream.EndRead(result);
            byte[] myBytes = null;
            Array.Resize(ref myBytes, tamanhoMessage);
            Buffer.BlockCopy(asnycBuff, 0, myBytes, 0, tamanhoMessage);

            if (tamanhoMessage == 0)
            {
                Debug.Log("You got disconnected from the server.");
                PlayerSocket.Close();
                return;
            }


            //HandleData
            string sa = Encoding.ASCII.GetString(myBytes);
            if (sa.Contains("ranking"))
            {
                rank = sa;
            }

            else if (sa.Contains("pos"))
            {
                string[] d = sa.Split(';')[1].Split('.');

                try
                {
                    posicao.x = float.Parse(d[0]);
                    posicao.y = float.Parse(d[1]);
                    posicao.z = float.Parse(d[2]);
                }
                catch (Exception e)
                {
                    Debug.Log($"Pos Erro: {e}");
                }
            }
            else if (sa.Contains("rot"))
            {
                string[] d = sa.Split(';')[1].Split('.');

                try
                {
                    rotacao.x = float.Parse(d[0]);
                    rotacao.y = float.Parse(d[1]);
                    rotacao.z = float.Parse(d[2]);
                }
                catch (Exception e)
                {
                    Debug.Log($"Rot Erro: {e}");
                }
            }
            else if (sa.Contains("pontos"))
            {
                string[] d = sa.Split(';')[1].Split('.');

                try
                {
                    pontos1 = int.Parse(d[0]);
                    pontos2 = int.Parse(d[1]);
                }
                catch (Exception e)
                {
                    Debug.Log($"Pontos erro: {e}");
                }
            }
            else if (sa.Contains("voltas"))
            {
                string[] d = sa.Split(';')[1].Split('.');

                try
                {
                    voltas1 = int.Parse(d[0]);
                    voltas2 = int.Parse(d[1]);

                }
                catch (Exception e)
                {
                    Debug.Log($"Voltas erro : {e}");
                }
            }
            
            else { Debug.Log(sa); }




            if (PlayerSocket == null)
                return;
            myStream.BeginRead(asnycBuff, 0, 8192, OnReceive, null);
        }
    }

}

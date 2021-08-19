using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HStable : MonoBehaviour
{
    private Transform eCont;
    private Transform eTempla;
    private List<HighscoreEntry> highscoreEntryList;
    private List<Transform> highscoreEntryTransformList;
    private bool run = false;


    private void Start()
    {
        StartRanking();

    }

    public void FuncaoRank(string ss)
    {
        eCont = transform.Find("HSEnt");
        eTempla = eCont.Find("HStemplate");

        eTempla.gameObject.SetActive(false);

        string[] dados = ss.Split(';') ;

        Debug.Log(ss);

        highscoreEntryList = new List<HighscoreEntry>()
        {
            

        };

        string[] dados2;

        for (int index = 1; index < dados.Length; index++)
        {
            try
            {
                dados2 = dados[index].Split('.');
                highscoreEntryList.Add(new HighscoreEntry { name = dados2[0], score = float.Parse(dados2[1]) });
            }
            catch 
            {
                Debug.Log(dados[index]);
                
            }
        }



        for(int i=0; i < highscoreEntryList.Count; i++)
        {
            for(int j = i+1; j< highscoreEntryList.Count; j++)
            {
                if(highscoreEntryList[j].score > highscoreEntryList[i].score)
                {
                    HighscoreEntry tmp = highscoreEntryList[i];
                    highscoreEntryList[i] = highscoreEntryList[j];
                    highscoreEntryList[j] = tmp;
                }
            }
        }

        highscoreEntryTransformList = new List<Transform>();
        foreach (HighscoreEntry highscoreEntry in highscoreEntryList)
        {
            CreateHighscoreEntryTransform(highscoreEntry, eCont, highscoreEntryTransformList);
        }
    }

    private void CreateHighscoreEntryTransform(HighscoreEntry highscoreEntry, Transform container, List<Transform> transformList)
    {
        float tamanhoLinha = 20f; // colocar tamanho das linhas sql

        Transform trans = Instantiate(eTempla, container);
        RectTransform rectTransf = trans.GetComponent<RectTransform>();
        rectTransf.anchoredPosition = new Vector2(0, -tamanhoLinha * transformList.Count);
        trans.gameObject.SetActive(true);

        int rank = transformList.Count + 1;
        string rankString = rank + "s";

        string name = highscoreEntry.name;

        trans.Find("Nametext").GetComponent<Text>().text = name;

        float score = highscoreEntry.score;

        trans.Find("Ponttext").GetComponent<Text>().text = score.ToString();

        trans.Find("Postext").GetComponent<Text>().text = rankString;

        transformList.Add(trans);

    }

    IEnumerator GetRanking()
    {
        Network.instance.SendData("ranking");
        while (string.IsNullOrEmpty(Network.instance.rank))
        {
            Debug.Log("wait");
            yield return new WaitForFixedUpdate();
        }
        FuncaoRank(Network.instance.rank);
        run = false;
    }



    public void StartRanking()
    {
        if (!run)
        {
            run = true;
            StartCoroutine(GetRanking());
        }
        

    }

    private class HighscoreEntry
    {
        public float score;
        public string name;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using TMPro;
using UnityEngine.UI;


public class SkinManager1 : MonoBehaviourPun
{
    public static SkinManager1 instance;
    private void Awake()
    {
        instance = this;
    }

    public bool isEarth;
    public bool isSat;
    public bool isMoon;
    public bool isMars;

    public int spwanlimit = 1;

    public Transform[] SpawnPosiotion;
    Vector3 PlayerPosiotion;

    public enum State
    { 
        A,
        B,
        C,
        D,  
    }
  
    State state;
    //Çý¸° ¼öÁ¤
    public GameObject[] characterPrefab;
    GameObject[] clone;
    //int selectPlayerCount = 0;
    int playerCounts;
    void Start()
    {
        //int selectPlayerCount = characterPrefab.GetValue("selectPlayerCount");
        //PlayerController.instance.SelectCharacter();
        playerCounts = PlayerController.instance.selectPlayerCount;
        //GameObject clone = Instantiate(prefab, SpawnPosiotion[0].position, Quaternion.identity);
        GameObject prefab = characterPrefab[playerCounts];

        for (int i = 0; i < PhotonNetwork.CurrentRoom.PlayerCount; i++)
        {
            clone[i] = Instantiate(prefab, SpawnPosiotion[i].position, Quaternion.identity);
        
        }

        Vector2 initPos = Random.insideUnitCircle * 1.5f;
        //Vector3(-6.28499985, 2.3900001, -2.61999989)
        if (spwanlimit < 5)
        {
            PhotonNetwork.Instantiate("Player", new Vector3(-6.28f, 2.39f, -2.61f), Quaternion.identity);
            spwanlimit++;
        }
    
    }

   
    // Update is called once per frame
    void Update()
    {
        //int a = Random.Range(0, 4);
        //print(a);

        //switch(state)
        //{
        //   case State.A:
        //        PlayerPosiotion = SpawnPosiotion[0];
        //        break;

        //    case State.B:
        //        PlayerPosiotion = SpawnPosiotion[1];

        //        break;

        //    case State.C:
        //        PlayerPosiotion = SpawnPosiotion[2];

        //        break;

        //    case State.D:
        //        PlayerPosiotion = SpawnPosiotion[3];
        //        break;
       
        //}

        if (isEarth == true)
        {
            isSat = false;
            isMoon = false;
            isMars = false;
        }
        else if (isSat == true)
        {
            isEarth = false;
            isMoon = false;
            isMars = false;
        }
        else if (isMoon == true)
        {
            isEarth = false;
            isSat = false;
            isMars = false;
        }
        else if (isMars == true)
        {
            isEarth = false;
            isSat = false;
            isMoon = false;
        }
    }
}

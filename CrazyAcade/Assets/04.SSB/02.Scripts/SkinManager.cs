using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;


public class SkinManager : MonoBehaviourPun
{
    public static SkinManager instance;
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

    public GameObject[] PlayerSkin;

    public enum State
    { 
        A,
        B,
        C,
        D,  
    }
  
    State state;

    // Start is called before the first frame update
    void Start()
    {
        Vector2 initPos = Random.insideUnitCircle * 1.5f;
        //Vector3(-6.28499985, 2.3900001, -2.61999989)
        if (spwanlimit < 5)
        {

            for (int i = 0; i < PhotonNetwork.PlayerList.Length; i++)//PhotonNetwork.CurrentRoom.PlayerCount; i++)
            {
                PhotonNetwork.Instantiate("Player"+i, SpawnPosiotion[i].transform.position, Quaternion.identity);
            }
            //PhotonNetwork.Instantiate("Player", new Vector3(-6.28f, 2.39f, -2.61f), Quaternion.identity);
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

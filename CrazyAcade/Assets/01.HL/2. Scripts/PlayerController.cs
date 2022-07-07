using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;

using UnityEngine.SceneManagement;


public class PlayerController : MonoBehaviourPunCallbacks
{

    ExitGames.Client.Photon.Hashtable playerProperties = new ExitGames.Client.Photon.Hashtable();
    //public GameObject[] playerListUI; // UI상 플레이어 대기칸 playerList
    public GameObject[] characterPrefab; // 캐릭터 프리펩 인덱스
    public int selectPlayerCount = 0;
    public GameObject[] customPrefab= new GameObject[4];
    Player player;
    public GameObject[] slotPlayer;
    public GameObject spwanPose;
    public bool setActive;

    public static PlayerController instance;
    private void Awake()
    {
        DontDestroyOnLoad(this);
        //characterPrefab = new GameObject[0];
        // characterPrefab = GetComponent<GameObject[]>();
        instance = this;
    }
    void Start()
    {
        print("aaaa");
        //print(selectPlayerCount);
        //print(characterPrefab[0]);
        for (int i = 0; i < 4; i++)
        {
            print("bbbb");
            customPrefab[i] = Instantiate(characterPrefab[i], spwanPose.transform);
            customPrefab[i].SetActive(false);
        }
        customPrefab[selectPlayerCount].SetActive(true);
    }

    public void SetPlayerInfo(Player players)
    {
        player = players;
        UpdatePlayerInfo(player);

    }

    // 오른쪽 버튼을 누르면 플레이어 인덱스가 1 증가
    // 인덱스 3이 오른쪽 버튼을 만나면 실행 안됨
    public void ButtonRightDown()
    {
        selectPlayerCount++;
        selectPlayerCount %= 4;
        for (int i = 0; i < 4; i++)
        {
            customPrefab[i].SetActive(false);
        }
        customPrefab[selectPlayerCount].SetActive(true);
        // customPrefab = Instantiate(characterPrefab[selectPlayerCount], spwanPose.transform);
        //끊임없이 생성되고 있음
        // 전체를(프레펩 배열 형식으로) 불러오고 하나를 선택했을때
        // 해당 배열만 셋액티브, 나머지는 펄스
        //---------------------------------------

        PhotonNetwork.SetPlayerCustomProperties(playerProperties);
    }
    private void Update()
    {
       print("선택"+ selectPlayerCount);
    }
    // 왼쪽 버튼을 누르면 플레이어 인덱스가 1 감소
    // 인덱스 0이 왼쪽 버튼을 만나면 실행 안됨
    public void ButtonLeftDown()
    {
        selectPlayerCount--;
        if(selectPlayerCount <0)
        {
            selectPlayerCount += 4;
        }

        for (int i = 0; i < 4; i++)
        {
            customPrefab[i].SetActive(false);
        }
        customPrefab[selectPlayerCount].SetActive(true);
        PhotonNetwork.SetPlayerCustomProperties(playerProperties);

    }

    public override void OnPlayerPropertiesUpdate(Player targetPlayer, ExitGames.Client.Photon.Hashtable changedProps)
    {
        if (player == targetPlayer)
        {
            UpdatePlayerInfo(targetPlayer);
        }
    }

    //이 버튼이 활성화 되었다면
    //해당 인덱스 오브젝트는 비활성화가 된다
    //플레이어(닉네임은)는 해당 인덱스 오브젝트로 변하고
    //캐릭터 스킨변경 확정버튼
    public void SelectCharacter()
    {
        setActive = true;
        print("선택했다");
        //만약 버튼 다시누른다면
        //

        //만약 선택했다면 slotPlayer가 해당 selectPlayerCount로 변한다
        //slotPlayer[] = characterPrefab.SetValue("selectPlayerCount", selectPlayerCount);

    }
    void UpdatePlayerInfo(Player player)
    {
        if (player.CustomProperties.ContainsKey("customPrefab"))
        {
          //  customPrefab = characterPrefab[(int)player.CustomProperties["customPrefab"]];
          //  playerProperties["customPrefab"] = (int)player.CustomProperties["customPrefab"];
        }
        else
        {
           // playerProperties["customPrefab"] = 0;
        }
    }
    
    //플레이어 닉네임을 오브젝트로 받고 싶다
    //플레이어(닉네임은=PhotonNetwork.NickName)==> 오브젝트를 가져야한다/
    //는 해당 인덱스 오브젝트로 변하고
    //해당 인덱스 오브젝트는 비활성화가 된다

    public void OnClickGameStart()
    {
        //게임씬으로 이동
        //선택한 캐릭터로
        print("OnClickGameStart");
        PhotonNetwork.LoadLevel("Game");

    }


}

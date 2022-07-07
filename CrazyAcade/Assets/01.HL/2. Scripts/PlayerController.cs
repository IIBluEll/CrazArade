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
    //public GameObject[] playerListUI; // UI�� �÷��̾� ���ĭ playerList
    public GameObject[] characterPrefab; // ĳ���� ������ �ε���
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

    // ������ ��ư�� ������ �÷��̾� �ε����� 1 ����
    // �ε��� 3�� ������ ��ư�� ������ ���� �ȵ�
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
        //���Ӿ��� �����ǰ� ����
        // ��ü��(������ �迭 ��������) �ҷ����� �ϳ��� ����������
        // �ش� �迭�� �¾�Ƽ��, �������� �޽�
        //---------------------------------------

        PhotonNetwork.SetPlayerCustomProperties(playerProperties);
    }
    private void Update()
    {
       print("����"+ selectPlayerCount);
    }
    // ���� ��ư�� ������ �÷��̾� �ε����� 1 ����
    // �ε��� 0�� ���� ��ư�� ������ ���� �ȵ�
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

    //�� ��ư�� Ȱ��ȭ �Ǿ��ٸ�
    //�ش� �ε��� ������Ʈ�� ��Ȱ��ȭ�� �ȴ�
    //�÷��̾�(�г�����)�� �ش� �ε��� ������Ʈ�� ���ϰ�
    //ĳ���� ��Ų���� Ȯ����ư
    public void SelectCharacter()
    {
        setActive = true;
        print("�����ߴ�");
        //���� ��ư �ٽô����ٸ�
        //

        //���� �����ߴٸ� slotPlayer�� �ش� selectPlayerCount�� ���Ѵ�
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
    
    //�÷��̾� �г����� ������Ʈ�� �ް� �ʹ�
    //�÷��̾�(�г�����=PhotonNetwork.NickName)==> ������Ʈ�� �������Ѵ�/
    //�� �ش� �ε��� ������Ʈ�� ���ϰ�
    //�ش� �ε��� ������Ʈ�� ��Ȱ��ȭ�� �ȴ�

    public void OnClickGameStart()
    {
        //���Ӿ����� �̵�
        //������ ĳ���ͷ�
        print("OnClickGameStart");
        PhotonNetwork.LoadLevel("Game");

    }


}

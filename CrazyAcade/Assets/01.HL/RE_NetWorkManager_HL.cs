using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;
using System;
using UnityEngine.SceneManagement;

/* For 혜린 
 *  PhotonNetwork.CurrentRoom.PlayerCount       => 현재 접속하고 있는 방의 플레이어 인원 수 
 *  PhotonNetwork.CurrentRoom.Name              => 현재 접속하고 있는 방의 이름
 *  PhotonNetwork.LocalPlayer.NickName          => 서버에 저장된 '나'의 닉네임 
 *  PhotonNetwork.NickName                      => 위와 동일
 *  string player_NickName;                     => 로그인 씬에서 저장한 나의 닉네임을 가져와서 변수에 저장하기 위함
 *  string room_Name;                           => 방만들기 씬에서 저장한 방 이름을 가져와서 변수에 저장하기 위함 
 *                                              => 즉 "player_NickName"과 "room_Name" 변수를 활용해서 너가 만들고 싶은거 만들수 있음!
*/

public class RE_NetWorkManager_HL : MonoBehaviourPunCallbacks
{
    //저장된 플레이어 닉네임, 방 이름 정보를 가진 오브젝트
    GameObject temp_Info;
    public PhotonView pv;
    string player_NickName;             // 플레이어 닉네임 변수
    string room_Name;                   // 방이름 변수
    //로비
    public Text Login_Text;             // 로그인할때 나오는 텍스트

    //룸 // 채팅에 대한 정보가 필요

    public GameObject RoomPannel;       // 방 UI 오브젝트
    public TextMeshProUGUI[] chatText;  // 채팅 텍스트 
    public TMP_InputField chatInput;    // 채팅 인풋필드
    public TextMeshProUGUI RoomInfoText;           // 방정보 텍스트
    public Text PlayerInfoText;         // 플레이어 정보 텍스트
    public Text PlayerCountText;         // 현재 명수 정보 텍스트
    public TextMeshProUGUI[] PlayerNickname;           // 방정보 텍스트
    public GameObject[] slotUI;

    int slotCount = 0;
    public int selslot = 0;
    // Start is called before the first frame update

    

    private void Awake()
    {

        //player_NickName =// pv.IsMine ? PhotonNetwork.NickName : pv.Owner.NickName;

    }
    void Start()
    {
        //pv.GetComponent<PhotonView>();
        temp_Info = GameObject.Find("Temp_Info"); // 정보를 가진 오브젝트 찾기
        player_NickName = temp_Info.GetComponent<Re_TempInfo>().user_ID;    // 닉네임 불러와서 저장
        room_Name = temp_Info.GetComponent<Re_TempInfo>().roomName; // 방이름 불러와서 저장


        Login_Text.text = PhotonNetwork.LocalPlayer.NickName + "님이 입장하셨습니다"; // "[닉네임]님 환영합니다" 

        for (int i = 0; i < chatText.Length; i++) // 텍스트들을 아무 글자 없게 초기화 해줌
        {
            chatText[i].text = "";
        }
        chatInput.text = "";

        RoomInfo();




    }

    public override void OnJoinedRoom() // 그냥 콜백만 하기 위해서 씀
    {

    }
    public override void OnPlayerEnteredRoom(Player newPlayer)  // 그냥 콜백만 하기 위해서 씀
    {

    }

    private void RoomInfo() // 특별한 기능은 없고 그저 텍스트를 띄워주는 일을 함
    {
        PlayerCountText.text = "현재" + PhotonNetwork.CurrentRoom.PlayerCount + "명 / 최대" + PhotonNetwork.CurrentRoom.MaxPlayers + "명";
        PlayerInfoText.text = PhotonNetwork.LocalPlayer.NickName + "플레이어가 입장하였습니다";
    }

    public void LeaveRoom() // 방 떠나기 
    {
        PhotonNetwork.LeaveRoom();
        PhotonNetwork.LoadLevel("Re_Lobby"); // Re_Lobby 씬을 호출
    }

    public void SendMessage()
    {
        //내가 입력한 글자를 보낸다 -> 방장이 처리할 것
        photonView.RPC("ChatRPC", RpcTarget.All, PhotonNetwork.NickName + " : " + chatInput.text);
        chatInput.text = "";
    }

    [PunRPC]
    void ChatRPC(string msg)
    {
        bool isInput = false; // 매번 입력 여부확인 위한 변수
        for (int i = 0; i < chatText.Length; i++)
        {
            if (chatText[i].text == "") //화면에 보이는 영역이 비었을 때
            {
                isInput = true; // 입력이 되었다
                chatText[i].text = msg;
                break; // 입력을 마치면 반복문 끝
            }
        }
        if (isInput == false) // 꽉차서 더이상 입력값을 넣지 못헀을 때 -> 1칸 밀려 쓰기, 아래 공간에 챗 입력
        {
            for (int i = 1; i < chatText.Length; i++) // 0,1,2,3 => 1,2,3,3 
            {
                chatText[i - 1].text = chatText[i].text;
            }
            chatText[chatText.Length - 1].text = msg;
        }
    }


    //public GameObject[] playerListUI; // UI상 플레이어 대기칸 playerList
    int slot = 0;
    int slotPlayer;
    public GameObject[] dummyCharacter;
    [PunRPC]
    public void SlotActive()
    {
        print("플레이어가 들어왔어요!");
        for (int i = 0; i < PhotonNetwork.PlayerList.Length; i++)//PhotonNetwork.CurrentRoom.PlayerCount; i++)
        {
            print("인원" + PhotonNetwork.PlayerList.Length);
            selslot = PhotonNetwork.PlayerList.Length;
            // PlayerNickname[slot].text = player_NickName;
            PlayerNickname[i].text = PhotonNetwork.PlayerList[i].NickName;
            slotUI[i].SetActive(false);

            SelectDummy();
        }

    }

    public void SelectDummy()
    {
        //slotPlayer, dummyCharacter
        //만약 캐릭터 선택을 했다면 해당 오브젝트로 바꿔줌
        if (PlayerController.instance.setActive == true)
        {
            slotPlayer = PlayerController.instance.selectPlayerCount;
            print(slotPlayer + "-----여기다!-----");
            GameObject dummySlot = dummyCharacter[slotPlayer];
            Instantiate(dummySlot, PlayerController.instance.slotPlayer[slotCount].transform);
            PlayerController.instance.setActive = false;
            slotCount++;
        }
        //선택을 하지 않았다면 오브젝트로 변하지 않음, 언제까지? 선택할 때까지
        //이 과정은 다음 플레이어 서버에 관여하지 않음
        else
        {
            //break;
        }
    }

    public void OnClickGameStart()
    {
        print("OnClickGameStart");
        //게임씬으로 이동
        SceneManager.LoadScene("GameMapScene"); // 수정해야함
    }
    //*  PhotonNetwork.LocalPlayer.NickName          => 서버에 저장된 '나'의 닉네임
    //slot 채우기
    //첫번째 들어오는 player_NickName.text에 첫번째 인덱스 오브젝트(enterPlayer)를 배치



    void Update()
    {
        PlayerCountText.text = "현재" + PhotonNetwork.CurrentRoom.PlayerCount + "명 / 최대" + PhotonNetwork.CurrentRoom.MaxPlayers + "명";
        pv.RPC("SlotActive", RpcTarget.All);


    }


}

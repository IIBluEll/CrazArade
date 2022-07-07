using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;
using System;

/* For 혜린 
 *  PhotonNetwork.CurrentRoom.PlayerCount       => 현재 접속하고 있는 방의 플레이어 인원 수 
 *  PhotonNetwork.CurrentRoom.Name              => 현재 접속하고 있는 방의 이름
 *  PhotonNetwork.LocalPlayer.NickName          => 서버에 저장된 '나'의 닉네임 
 *  PhotonNetwork.NickName                      => 위와 동일
 *  string player_NickName;                     => 로그인 씬에서 저장한 나의 닉네임을 가져와서 변수에 저장하기 위함
 *  string room_Name;                           => 방만들기 씬에서 저장한 방 이름을 가져와서 변수에 저장하기 위함 
 *                                              => 즉 "player_NickName"과 "room_Name" 변수를 활용해서 너가 만들고 싶은거 만들수 있음!
*/

public class RE_NetWorkManager : MonoBehaviourPunCallbacks
{
    //저장된 플레이어 닉네임, 방 이름 정보를 가진 오브젝트
    GameObject temp_Info;

    string player_NickName;             // 플레이어 닉네임 변수
    string room_Name;                   // 방이름 변수
    //로비
    public Text Login_Text;             // 로그인할때 나오는 텍스트

    //룸 // 채팅에 대한 정보가 필요

    public GameObject RoomPannel;       // 방 UI 오브젝트
    public TextMeshProUGUI[] chatText;  // 채팅 텍스트 
    public TMP_InputField chatInput;    // 채팅 인풋필드
    public Text RoomInfoText;           // 방정보 텍스트
    public Text PlayerInfoText;         // 플레이어 정보 텍스트

    // Start is called before the first frame update
    void Start()
    {
        temp_Info = GameObject.Find("Temp_Info"); // 정보를 가진 오브젝트 찾기
        player_NickName = temp_Info.GetComponent<Re_TempInfo>().user_ID;    // 닉네임 불러와서 저장
        room_Name = temp_Info.GetComponent<Re_TempInfo>().roomName; // 방이름 불러와서 저장


        Login_Text.text = PhotonNetwork.LocalPlayer.NickName + "님 환영합니다"; // "[닉네임]님 환영합니다" 

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
        RoomInfoText.text = "현재" + PhotonNetwork.CurrentRoom.PlayerCount + "명 / " + "최대" + PhotonNetwork.CurrentRoom.MaxPlayers;
        PlayerInfoText.text = PhotonNetwork.CurrentRoom.Name + "에 접속하셨습니다";
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

    // Update is called once per frame
    void Update()
    {

    }


}

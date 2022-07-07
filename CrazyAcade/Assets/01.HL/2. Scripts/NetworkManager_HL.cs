using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;

public class NetworkManager : MonoBehaviourPunCallbacks //MonoBehaviour
{
    //로그인
    public InputField NicknameInput;

    // 로비
    public GameObject LobbyPannel;
    public InputField RoomInput;
    public Text Login_Text;
    List<RoomInfo> roominfolist = new List<RoomInfo>();

    //룸 // 채팅에 대한 정보가 필요
    public GameObject RoomPannel;
    public Text[] chatText;
    public InputField chatInput;
    public Text RoomInfoText;
    public Text PlayerInfoText;

    public void Connect_Work() // 버튼과 연결
    {
        if (NicknameInput.text != null) // 입력된 닉네임을 넣는다
        {
            PhotonNetwork.LocalPlayer.NickName = NicknameInput.text;
        }
        else
        {
            PhotonNetwork.LocalPlayer.NickName = "Player" + Random.Range(0, 100);
        }
        PhotonNetwork.ConnectUsingSettings();
    }
    public override void OnConnectedToMaster()
    {
        PhotonNetwork.JoinLobby();
    }
    public override void OnJoinedLobby()
    {
        LobbyPannel.SetActive(true); // 로그인 화면 위에 로비화면을 덧입힌다
        RoomPannel.SetActive(false);
        Login_Text.text = PhotonNetwork.LocalPlayer.NickName + "님 환영합니다";
        roominfolist.Clear();//초기화 -> 게임에서 룸으로 들어가거나 룸에서 나왔을 때, 내가 가진 룸 정보를 초기화
    }
    public void CreateRoom()
    {
        if (RoomInput.text == "")
        {
            RoomInput.text = "Room" + Random.Range(0, 100);
        }
        else
        {
            RoomInput.text = RoomInput.text;
            //방을 만든다
            PhotonNetwork.CreateRoom(RoomInput.text, new RoomOptions { MaxPlayers = 4 }); // 최대 플레이어 4명
        }

    }

    public override void OnPlayerEnteredRoom(Player newPlayer) // 방 참여할 때 호출되는 매서드
    {
        RoomInfo();
    }

    //빠른 실행하기 방
    public void JoinRandomRoom() // 방이 있을 수도 없을 수도 있음
    {
        PhotonNetwork.JoinRandomRoom();
    }

    //들어갔을 때
    public override void OnJoinedRoom()
    {
        RoomPannel.SetActive(true);
        //채팅창 초기화
        for (int i = 0; i < chatText.Length; i++)
        {
            chatText[i].text = "";
        }
        chatInput.text = "";
        RoomInfo();
    }

    void RoomInfo()
    {

        RoomInfoText.text =  PhotonNetwork.CurrentRoom.PlayerCount + " 명 /" + PhotonNetwork.CurrentRoom.MaxPlayers + "최대";
        PlayerInfoText.text = PhotonNetwork.CurrentRoom.Name + " 님이 입장하셨습니다. ";
    }
    public override void OnJoinRandomFailed(short returnCode, string message) // 랜덤으로 들어갈 방이 없을때
    {
        RoomInput.text = "방 참여 실패 방 생성 중";
        CreateRoom();
    }
    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        RoomInput.text = "방생성 실패 재 생성 중";
        CreateRoom();
    }

    public void LeaveRoom()
    {
        PhotonNetwork.LeaveRoom();
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

}

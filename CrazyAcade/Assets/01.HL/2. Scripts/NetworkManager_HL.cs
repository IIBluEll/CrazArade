using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;

public class NetworkManager : MonoBehaviourPunCallbacks //MonoBehaviour
{
    //�α���
    public InputField NicknameInput;

    // �κ�
    public GameObject LobbyPannel;
    public InputField RoomInput;
    public Text Login_Text;
    List<RoomInfo> roominfolist = new List<RoomInfo>();

    //�� // ä�ÿ� ���� ������ �ʿ�
    public GameObject RoomPannel;
    public Text[] chatText;
    public InputField chatInput;
    public Text RoomInfoText;
    public Text PlayerInfoText;

    public void Connect_Work() // ��ư�� ����
    {
        if (NicknameInput.text != null) // �Էµ� �г����� �ִ´�
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
        LobbyPannel.SetActive(true); // �α��� ȭ�� ���� �κ�ȭ���� ��������
        RoomPannel.SetActive(false);
        Login_Text.text = PhotonNetwork.LocalPlayer.NickName + "�� ȯ���մϴ�";
        roominfolist.Clear();//�ʱ�ȭ -> ���ӿ��� ������ ���ų� �뿡�� ������ ��, ���� ���� �� ������ �ʱ�ȭ
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
            //���� �����
            PhotonNetwork.CreateRoom(RoomInput.text, new RoomOptions { MaxPlayers = 4 }); // �ִ� �÷��̾� 4��
        }

    }

    public override void OnPlayerEnteredRoom(Player newPlayer) // �� ������ �� ȣ��Ǵ� �ż���
    {
        RoomInfo();
    }

    //���� �����ϱ� ��
    public void JoinRandomRoom() // ���� ���� ���� ���� ���� ����
    {
        PhotonNetwork.JoinRandomRoom();
    }

    //���� ��
    public override void OnJoinedRoom()
    {
        RoomPannel.SetActive(true);
        //ä��â �ʱ�ȭ
        for (int i = 0; i < chatText.Length; i++)
        {
            chatText[i].text = "";
        }
        chatInput.text = "";
        RoomInfo();
    }

    void RoomInfo()
    {

        RoomInfoText.text =  PhotonNetwork.CurrentRoom.PlayerCount + " �� /" + PhotonNetwork.CurrentRoom.MaxPlayers + "�ִ�";
        PlayerInfoText.text = PhotonNetwork.CurrentRoom.Name + " ���� �����ϼ̽��ϴ�. ";
    }
    public override void OnJoinRandomFailed(short returnCode, string message) // �������� �� ���� ������
    {
        RoomInput.text = "�� ���� ���� �� ���� ��";
        CreateRoom();
    }
    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        RoomInput.text = "����� ���� �� ���� ��";
        CreateRoom();
    }

    public void LeaveRoom()
    {
        PhotonNetwork.LeaveRoom();
    }

    public void SendMessage()
    {
        //���� �Է��� ���ڸ� ������ -> ������ ó���� ��
        photonView.RPC("ChatRPC", RpcTarget.All, PhotonNetwork.NickName + " : " + chatInput.text);
        chatInput.text = "";
    }

    [PunRPC]
    void ChatRPC(string msg)
    {
        bool isInput = false; // �Ź� �Է� ����Ȯ�� ���� ����
        for (int i = 0; i < chatText.Length; i++)
        {
            if (chatText[i].text == "") //ȭ�鿡 ���̴� ������ ����� ��
            {
                isInput = true; // �Է��� �Ǿ���
                chatText[i].text = msg;
                break; // �Է��� ��ġ�� �ݺ��� ��
            }
        }
        if (isInput == false) // ������ ���̻� �Է°��� ���� ������ �� -> 1ĭ �з� ����, �Ʒ� ������ ê �Է�
        {
            for (int i = 1; i < chatText.Length; i++) // 0,1,2,3 => 1,2,3,3 
            {
                chatText[i - 1].text = chatText[i].text;
            }
            chatText[chatText.Length - 1].text = msg;
        }
    }

}

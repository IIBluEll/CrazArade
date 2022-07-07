using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;
using System;

/* For ���� 
 *  PhotonNetwork.CurrentRoom.PlayerCount       => ���� �����ϰ� �ִ� ���� �÷��̾� �ο� �� 
 *  PhotonNetwork.CurrentRoom.Name              => ���� �����ϰ� �ִ� ���� �̸�
 *  PhotonNetwork.LocalPlayer.NickName          => ������ ����� '��'�� �г��� 
 *  PhotonNetwork.NickName                      => ���� ����
 *  string player_NickName;                     => �α��� ������ ������ ���� �г����� �����ͼ� ������ �����ϱ� ����
 *  string room_Name;                           => �游��� ������ ������ �� �̸��� �����ͼ� ������ �����ϱ� ���� 
 *                                              => �� "player_NickName"�� "room_Name" ������ Ȱ���ؼ� �ʰ� ����� ������ ����� ����!
*/

public class RE_NetWorkManager : MonoBehaviourPunCallbacks
{
    //����� �÷��̾� �г���, �� �̸� ������ ���� ������Ʈ
    GameObject temp_Info;

    string player_NickName;             // �÷��̾� �г��� ����
    string room_Name;                   // ���̸� ����
    //�κ�
    public Text Login_Text;             // �α����Ҷ� ������ �ؽ�Ʈ

    //�� // ä�ÿ� ���� ������ �ʿ�

    public GameObject RoomPannel;       // �� UI ������Ʈ
    public TextMeshProUGUI[] chatText;  // ä�� �ؽ�Ʈ 
    public TMP_InputField chatInput;    // ä�� ��ǲ�ʵ�
    public Text RoomInfoText;           // ������ �ؽ�Ʈ
    public Text PlayerInfoText;         // �÷��̾� ���� �ؽ�Ʈ

    // Start is called before the first frame update
    void Start()
    {
        temp_Info = GameObject.Find("Temp_Info"); // ������ ���� ������Ʈ ã��
        player_NickName = temp_Info.GetComponent<Re_TempInfo>().user_ID;    // �г��� �ҷ��ͼ� ����
        room_Name = temp_Info.GetComponent<Re_TempInfo>().roomName; // ���̸� �ҷ��ͼ� ����


        Login_Text.text = PhotonNetwork.LocalPlayer.NickName + "�� ȯ���մϴ�"; // "[�г���]�� ȯ���մϴ�" 

        for (int i = 0; i < chatText.Length; i++) // �ؽ�Ʈ���� �ƹ� ���� ���� �ʱ�ȭ ����
        {
            chatText[i].text = "";
        }
        chatInput.text = "";

        RoomInfo();
    }

    public override void OnJoinedRoom() // �׳� �ݹ鸸 �ϱ� ���ؼ� ��
    {
    }
    public override void OnPlayerEnteredRoom(Player newPlayer)  // �׳� �ݹ鸸 �ϱ� ���ؼ� ��
    {
    }

    private void RoomInfo() // Ư���� ����� ���� ���� �ؽ�Ʈ�� ����ִ� ���� ��
    {
        RoomInfoText.text = "����" + PhotonNetwork.CurrentRoom.PlayerCount + "�� / " + "�ִ�" + PhotonNetwork.CurrentRoom.MaxPlayers;
        PlayerInfoText.text = PhotonNetwork.CurrentRoom.Name + "�� �����ϼ̽��ϴ�";
    }

    public void LeaveRoom() // �� ������ 
    {
        PhotonNetwork.LeaveRoom();
        PhotonNetwork.LoadLevel("Re_Lobby"); // Re_Lobby ���� ȣ��
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

    // Update is called once per frame
    void Update()
    {

    }


}

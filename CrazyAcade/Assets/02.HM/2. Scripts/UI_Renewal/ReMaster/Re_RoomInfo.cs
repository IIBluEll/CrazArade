using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

using Photon.Pun;
using Photon.Realtime;

public class Re_RoomInfo : MonoBehaviour
{
    GameObject temp_Info;

    //public TMP_InputField inputRoomName;
    public TextMeshProUGUI roomNameText;
    public TextMeshProUGUI playerCount;

    RoomInfo roomInfo;

    public RoomInfo RoomInfo
    {
        get
        {
            return roomInfo;
        }
        set
        {
            roomInfo = value;

            playerCount.text = $"{roomInfo.PlayerCount}/{roomInfo.MaxPlayers}";

            roomNameText.text = $"{roomInfo.Name}";

            GetComponent<UnityEngine.UI.Button>().onClick.AddListener(() => OnEnterRoom(roomInfo.Name));
        }
    }

    private void Awake()
    {
        temp_Info = GameObject.Find("Temp_Info");
        roomNameText.text = temp_Info.GetComponent<Re_TempInfo>().roomName;
    }

    void OnEnterRoom(string roomName)
    {
        RoomOptions ro = new RoomOptions();
        ro.IsOpen = true;
        ro.IsVisible = true;
        ro.MaxPlayers = 4;

        PhotonNetwork.NickName = temp_Info.GetComponent<Re_TempInfo>().user_ID;
        PhotonNetwork.JoinOrCreateRoom(roomName, ro, TypedLobby.Default);
    }

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

using Photon.Pun;
using Photon.Realtime;

public class Re_RoomBtn_Info : MonoBehaviour
{
    public Text roomInfoText;
    RoomInfo roomInfo;

    public InputField userIDText;
    public RoomInfo RoomInfo
    {
        get
        {
            return roomInfo;
        }
        set
        {
            roomInfo = value;
            roomInfoText.text = $"{roomInfo.PlayerCount}/{roomInfo.MaxPlayers}";

            GetComponent<UnityEngine.UI.Button>().onClick.AddListener(() => OnEnterRoom(roomInfo.Name));
        }
    }

    private void Awake()
    {
        roomInfoText = GetComponentInChildren<Text>();
        userIDText = GameObject.Find("UserID").GetComponent<InputField>();

    }

    void OnEnterRoom(string roomName)
    {
        RoomOptions ro = new RoomOptions();
        ro.IsOpen = true;
        ro.IsVisible = true;
        ro.MaxPlayers = 4;

        PhotonNetwork.NickName = userIDText.text;
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

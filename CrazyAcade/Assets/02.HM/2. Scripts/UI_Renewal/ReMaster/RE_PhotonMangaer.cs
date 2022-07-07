using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

using Photon.Pun;
using Photon.Realtime;
public class RE_PhotonMangaer : MonoBehaviourPunCallbacks
{
    string gameVersion = "v1.0";
    string userID = "";

    Dictionary<string, GameObject> roomdic = new Dictionary<string, GameObject>();

    public GameObject roomPrefeb;
    public GameObject lobby_UI;
    public GameObject waitRoom_UI;

    public TMP_InputField roomNameText;
    

    GameObject tempInfo;

    private void Awake()
    {
        PhotonNetwork.AutomaticallySyncScene = true;
        PhotonNetwork.GameVersion = gameVersion;
        PhotonNetwork.ConnectUsingSettings();

        tempInfo = GameObject.Find("Temp_Info");
       // roomNameText = GameObject.Find("roomNameText").GetComponent<TMP_InputField>();
    }

    // Start is called before the first frame update
    void Start()
    {
        userID = tempInfo.GetComponent<Re_TempInfo>().user_ID;
        PhotonNetwork.NickName = userID;
    }

    public override void OnConnectedToMaster()
    {
        print("1. 포톤 서버에 접속");

        PhotonNetwork.JoinLobby();
    }

    public override void OnJoinedLobby()
    {
        print("2. 로비에 접속");
    }

    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        print("랜덤 룸 접속 실패");

        RoomOptions ro = new RoomOptions();
        ro.IsOpen = true;
        ro.IsVisible = true;
        ro.MaxPlayers = 4;

        roomNameText.text = $"Room_{Random.Range(1, 100):000}";

        PhotonNetwork.CreateRoom("room_1", ro);
    }

    public override void OnCreatedRoom()
    {
        print("3. 방생성 완료");
    }

    public override void OnJoinedRoom()
    {
        print("4. 방 입장 완료");
        if (PhotonNetwork.IsMasterClient)
        {
            PhotonNetwork.LoadLevel("UI_HL");

        }
    }

    

    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {
        GameObject tempRoom = null;
        foreach (var room in roomList)
        {
            if (room.RemovedFromList == true)
            {
                roomdic.TryGetValue(room.Name, out tempRoom);
                Destroy(tempRoom);
                roomdic.Remove(room.Name);
            }
            else
            {
                if (roomdic.ContainsKey(room.Name) == false)
                {

                    GameObject _room = Instantiate(roomPrefeb, Re_RoomPrefeb_Manager.instance.InstansiateRoom());
                    _room.GetComponent<Re_RoomInfo>().RoomInfo = room;
                    roomdic.Add(room.Name, _room);
                }
                else
                {
                    roomdic.TryGetValue(room.Name, out tempRoom);
                    tempRoom.GetComponent<Re_RoomInfo>().RoomInfo = room;

                }
            }
        }
    }

    public void OnRandomBtn()
    {
        
        PlayerPrefs.SetString("USER_ID", userID);
        PhotonNetwork.NickName = userID;
        PhotonNetwork.JoinRandomRoom();
    }

    public void OnMakeRoomClik()
    {
        RoomOptions ro = new RoomOptions();
        ro.IsOpen = true;
        ro.IsVisible = true;
        ro.MaxPlayers = 4;

        if (string.IsNullOrEmpty(roomNameText.text))
        {
            roomNameText.text = $"Room_{Random.Range(1, 100):000}";
        }
        PhotonNetwork.CreateRoom(roomNameText.text, ro);

        tempInfo.GetComponent<Re_TempInfo>().roomName = roomNameText.text;
    }

    public void QuickStart_Btn()
    {
        PhotonNetwork.JoinRandomRoom();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

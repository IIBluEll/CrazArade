using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using Photon.Pun;
using Photon.Realtime;

public class PhotonManager1 : MonoBehaviourPunCallbacks
{
    string gameVersion = "v1.0";
    string userId = "HML";

    public InputField userIdText;
    public InputField roomNameText;

    Dictionary<string, GameObject> roomdic = new Dictionary<string, GameObject>();

    public GameObject roomPrefab;
    public Transform scrollContent;

    private void Awake()
    {
        PhotonNetwork.AutomaticallySyncScene = true;
        PhotonNetwork.GameVersion = gameVersion;
        PhotonNetwork.ConnectUsingSettings();
    }

    // Start is called before the first frame update
    void Start()
    {
        userId = PlayerPrefs.GetString("User_ID", $"USER_{Random.Range(0, 100):00}");
        userIdText.text = userId;
        PhotonNetwork.NickName = userId;

        OnConnectedToMaster();
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
        if(PhotonNetwork.IsMasterClient)
        {
            PhotonNetwork.LoadLevel("WaitRoom");
        }
    }

    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {
        GameObject tempRoom = null;
        foreach(var room in roomList)
        {
            if(room.RemovedFromList == true)
            {
                roomdic.TryGetValue(room.Name, out tempRoom);
                Destroy(tempRoom);
                roomdic.Remove(room.Name);
            }
            else
            {
                if (roomdic.ContainsKey(room.Name) == false)
                {
                    GameObject _room = Instantiate(roomPrefab, scrollContent);
                    _room.GetComponent<Robby_Data>().RoomInfo = room;
                    roomdic.Add(room.Name, _room);
                }
                else
                {
                    roomdic.TryGetValue(room.Name, out tempRoom);
                    tempRoom.GetComponent<Robby_Data>().RoomInfo = room;
                     
                }
            }
        }
    }

    public void OnRandomBtn()
    {
        if(string.IsNullOrEmpty(userIdText.text))
        {
            userId = $"USER_{Random.Range(0, 100):00}";
            userIdText.text = userId;
        }
        PlayerPrefs.SetString("USER_ID", userIdText.text);
        PhotonNetwork.NickName = userIdText.text;
        PhotonNetwork.JoinRandomRoom();
    }

    public void OnMakeRoomClik()
    {
        RoomOptions ro = new RoomOptions();
        ro.IsOpen = true;
        ro.IsVisible = true;
        ro.MaxPlayers = 4;

        if(string.IsNullOrEmpty(roomNameText.text))
        {
            roomNameText.text = $"Room_{Random.Range(1, 100):000}";
        }
        PhotonNetwork.CreateRoom(roomNameText.text, ro);
    }



    // Update is called once per frame
    void Update()
    {
        DontDestroyOnLoad(this);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;
using TMPro;

using Photon.Pun;
using Photon.Realtime;

public class Re_TitleManager : MonoBehaviour
{
    private string user_ID = "";

    bool isInputAnyKey = false;

    public GameObject mainTitle_UI;
    public GameObject login_ID_UI;

    public TMP_InputField nickName;

    public GameObject temp_Info;
    

    // Start is called before the first frame update
    void Start()
    {
        user_ID = PlayerPrefs.GetString("User_ID", $"USER_{Random.Range(1, 100):00}");
        nickName.text = user_ID;

        PhotonNetwork.NickName = nickName.text;

        
    }

    public void JoinLobby()
    {
        if(string.IsNullOrEmpty(nickName.text))
        {
            user_ID = $"USER_{Random.Range(0, 100):00}";
            nickName.text = user_ID;
        }

        PlayerPrefs.SetString("USER_ID", nickName.text);
        PhotonNetwork.NickName = nickName.text;

        temp_Info.GetComponent<Re_TempInfo>().user_ID = nickName.text;

        PhotonNetwork.LoadLevel("Re_Lobby");
    }

    // Update is called once per frame
    void Update()
    {
        DontDestroyOnLoad(temp_Info);

        if ( Input.anyKeyDown && isInputAnyKey == false)
        {
            mainTitle_UI.SetActive(false);
            login_ID_UI.SetActive(true);
            isInputAnyKey = true;
        }    
    }
}

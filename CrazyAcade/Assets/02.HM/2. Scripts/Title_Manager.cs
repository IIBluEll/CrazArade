using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

using Photon.Pun;
using Photon.Realtime;

public class Title_Manager : MonoBehaviourPunCallbacks
{
    public GameObject mainTitle_UI;
    public GameObject joinSever_UI;

    public Button joinSever_Btn;
    public Button quitGame_Btn;

   
    public bool isInputAnyKey = false;
    // Start is called before the first frame update
    void Start()
    {
        joinSever_Btn.GetComponent<Button>();
        quitGame_Btn.GetComponent<Button>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.anyKeyDown && isInputAnyKey == false)
        {
            MainTitle_Set_DeActive();
            LoginUI_Active();
            isInputAnyKey = true;
        }
    }

    #region 메인타이틀 비활성화
    void MainTitle_Set_DeActive()
    {
        isInputAnyKey = true;
        mainTitle_UI.SetActive(false);
    }
    #endregion

    #region 로그인UI 활성화 
    void LoginUI_Active()
    {
        joinSever_UI.SetActive(true);
    }
    #endregion

    public void GotoRobby()
    {
        PhotonNetwork.LoadLevel("Lobby");
    }

    #region 게임종료하기
    public void QuitGame()
    {
        Application.Quit();
    }
    #endregion
}

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

    #region ����Ÿ��Ʋ ��Ȱ��ȭ
    void MainTitle_Set_DeActive()
    {
        isInputAnyKey = true;
        mainTitle_UI.SetActive(false);
    }
    #endregion

    #region �α���UI Ȱ��ȭ 
    void LoginUI_Active()
    {
        joinSever_UI.SetActive(true);
    }
    #endregion

    public void GotoRobby()
    {
        PhotonNetwork.LoadLevel("Lobby");
    }

    #region ���������ϱ�
    public void QuitGame()
    {
        Application.Quit();
    }
    #endregion
}

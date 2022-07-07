using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using Photon.Pun;
using Photon.Realtime;

[RequireComponent(typeof(PhotonView))]
public class Timer : MonoBehaviourPunCallbacks, IPunObservable
{

    System.DateTime startTime = System.DateTime.UtcNow;

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            stream.SendNext(startTime.Ticks);
        }
        else
        {
            startTime = new System.DateTime((long)stream.ReceiveNext());
        }
    }

    public void SetTimer()
    {

        timer = oneGameTime - ((System.DateTime.UtcNow.Ticks - startTime.Ticks) / 10000000);
        timer = Mathf.Clamp(0, timer, oneGameTime);
        timerUI.text = timer.ToString();
    }

     static readonly float oneGameTime = 20;
    float timer = oneGameTime;
    public Text timerUI;

    public GameObject drawUi;
    

    // Update is called once per frame
    void Update()
    {
        SetTimer();

        if(timer <= 0)
        {
            drawUi.SetActive(true);
            Time.timeScale = 0;
        }
    }


}
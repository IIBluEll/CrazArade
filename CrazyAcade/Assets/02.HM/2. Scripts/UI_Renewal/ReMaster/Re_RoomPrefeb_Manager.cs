using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Re_RoomPrefeb_Manager : MonoBehaviour
{
    public static Re_RoomPrefeb_Manager instance;

    public Transform[] roomBtn_Pos;
    int maxCount = 6;
    int count = -1;
    private void Awake()
    {
        instance = this;
    }

   public Transform InstansiateRoom()
    {
        count++;
        if(count > maxCount)
        {
            count = 0;
        }

        return roomBtn_Pos[count].transform;
        
    }
}

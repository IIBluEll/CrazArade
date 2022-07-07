using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BlinkingText1 : MonoBehaviour
{
    bool isInputAnyKey = false;
    private void Start()
    {
        if (Input.anyKeyDown && isInputAnyKey == false)
        {
            isInputAnyKey = true;
        }
    }

    private void Update()
    {

    }


}

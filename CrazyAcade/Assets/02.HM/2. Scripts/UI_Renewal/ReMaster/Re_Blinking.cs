using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class Re_Blinking : MonoBehaviour
{
    public TextMeshProUGUI anyKeyTxt;

    // Start is called before the first frame update
    void Start()
    {
        anyKeyTxt.GetComponent<TextMeshProUGUI>();
        StartCoroutine(FadeIn());
    }

    IEnumerator FadeIn()
    {
        for (int i = 0; i < 15; i++)
        {
            float f = i / 15.0f;

            Color a = anyKeyTxt.color;
            a.a = f;
            anyKeyTxt.color = a;
            yield return new WaitForSeconds(0.1f);
        }

        yield return new WaitForSeconds(1f);
        StartCoroutine(FadeOut());
    }

    IEnumerator FadeOut()
    {
        for (int i = 15; i >= 0; i--)
        {
            float f = i / 15.0f;
            Color a = anyKeyTxt.color;
            a.a = f;
            anyKeyTxt.color = a;
            yield return new WaitForSeconds(0.1f);
        }

        yield return new WaitForSeconds(0.5f);
        StartCoroutine(FadeIn());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

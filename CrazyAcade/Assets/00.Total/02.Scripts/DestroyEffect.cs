using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyEffect : MonoBehaviour
{
    public float Delay = 3f;

    void Start()
    {
        Destroy(gameObject, Delay);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}

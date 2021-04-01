using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateTrackDetect : MonoBehaviour
{
    public bool IsPlayerIn { get; private set; }    //玩家在范围内，可以进行输入

    private void Start()
    {
        IsPlayerIn = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(!IsPlayerIn)
        {
            if (collision.CompareTag("Player"))
                IsPlayerIn = true;
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (!IsPlayerIn)
        {
            if (collision.CompareTag("Player"))
                IsPlayerIn = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(IsPlayerIn)
        {
            if (collision.CompareTag("Player"))
                IsPlayerIn = false;
        }
    }
}

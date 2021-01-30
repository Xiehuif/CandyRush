using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoldPass : MonoBehaviour
{
    public GameObject stirringRods;
    void Update()
    {
        OnPress();
    }

    private void OnPress()
    {
        if (Input.GetKeyDown(KeyCode.W))//长按开始
        {
            stirringRods.GetComponent<BoxCollider2D>().enabled = false;
        }
        if (Input.GetKeyUp(KeyCode.W))//长按结束
        {
            stirringRods.GetComponent<BoxCollider2D>().enabled = true;
        }
    }
}

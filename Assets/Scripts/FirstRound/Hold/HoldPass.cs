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
        if (DetectPress())//长按开始
        {
            stirringRods.GetComponent<BoxCollider2D>().enabled = false;
        }
        else
        {
            stirringRods.GetComponent<BoxCollider2D>().enabled = true;
        }
    }
    private bool DetectPress()//长按函数,先用鼠标模拟,后期再换成触屏
    {
        if (Input.GetMouseButton(0))
            return true;
        else
            return false;
    }
}

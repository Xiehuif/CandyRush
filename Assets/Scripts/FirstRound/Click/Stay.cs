using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stay : MonoBehaviour
{
    public GameObject steam;//类似蒸汽的滞留机关
    public float speed = 1.0f;//到位速度
    private bool coroutineOpen = false;//协程状态
    void Start()
    {
        steam.SetActive(false);//初始隐藏
    }
    void Update()
    {
        OnClick();
    }
    private void OnClick()
    {
        if (Input.GetKeyDown(KeyCode.W) && !coroutineOpen)//无协程进行
        {
            StartCoroutine("ToStay");
            coroutineOpen = true;//协程进行中
        }
    }

    private IEnumerator ToStay()
    {
        steam.SetActive(true);
        for (float schedule = 0; schedule <= 1; schedule += speed * Time.deltaTime)
        {
            yield return 0;
        }
        steam.SetActive(false);
        coroutineOpen = false;//无协程进行
        yield break;
    }
}

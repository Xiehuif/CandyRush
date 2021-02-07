using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spin : MonoBehaviour
{
    public float speed = 1.0f;//到位速度
    public bool status = true;//关卡状态
    private bool coroutineOpen = false;//协程状态

    void Start()
    {
        if (!status)//status为false时初始化为放倒
        {
            this.transform.Rotate(0, 0, -90);
        }
    }

    void Update()
    {
        OnClick();
    }
    private void OnClick()
    {
        if (DetectClick() && !coroutineOpen)//无协程进行
        {
            if (status)
            {
                StartCoroutine("ToEnd");
            }
            else
            {
                StartCoroutine("ToOri");
            }
            status = !status;
            coroutineOpen = true;//协程进行中
        }
    }

    private IEnumerator ToEnd()
    {
        for (float schedule = 0; schedule <= 1; schedule += speed * Time.deltaTime)
        {
            this.transform.Rotate(0, 0, -90 * speed * Time.deltaTime);
            yield return 0;
        }
        coroutineOpen = false;//无协程进行
        yield break;
    }

    private IEnumerator ToOri()
    {
        for (float schedule = 0; schedule <= 1; schedule += speed * Time.deltaTime)
        {
            this.transform.Rotate(0, 0, 90 * speed * Time.deltaTime);
            yield return 0;
        }
        coroutineOpen = false;//无协程进行
        yield break;
    }
    private bool DetectClick()//单击函数,先用鼠标模拟,后期再换成触屏
    {
        if (Input.GetMouseButtonDown(0))
            return true;
        else
            return false;
    }
}

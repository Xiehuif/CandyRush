using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum ElevatorStatus
{
    upper,
    origin,
    lower
}

public class ElevatorScript : MonoBehaviour
{
    ElevatorStatus thisStatus; //当前位置状态
    public bool initialRunStatus; //初始运行方向，真向上，假向下
    public Transform origin; //初始位置
    public Transform upper;  //高位置
    public Transform lower;  //低位置
    private bool coroutineOpen;
    public float speed;      //运行速度


    public GameObject upperPlatform;
    public GameObject lowerPlatform;
    private Vector3 upperPlatformOri;
    private Vector3 lowerPlatformOri;
    private Vector3 delta;
    // Start is called before the first frame update
    void Start()
    {
        thisStatus = ElevatorStatus.origin;
        coroutineOpen = false; 
    }

    // Update is called once per frame
    void Update()
    {
        Click();
    }

    void Click()
    {
        if(DetectClick() && !coroutineOpen)
        {
            upperPlatformOri = upperPlatform.transform.position;
            lowerPlatformOri = lowerPlatform.transform.position;
            if(thisStatus == ElevatorStatus.lower)
            {
                delta = origin.position - lower.position;
                StartCoroutine("MoveTo");
                initialRunStatus = true;
                thisStatus = ElevatorStatus.origin;
                coroutineOpen = true;
                return;
            }
            if (thisStatus == ElevatorStatus.upper)
            {
                delta = origin.position - upper.position;
                StartCoroutine("MoveTo");
                initialRunStatus = false;
                thisStatus = ElevatorStatus.origin;
                coroutineOpen = true;
                return;
            }
            if(thisStatus == ElevatorStatus.origin)
            {
                if (initialRunStatus)
                {
                    delta = upper.position - origin.position;
                    thisStatus = ElevatorStatus.upper;
                }
                else
                {
                    delta = lower.position - origin.position;
                    thisStatus = ElevatorStatus.lower;
                }
                StartCoroutine("MoveTo");
                coroutineOpen = true;
                return;
            }
        }
    }

    private IEnumerator MoveTo()
    {
        for (float schedule = 0; schedule <= 1; schedule += speed * Time.deltaTime)
        {
            upperPlatform.transform.position = upperPlatformOri + delta * schedule;
            lowerPlatform.transform.position = lowerPlatformOri + delta * schedule;
            yield return 0;
        }
        upperPlatform.transform.position = upperPlatformOri + delta;
        lowerPlatform.transform.position = lowerPlatformOri + delta;
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

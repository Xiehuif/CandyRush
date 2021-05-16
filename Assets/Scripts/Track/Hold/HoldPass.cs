using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoldPass : MonoBehaviour
{
    private float cameraZoomEndPoint = 4.0f;//摄像机放大终点
    private float timeZoomEnd = 0.6f;//时间流速变缓终点

    public GameObject standby;
    public GameObject workBackground;
    public GameObject work;
    public GameObject smokeEffect;//烟雾特效
    public Transform end;//work目标点
    private Vector3 begin;//wor中初始位
    private float schedule = 0.0f;//动画进度
    private bool down = true;


    void Start()
    {
        begin = work.transform.position;
    }
    void Update()
    {
        OnPress();
    }

    private void OnPress()
    {
        if (DetectPress())//长按开始
        {
            this.tag = "Untagged";//tag置空
            work.SetActive(true);
            smokeEffect.SetActive(true);
            workBackground.SetActive(true);
            standby.SetActive(false);
            StirringAnimation();
        }
        else
        {
            this.tag = "Track";
            work.SetActive(false);
            smokeEffect.SetActive(false);
            workBackground.SetActive(false);
            standby.SetActive(true);
        }
    }

    private void StirringAnimation()
    {
        if (down)
        {
            schedule += 5 * Time.deltaTime;
            if (schedule > 1)//末尾去除误差
            {
                schedule = 1;
                down = !down;
            }
            work.transform.position = begin + (end.position - begin) * schedule;
        }
        else
        {
            schedule -= 5 * Time.deltaTime;
            if (schedule < 0)
            {
                schedule = 0;
                down = !down;
            }
            work.transform.position = end.position + (begin - end.position) * schedule;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")//检测碰撞物体是否为主角
        {
            CameraFollow cameraFollow = Camera.main.GetComponent<CameraFollow>();//获取主摄脚本
            StartCoroutine("ToBegin");
            Time.timeScale = timeZoomEnd;//修改时间流速
            Time.fixedDeltaTime = 0.02f * Time.timeScale;
            cameraFollow.smooth = 0.01f;//相机速度变慢
            cameraFollow.target = this.transform;//摄像机对准关卡
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Player")//检测碰撞物体是否为主角
        {
            CameraFollow cameraFollow = Camera.main.GetComponent<CameraFollow>();
            cameraFollow.target = other.transform;//摄像机归位
            cameraFollow.smooth = 0.1f;//相机速度回调
            Time.timeScale = 1.0F;//修改时间流速
            Time.fixedDeltaTime = 0.02f * Time.timeScale;
            StartCoroutine("ToEnd");
            Camera.main.orthographicSize = 5;//摄像机归位(消除误差)
        }
    }

    private IEnumerator ToBegin()
    {
        //摄像机拉近
        for (float schedule = 0; schedule <= 1; schedule += 3 * Time.deltaTime)
        {
            Camera.main.orthographicSize = 5f - (5f - cameraZoomEndPoint) * schedule;
            yield return 0;
        }
        yield break;
    }

    private IEnumerator ToEnd()
    {
        //摄像机归位
        for (float schedule = 0; schedule <= 1; schedule += 3 * Time.deltaTime)
        {
            Camera.main.orthographicSize = cameraZoomEndPoint + (5f - cameraZoomEndPoint) * schedule;
            yield return 0;
        }
        yield break;
    }


    private bool DetectPress()//长按函数,先用鼠标模拟,后期再换成触屏
    {
if (Input.GetMouseButton(0))
            return true;
        else
            return false;
    }
}

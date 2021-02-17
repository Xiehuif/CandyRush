using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoldControl : MonoBehaviour
{
    public float speed = 0.4f;//变温速度
    public bool coolDown = true;//加热or冷却
    public float safeUpperLimit = 0.4f;//安全加热进度上限
    public float safeLowerLimit = 0.15f;//安全加热进度下限
    private float currentTemperature = 0.5f;//初始进度位置
    public bool status = false;//关卡状态

    private float timer = 0;//关卡计时器

    public Transform cursor;//游标
    public Transform cursorUpperLimit;//游标上限
    public Transform cursorLowerLimit;//游标下限
    public Transform safeArea;//安全区
    public GameObject temperatureBar;//整个温度条物体


    void Start()
    {
        //根据加热or冷却模式来初始化安全区的位置
        if (coolDown)
        {
            safeArea.position = cursorLowerLimit.position + (cursorUpperLimit.position - cursorLowerLimit.position) * (safeUpperLimit - safeLowerLimit);
        }
        else
        {
            safeArea.position = cursorUpperLimit.position + (cursorLowerLimit.position - cursorUpperLimit.position) * (safeUpperLimit - safeLowerLimit);
        }
        //初始化游标位置(正中)
        cursor.position = cursorLowerLimit.position + (cursorUpperLimit.position - cursorLowerLimit.position) * currentTemperature;
        temperatureBar.SetActive(false);

    }

    void Update()
    {
        if (status)
        {
            timer += Time.deltaTime;//关卡计时器
            TemperatureControl();
            if (timer > 3)
            {
                DeathCheck();
            }
        }
    }
    private void TemperatureControl()//温度游标控制函数,自动左移,长按右移
    {
        cursor.position = cursorLowerLimit.position + (cursorUpperLimit.position - cursorLowerLimit.position) * currentTemperature;
        if (DetectPress())
        {
            if (currentTemperature < 1)
                currentTemperature += speed * Time.deltaTime;
        }
        else
        {
            if (currentTemperature > 0)
            {
                currentTemperature -= speed * Time.deltaTime;
            }
        }
    }

    private void DeathCheck()//死亡判定,进关卡3s后开始,给予玩家调整时间
    {
        if ((coolDown && currentTemperature > safeLowerLimit && currentTemperature < safeUpperLimit) || (!coolDown && currentTemperature < 1 - safeLowerLimit && currentTemperature > 1 - safeUpperLimit))
            return;
        else
            this.tag = "Track";//gameOver

    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")//检测碰撞物体是否为主角
        {
            status = true;//关卡开启
            timer = 0;
            temperatureBar.SetActive(true);
            StartCoroutine("ToBegin");
        }
    }
    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.tag == "Player")//检测碰撞物体是否为主角
        {
            temperatureBar.transform.position = new Vector3(other.transform.position.x, temperatureBar.transform.position.y, temperatureBar.transform.position.z);
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Player")//检测碰撞物体是否为主角
        {
            this.tag = "Untagged";
            temperatureBar.SetActive(false);
            status = false;//关卡关闭
            StartCoroutine("ToEnd");
            Camera.main.orthographicSize = 5;//摄像机归位(消除误差)
        }
    }

    private IEnumerator ToBegin()
    {
        //摄像机拉近
        for (float schedule = 0; schedule <= 1; schedule += 3 * Time.deltaTime)
        {
            Camera.main.orthographicSize = 5f - 1f * schedule;
            yield return 0;
        }
        yield break;
    }

    private IEnumerator ToEnd()
    {
        //摄像机归位
        for (float schedule = 0; schedule <= 1; schedule += 3 * Time.deltaTime)
        {
            Camera.main.orthographicSize = 4f + 1f * schedule;
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

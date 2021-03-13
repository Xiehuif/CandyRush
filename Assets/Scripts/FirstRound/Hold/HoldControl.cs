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
    private float cameraZoomEndPoint = 4.0f;//摄像机放大终点
    private float timeZoomEnd = 0.3f;//时间流速变缓终点
    public int targetAppearance = 0;//目标状态

    public PassCheck passCheck;


    public Transform cursor;//游标
    public Transform cursorUpperLimit;//游标上限
    public Transform cursorLowerLimit;//游标下限
    public Transform safeArea;//安全区
    public GameObject temperatureBar;//整个温度条物体
    public GameObject heatPrompt;//加热提示牌
    public GameObject coolPrompt;//冷却提示牌
    public GameObject prompt;//提示牌
    public GameObject smokeEffect;//烟雾特效

    void Start()
    {
        coolPrompt.SetActive(coolDown);
        heatPrompt.SetActive(!coolDown);
        //根据加热or冷却模式来初始化安全区的位置
        if (coolDown)
        {
            safeArea.position = cursorLowerLimit.position + (cursorUpperLimit.position - cursorLowerLimit.position) * (safeUpperLimit + safeLowerLimit) / 2;
        }
        else
        {
            safeArea.position = cursorUpperLimit.position + (cursorLowerLimit.position - cursorUpperLimit.position) * (safeUpperLimit + safeLowerLimit) / 2;
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
            if (timer > 1)//进入关卡3s后开始死亡检测
            {
                DeathCheck();
            }
        }
        if (passCheck.pass)
        {
            prompt.SetActive(false);
            NextAppearance();
            passCheck.pass = false;
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
        {
            this.tag = "Track";//gameOver
            Time.timeScale = 1.0F;//修改时间流速
            Time.fixedDeltaTime = 0.02f * Time.timeScale;
        }

    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")//检测碰撞物体是否为主角
        {
            status = true;//关卡开启
            timer = 0;//重新开始计时
            temperatureBar.SetActive(true);
            CameraFollow cameraFollow = Camera.main.GetComponent<CameraFollow>();//获取主摄脚本
            StartCoroutine("ToBegin");
            cameraFollow.smooth = 0.01f;//相机速度变慢
            cameraFollow.target = this.transform;//摄像机对准关卡
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Player")//检测碰撞物体是否为主角
        {
            this.tag = "Untagged";//tag置空
            temperatureBar.SetActive(false);
            status = false;//关卡关闭
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
        Time.timeScale = timeZoomEnd;//修改时间流速
        Time.fixedDeltaTime = 0.02f * Time.timeScale;
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

    private void NextAppearance()
    {
        smokeEffect.SetActive(true);
        AppearanceManager.Instance.ChangeAppearance(targetAppearance);
    }

    private bool DetectPress()//长按函数,先用鼠标模拟,后期再换成触屏
    {
        if (Input.GetMouseButton(0))
            return true;
        else
            return false;
    }

}

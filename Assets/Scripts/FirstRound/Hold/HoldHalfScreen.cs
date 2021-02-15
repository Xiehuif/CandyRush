using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoldHalfScreen : MonoBehaviour
{
    public Transform origin;//初相
    public Transform end;//反相
    private Vector3 originPos;//初相位置
    public float speed = 1.0f;//加工速度
    public bool heat = true;//加热or冷却
    private float schedule = 0.0f;//加工进度
    private bool status = false;//关卡状态
    void Start()
    {
        originPos = origin.position;//记录初始位置
    }
    void Update()
    {
        if (status)
        {
            ColorChange();
            OnPress();
        }
    }
    private void OnPress()
    {
        if (Input.GetKey(KeyCode.Q) && !Input.GetKey(KeyCode.E))//企图加热
        {
            if (heat)//符合条件, 开始加热
            {
                if (schedule < 1)
                    schedule += speed * Time.deltaTime;
                else
                    schedule = 1;
            }
            else//不符合条件,进度清零
            {
                schedule = 0;
            }
        }
        else if (Input.GetKey(KeyCode.E) && !Input.GetKey(KeyCode.Q))//企图冷却
        {
            if (!heat)//符合条件,开始冷却
            {
                if (schedule < 1)
                    schedule += speed * Time.deltaTime;
            }
            else//不符合条件,进度清零
            {
                schedule = 0;
            }
        }
        else if (schedule > 0)//松开按键
        {
            schedule -= 3 * speed * Time.deltaTime;
        }
        origin.position = originPos + (end.position - originPos) * schedule;
    }
    private void ColorChange()//温控颜色修改
    {
        if (Input.GetKeyDown(KeyCode.Q) && !Input.GetKey(KeyCode.E))//企图加热
        {
            this.GetComponent<SpriteRenderer>().color = new Color32(255, 9, 9, 50);//红色
        }
        else if (Input.GetKeyDown(KeyCode.E) && !Input.GetKey(KeyCode.Q))//企图冷却
        {
            this.GetComponent<SpriteRenderer>().color = new Color32(50, 0, 255, 50);//蓝色
        }
        if ((Input.GetKeyUp(KeyCode.Q) || Input.GetKeyUp(KeyCode.E)) && !(Input.GetKey(KeyCode.E) || Input.GetKey(KeyCode.Q)))//松开按键
        {
            this.GetComponent<SpriteRenderer>().color = new Color32(255, 255, 255, 25);//灰色
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")//检测碰撞物体是否为主角
        {
            status = true;
            Camera.main.orthographicSize = 3;//非常粗糙的摄像机拉近(
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Player")//检测碰撞物体是否为主角
        {
            status = false;
            Camera.main.orthographicSize = 5;
        }
    }
}

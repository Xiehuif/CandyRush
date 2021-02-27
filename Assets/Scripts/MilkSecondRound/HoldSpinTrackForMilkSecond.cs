using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoldSpinTrackForMilkSecond : MonoBehaviour
{
    public enum RotateDirection
    {
        Clockwise,
        CounterClockwise,
    };

    public Transform rotateCenter;
    public float speed;
    public RotateDirection rotateDirection;
    public float rotateAngle;  //正实数

    private float schedule;
    private Quaternion originRotation;
    // Start is called before the first frame update
    void Start()
    {
        if(rotateDirection == RotateDirection.Clockwise)
        {
            rotateAngle = Mathf.Abs(rotateAngle);
        }
        else
        {
            rotateAngle = -Mathf.Abs(rotateAngle);
        }
        schedule = 0;
        originRotation = this.transform.rotation;
    }

    // Update is called once per frame
    void Update()
    {
        HoldRotate();
    }
    
    void HoldRotate()
    {
        if (OnHold())
        {
            if (schedule >= 1) return;
            this.transform.RotateAround(rotateCenter.position, new Vector3(0, 0, 1), Time.deltaTime * speed * rotateAngle);
            schedule += Time.deltaTime * speed;
        }
        else
        {
            if (schedule <= 0) return;
            this.transform.RotateAround(rotateCenter.position, new Vector3(0, 0, 1), -Time.deltaTime * speed * rotateAngle);
            schedule -= Time.deltaTime * speed;
            if (schedule <= 0)
            {
                this.transform.rotation = originRotation;
                schedule = 0;
            }
        }
        return;
    }

    private bool OnHold()//单击函数,先用鼠标模拟,后期再换成触屏
    {
        if (Input.GetMouseButton(0))
            return true;
        else
            return false;
    }
}

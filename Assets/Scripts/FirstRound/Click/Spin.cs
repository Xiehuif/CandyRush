using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spin : MonoBehaviour
{
    public float speed = 1.0f;//到位速度
    public bool status = true;//关卡状态
    public Vector3 center = Vector3.zero;
    private float oriAngle;//初始角度
    private bool coroutineOpen = false;//协程状态

    void Start()
    {
        InputHandler.Instance.StartListener(this.gameObject, OnClick);
        center = transform.position + center;
        oriAngle = this.transform.localEulerAngles.z;//记录初始角度
        if (!status)//status为false时初始化为放倒
        {
            this.transform.Rotate(0, 0, -90);
        }

    }
    private void OnDisable()
    {
        if (InputHandler.IsInitialized)
            InputHandler.Instance.StopListener(this.gameObject, OnClick);
    }

    private void OnClick()
    {
        if (!coroutineOpen)//无协程进行
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
        for (float schedule = 0; schedule < 2; schedule += speed * Time.deltaTime)
        {
            if (schedule > 1)//末尾去除误差
            {
                break;
            }
            this.transform.RotateAround(center, Vector3.forward, -90 * speed * Time.deltaTime);
            yield return 0;
        }
        this.transform.RotateAround(center, Vector3.forward, oriAngle - 90 - this.transform.localEulerAngles.z);
        coroutineOpen = false;//无协程进行
        yield break;
    }

    private IEnumerator ToOri()
    {
        for (float schedule = 0; schedule < 2; schedule += speed * Time.deltaTime)
        {
            if (schedule > 1)//末尾去除误差
            {
                break;
            }
            this.transform.RotateAround(center, Vector3.forward, 90 * speed * Time.deltaTime);
            yield return 0;
        }
        this.transform.RotateAround(center, Vector3.forward, oriAngle - this.transform.localEulerAngles.z);

        coroutineOpen = false;//无协程进行
        yield break;
    }
#if UNITY_EDITOR
    protected void OnDrawGizmosSelected()
    {
        UnityEditor.Handles.color = Color.red;
        UnityEditor.Handles.DrawWireDisc(transform.position + center, Vector3.back, 0.2f);
    }
#endif
}

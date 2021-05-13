using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour
{
    public Transform origin;//初相
    public Transform end;//反相
    private Vector3 originPos;//初相位置
    private Vector3 endPos;//反相位置
    public float speed = 1.0f;//到位速度
    public bool status = true;//关卡状态
    public bool IsDirctionOp = false;
    private bool coroutineOpen = false;//协程状态
    void Start()
    {
        InputHandler.Instance.StartListener(this.gameObject, OnClick);
        originPos = origin.position;//记录初始位置
        endPos = end.position;
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
    private void Reverse()
    {
        origin.GetComponent<SurfaceEffector2D>().speed *= -1;
        GameObject left = origin.transform.Find("left_arrow").gameObject, right = origin.transform.Find("right_arrow").gameObject;
        if (origin.GetComponent<SurfaceEffector2D>().speed > 0)
        {
            left.SetActive(false);
            right.SetActive(true);
        }
        else
        {
            left.SetActive(true);
            right.SetActive(false);
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
            origin.position = originPos + (endPos - originPos) * schedule;
            yield return 0;
        }
        if (IsDirctionOp) { Reverse(); }
        origin.position = endPos;
        coroutineOpen = false;//无协程进行
        yield break;
    }

    private IEnumerator ToOri()
    {
        for (float schedule = 0; schedule <= 1; schedule += speed * Time.deltaTime)
        {
            if (schedule > 1)//末尾去除误差
            {
                break;
            }
            origin.position = endPos + (originPos - endPos) * schedule;
            yield return 0;
        }
        origin.position = originPos;
//#if IsDirctionOp
        if (IsDirctionOp) 
        {
            Reverse();
        }
//#endif
        coroutineOpen = false;//无协程进行
        yield break;
    }
#if UNITY_EDITOR
    protected void OnDrawGizmosSelected()
    {
        UnityEditor.Handles.color = Color.red;
        UnityEditor.Handles.DrawLine(origin.position, end.position);
    }
#endif
}

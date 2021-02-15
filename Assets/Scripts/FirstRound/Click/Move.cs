using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour
{
    public Transform origin;//初相
    public Transform end;//反相
    private Vector3 originPos;//初相位置
    public float speed = 1.0f;//到位速度
    public bool status = true;//关卡状态
    private bool coroutineOpen = false;//协程状态
    void Start()
    {
        InputHandler.Instance.StartListener(this.gameObject,OnClick);
        originPos = origin.position;//记录初始位置
    }
    private void OnDisable()
    {
        if(InputHandler.IsInitialized)
            InputHandler.Instance.StopListener(this.gameObject,OnClick);
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
        for (float schedule = 0; schedule <= 1; schedule += speed * Time.deltaTime)
        {
            origin.position = originPos + (end.position - originPos) * schedule;
            yield return 0;
        }
        coroutineOpen = false;//无协程进行
        yield break;
    }

    private IEnumerator ToOri()
    {
        for (float schedule = 0; schedule <= 1; schedule += speed * Time.deltaTime)
        {
            origin.position = end.position + (originPos - end.position) * schedule;
            yield return 0;
        }
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

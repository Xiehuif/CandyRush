using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LabMoveTrack : MonoBehaviour
{
    public Transform origin;//初相
    public Transform end;//反相
    private Vector3 originPos;//初相位置
    private Vector3 endPos;//反相位置
    public float speed = 1.0f;//到位速度
    public bool status = true;//关卡状态
    public bool IsDirctionOp = false;
    public float PlayerSpeed;
    private bool coroutineOpen = false;//协程状态
    private Transform m_PlayerTransform;
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
            if (status) StartCoroutine("ToEnd");
            else StartCoroutine("ToOri");
            
            status = !status;
            coroutineOpen = true;//协程进行中
        }
    }
    private void Reverse()
    {
        PlayerSpeed *= -1;
        GameObject left = origin.transform.Find("left_arrow").gameObject, right = origin.transform.Find("right_arrow").gameObject;
        left.SetActive(PlayerSpeed <= 0);
        right.SetActive(PlayerSpeed > 0);
    }
    private IEnumerator ToEnd()
    {
        if (m_PlayerTransform)
        {
            m_PlayerTransform.SetParent(origin);
            m_PlayerTransform.GetComponent<LabMov>().ChangeSpeed(0, gameObject.GetInstanceID());
        }

        for (float schedule = 0; schedule < 2; schedule += speed * Time.deltaTime)
        {
            if (schedule > 1)break;

            origin.position = originPos + (endPos - originPos) * schedule;
            yield return 0;
        }
        if (IsDirctionOp) { Reverse(); }
        origin.position = endPos;
        coroutineOpen = false;//无协程进行
        if (m_PlayerTransform)
        {
            m_PlayerTransform.SetParent(null);
            m_PlayerTransform.GetComponent<LabMov>().ChangeSpeed(PlayerSpeed, gameObject.GetInstanceID());
        }
        yield break;
    }

    private IEnumerator ToOri()
    {
        if (m_PlayerTransform)
        {
            m_PlayerTransform.SetParent(origin);
            m_PlayerTransform.GetComponent<LabMov>().ChangeSpeed(0, gameObject.GetInstanceID());
        }
        for (float schedule = 0; schedule <= 1; schedule += speed * Time.deltaTime)
        {
            if (schedule > 1)break;

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
        if (m_PlayerTransform)
        {
            m_PlayerTransform.SetParent(null);
            m_PlayerTransform.GetComponent<LabMov>().ChangeSpeed(PlayerSpeed, gameObject.GetInstanceID());
        }
        yield break;
    }
    

    public void Enter(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            m_PlayerTransform = collision.transform;
            collision.GetComponent<LabMov>().ChangeSpeed(PlayerSpeed, gameObject.GetInstanceID());
        }
    }

    public void Exit(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            m_PlayerTransform = null;
            collision.GetComponent<LabMov>().Recover(gameObject.GetInstanceID());
        }
    }
#if UNITY_EDITOR
    protected void OnDrawGizmosSelected()
    {
        UnityEditor.Handles.color = Color.red;
        UnityEditor.Handles.DrawLine(origin.position, end.position);

        Vector2[] points = gameObject.GetComponent<EdgeCollider2D>().points;
        Vector2 ori2end = end.localPosition - origin.localPosition;
        UnityEditor.Handles.color = Color.green;
        UnityEditor.Handles.DrawLine((Vector2)transform.parent.position + transform.parent.localScale * (points[0] + (Vector2)end.localPosition),
            (Vector2)transform.parent.position +(Vector2)transform.parent.localScale * ( points[1] + (Vector2)end.localPosition));
    }
#endif
}

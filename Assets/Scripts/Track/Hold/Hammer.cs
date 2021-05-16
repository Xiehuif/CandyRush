using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 锤子
/// </summary>
public class Hammer : MonoBehaviour
{
    public bool status = false;     //状态
    public Transform rotateCenter;  //旋转中心
    public Transform thron;     //刺

    [HideInInspector]
    public bool m_isRunningCo = false;  //正在旋转，只允许Thron访问
    private bool m_canClick = true;     //对点击响应
    private float m_speed = 180f;  //旋转速度

    private void Start()
    {
        if(status)
        {
            transform.rotation = Quaternion.Euler(0, 0, 180f);
            thron.localPosition = new Vector3(thron.localPosition.x, -thron.localPosition.y, thron.localPosition.z);
        }
    }

    private void Update()
    {
        if(m_canClick&&!m_isRunningCo&&Input.GetMouseButtonDown(0))
        {
            StartCoroutine(status ? ToOri() : ToEnd());
            status = !status;
        }
    }

    public void ThronHitPlayer()
    {
        GetScoreBonus();
        StartCoroutine(BeInvalid());
    }

    IEnumerator BeInvalid()
    {
        GetComponent<Collider2D>().enabled = false;
        thron.GetComponent<Collider2D>().enabled = false;
        m_canClick = false;

        SpriteRenderer spriteRenderer = transform.parent.GetComponent<SpriteRenderer>();
        Color colorBefore = spriteRenderer.color;
        spriteRenderer.color= Color.red;

        yield return new WaitForSeconds(3f);

        GetComponent<Collider2D>().enabled = true;
        thron.GetComponent<Collider2D>().enabled = true;
        m_canClick = true;
        spriteRenderer.color = colorBefore;
    }

    private void GetScoreBonus()
    {
        //获得分数加成
    }

    IEnumerator ToEnd()
    {
        m_isRunningCo = true;
        for(float t=0;t<1;t+=Time.deltaTime)
        {
            transform.RotateAround(rotateCenter.position, Vector3.forward, m_speed * Time.deltaTime);
            yield return 0;
        }
        transform.RotateAround(rotateCenter.position, Vector3.forward, 180f - transform.rotation.eulerAngles.z);
        m_isRunningCo = false;
        thron.localPosition = new Vector3(thron.localPosition.x, -thron.localPosition.y, thron.localPosition.z);
    }

    IEnumerator ToOri()
    {
        m_isRunningCo = true;
        for (float t = 0; t < 1; t += Time.deltaTime)
        {
            transform.RotateAround(rotateCenter.position, Vector3.back, m_speed * Time.deltaTime);
            yield return 0;
        }
        transform.RotateAround(rotateCenter.position, Vector3.forward,  - transform.rotation.eulerAngles.z);
        m_isRunningCo = false;
        thron.localPosition = new Vector3(thron.localPosition.x, -thron.localPosition.y, thron.localPosition.z);
    }

#if UNITY_EDITOR

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(rotateCenter.position, transform.position);
        Gizmos.DrawWireSphere(rotateCenter.position, 0.2f);
    }

#endif
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

/// <summary>
/// 切糖机器
/// </summary>
public class SlicingMachine : MonoBehaviour
{
    private int m_score=0;      //当前分数
    private bool m_drawScore = false;   //是否绘制分数
    private GUIStyle m_GUIStyle=new GUIStyle();

    private void Start()
    {
        m_GUIStyle.fontSize=30;
        m_GUIStyle.normal.textColor = Color.red;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            GetComponent<Collider2D>().enabled = false;
            StartCoroutine(StartCountClick(collision.GetComponent<Rigidbody2D>()));
        }
    }

    IEnumerator StartCountClick(Rigidbody2D rig2d)
    {
        m_drawScore = true;

        Vector3 velocityBefore = rig2d.velocity;
        rig2d.velocity = Vector3.zero;
        rig2d.constraints = RigidbodyConstraints2D.FreezeAll;

        for(float t=0;t<10f;t+=Time.deltaTime)
        {
            if (Input.GetMouseButtonDown(0))
                m_score++;
            yield return 0;
        }

        rig2d.constraints = RigidbodyConstraints2D.None;
        rig2d.velocity = velocityBefore;

        m_drawScore = false;

        gameObject.SetActive(false);
    }

    private void OnGUI()
    {
        if(m_drawScore)
        {
            GUI.color = Color.red;
            GUI.Label(new Rect(Screen.width / 2, Screen.height / 2, 200, 200), m_score.ToString(),m_GUIStyle);
        }
    }
}

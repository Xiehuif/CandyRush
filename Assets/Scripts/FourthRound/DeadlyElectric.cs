using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadlyElectric : MonoBehaviour
{
    [ReadOnly]
    public bool status = true;  //编辑时在脚本选项里调用ChangeStatus来改变

    private SpriteRenderer m_spriteRenderer;

    private void Start()
    {
        m_spriteRenderer = GetComponent<SpriteRenderer>();
        ChangeColor();
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (!status)
            return;
        else if (collision.CompareTag("Player"))
            collision.GetComponentInChildren<Death>().IsDeath();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            ChangeStatus();
        }
    }

    private void ChangeColor()
    {
#if UNITY_EDITOR
        if (!Application.isPlaying)
            m_spriteRenderer = GetComponent<SpriteRenderer>();
#endif
        if (!status)
            m_spriteRenderer.color = new Color(0.46f, 0.57f, 0.8f);
        else
            m_spriteRenderer.color = new Color(0, 0, 1f);
    }

    [ContextMenu("ChangeStatus")]
    private void ChangeStatus()
    {
        status = !status;
        ChangeColor();
    }
}

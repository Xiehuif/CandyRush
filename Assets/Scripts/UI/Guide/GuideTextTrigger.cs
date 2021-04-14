using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuideTextTrigger : MonoBehaviour
{
    public Transform target;
    public string content;

    private bool m_isFirstPass = true;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (m_isFirstPass &&collision.CompareTag("Player"))
        {
            if (target == null)
                TextShower.Instance.SetTarget(this.transform.position, content);
            else TextShower.Instance.SetTarget(target.position, content);
            Destroy(gameObject);
        }
    }
}

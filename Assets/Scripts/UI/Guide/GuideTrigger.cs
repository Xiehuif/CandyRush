using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuideTrigger : MonoBehaviour
{
    public Transform target;

    private bool m_isFirstPass;

    private void Start()
    {
        m_isFirstPass = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (target == null)
                CircleGuidanceController.Instance.SetTarget(this.transform.position);
            else CircleGuidanceController.Instance.SetTarget(target.position);
        }
    }
}

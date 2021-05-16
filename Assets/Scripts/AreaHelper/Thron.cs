using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Thron : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            Hammer hammer = transform.parent.GetComponent<Hammer>();
            if (hammer.m_isRunningCo)
            {
                transform.parent.GetComponent<Hammer>().ThronHitPlayer();
                Debug.Log("HIT");
            }
        }
    }
}

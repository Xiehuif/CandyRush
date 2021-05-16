using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FreezeArea : MonoBehaviour
{
    public float FreezeTime = 1f;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {

            collision.gameObject.transform.rotation =  Quaternion.identity;
            Rigidbody2D rig = collision.gameObject.GetComponent<Rigidbody2D>();
            rig.freezeRotation = true;
            TimeManager.Instance.DelayDo(() => { rig.freezeRotation = false; }, FreezeTime);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bounnce : MonoBehaviour
{
    public Vector2 Force;
    public float DelayTime = 1f;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            GameObject player = collision.gameObject;
            Rigidbody2D player_rig = player.GetComponent<Rigidbody2D>();
            player_rig.freezeRotation = true ;
            player.GetComponent<Rigidbody2D>().AddForce(Force, ForceMode2D.Impulse);
            TimeManager.Instance.DelayDo(() => { player_rig.freezeRotation = false; }, DelayTime);
        }
    }
}

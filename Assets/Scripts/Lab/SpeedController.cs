using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedController : MonoBehaviour
{
    public float Speed;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            collision.collider.GetComponent<LabMov>().ChangeSpeed(Speed, gameObject.GetInstanceID());
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            collision.collider.GetComponent<LabMov>().Recover(gameObject.GetInstanceID());
        }

    }
}

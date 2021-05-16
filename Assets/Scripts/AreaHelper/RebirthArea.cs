using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RebirthArea : MonoBehaviour
{
    public Vector2 Offset;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            AppearanceManager.Instance.OriAppearance = AppearanceManager.Instance.CurrentAppearance;
            Death.Instance.ChangeReBirthPos(transform.position);
        }
    }
#if UNITY_EDITOR
    protected void OnDrawGizmos()
    {
        UnityEditor.Handles.color = Color.blue;
        UnityEditor.Handles.DrawLine((Vector2)transform.position, (Vector2)transform.position + Offset);
    }
#endif
}

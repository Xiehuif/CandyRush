using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasePlayer : Singleton<BasePlayer>
{
    public bool OnGround = false;
    public float DectDistance;
    void Start()
    {
        if (DectDistance <= 0)
        {
            Debug.LogError("The DectDistance Is Invaild!");
            return;
        }
    }

    void Update()
    {
        OnGround = false;
        RaycastHit2D hit = Physics2D.Raycast(transform.position, new Vector2(0, -1f), DectDistance);
        if(hit)
        {
            GameObject road = hit.collider.gameObject;
            if(road.CompareTag("Track"))
            {
                OnGround = true;
            }
        }
    }
#if UNITY_EDITOR
    protected void OnDrawGizmosSelected()
    {
        UnityEditor.Handles.DrawLine(transform.position, transform.position + new Vector3(0, -DectDistance, 0));
    }
#endif
}

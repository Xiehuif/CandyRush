﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LabMov : MonoBehaviour
{
    public float distance = 4f;
    public Vector2 offset = Vector2.zero;
    private Rigidbody2D rigidbody2D;
    public float Speed;
    public bool HasLeave;
    private float LeaveTime;
    private void Start()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
    }
    private void Update()
    {
        if (HasLeave && LeaveTime < 0.2f)
        {
            LeaveTime += Time.deltaTime;
        }
    }
    void FixedUpdate()
    {
        RaycastHit2D[] hits = new RaycastHit2D[3];
        hits[0] =  Physics2D.Raycast((Vector2)transform.position + offset, -1 * transform.up,distance);
        hits[2] = Physics2D.Raycast((Vector2)transform.position - offset, -1 * transform.up, distance);
        hits[1] = Physics2D.Raycast((Vector2)transform.position, -1 * transform.up, distance);
        bool r1 = (hits[0].collider != null) && (hits[0].collider.CompareTag("Lab")),
            r2 = (hits[1].collider != null )&& (hits[1].collider.CompareTag("Lab")),
            r3 = (hits[2].collider != null) && hits[2].collider.CompareTag("Lab");
        if (!r1 && !r2 && !r3)
        {
            HasLeave = true;
            return;
        }
        Vector3 AverageNormal = Vector3.zero;
        if (r1) AverageNormal += (Vector3)hits[0].normal;
        if (r2) AverageNormal += (Vector3)hits[1].normal * 2;
        if (r3) AverageNormal += (Vector3)hits[2].normal;
        rigidbody2D.velocity = Vector3.Cross(AverageNormal.normalized, Vector3.back * -1f) * Speed;

        float LerpConstant = 0.2f;
        if(HasLeave && LeaveTime >= 0.2f)
        {
            LerpConstant = 0.6f;
            HasLeave = false;
            LeaveTime = 0;
        }
        if(r2)
            transform.right =Vector3.Lerp(transform.right, Vector3.Cross(hits[1].normal, Vector3.back * -1f), LerpConstant);
        else if(r1 && r3)
            transform.right = Vector3.Lerp(transform.right, Vector3.Cross((hits[0].normal + hits[2].normal).normalized, Vector3.back * -1f), LerpConstant);
    }

#if UNITY_EDITOR
    protected void OnDrawGizmosSelected()
    {
        for (int i = 0; i < 3; i++)
        {
            UnityEditor.Handles.DrawLine(transform.position + new Vector3((i - 1) * offset.x, 0, 0),
            transform.position + new Vector3((i - 1) * offset.x, -distance, 0));
        }
    }
#endif
}

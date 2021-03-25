using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[ExecuteInEditMode]
public class CircleMove : MonoBehaviour
{
    public float RotateSpeed;
    private Vector2 Center;
    public float Radius;
    private float rad;
    void Start()
    {
        Center = (Vector2)transform.position;
    }

    private void FixedUpdate()
    {
        rad += RotateSpeed * Time.fixedDeltaTime;
        rad %= 360;
        transform.position = Center +Radius * new Vector2(Mathf.Cos(rad), Mathf.Sin(rad)); 
    }
}

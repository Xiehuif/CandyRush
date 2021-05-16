using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrackTransform : MonoBehaviour
{
    public Transform Left, Right;
    public Transform Track;
    public Vector3 Scale;
    public double Distance;
    private float m_baseSize_X = 0.155f,m_baseSize_Y = 0.1616846f;
    private void OnValidate()
    {
        if (Scale.x == 0) return;
        if (Scale.magnitude == 0 || Scale.magnitude > 1e6) return;
        if (Left != null && Right != null)
        {
            Track.localScale = Scale;
            Right.localPosition = transform.localPosition + new Vector3((float)Distance * Scale.x, 0, 0);
            Right.localScale = new Vector3(Right.localScale.x,Scale.y,0);
            Left.localPosition = transform.localPosition - new Vector3((float)Distance * Scale.x, 0, 0);
            Left.localScale = new Vector3(Right.localScale.x, Scale.y, 0);
            
            this.GetComponent<CapsuleCollider2D>().size = new Vector2(m_baseSize_X  + (0.004f / Scale.x), m_baseSize_Y);
        }
    }
}

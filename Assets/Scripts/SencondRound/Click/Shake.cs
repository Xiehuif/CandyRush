using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shake : MonoBehaviour
{
    public Rigidbody2D rig2D;
    public Vector2 ShakeOffset;
    public float ShakeInterval;
    public AnimationCurve curve;
    private float m_currentTime;
    void FixedUpdate()
    {
        if (!BasePlayer.Instance.OnGround) return;
        if (m_currentTime > ShakeInterval)
        {
            Vector2 offset = new Vector2(ShakeOffset.x, ShakeOffset.y) * curve.Evaluate(Mathf.Abs(Mathf.Sin(Time.time)));
            rig2D.AddForce(offset, ForceMode2D.Impulse);
        }
        else m_currentTime += Time.fixedDeltaTime;
    }

}

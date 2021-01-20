using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("The Move Arugments")]
    public float XSpeed;
    public float YSpeed;
    public float Gravity;
    public Vector2 AdditonalForce;
    public bool OnGround;

    private float m_verSpeed, m_horSpeed;
    Rigidbody2D rig;


    private void Start()
    {
        m_verSpeed = 0;
        m_horSpeed = XSpeed;
        rig = GetComponent<Rigidbody2D>();
    }
    void Update()
    {
        OnGround = OnGroundFun();
    }
    void FixedUpdate()
    {
        Act();
        Move();
    }

    #region 帮助函数
    private void Act()
    {
        if(AdditonalForce.magnitude > 1e-2)
        {
            m_horSpeed += Time.fixedTime * AdditonalForce.x;
            m_verSpeed += Time.fixedTime * AdditonalForce.y;
        }
    }
    private bool OnGroundFun()
    {
        LayerMask layerMask = ~LayerMask.GetMask("IgnoreRaycast");
        for (int i = -1; i < 2; i++)
        {
            RaycastHit2D hit2D;
            hit2D = Physics2D.Raycast(transform.position + i * new Vector3(0.242f, 0, 0), new Vector2(0, -1), 1.25f, layerMask);
            if (hit2D) return true;
        }
        return false;
    }
    private void Move()
    {
        rig.MovePosition((Vector2)transform.position + new Vector2(m_horSpeed, m_verSpeed) * Time.fixedDeltaTime);

        if (!OnGround)
        {
            m_verSpeed -= Time.fixedDeltaTime * Gravity;
            m_verSpeed = Mathf.Clamp(m_verSpeed, -30, m_verSpeed);
        }
        else if (m_verSpeed < 0 && OnGround)
        {
            m_verSpeed = 0;
        }

        m_horSpeed = 0;
    }
    #endregion

    #region 体外调用
    public void AddForce(Vector2 force)
    {
        Vector2 clampForce = new Vector2(Mathf.Clamp(force.x, -100, 100), Mathf.Clamp(force.y, -100, 100));
        AdditonalForce = clampForce;
    }
    public void ClearForce()
    {
        AdditonalForce = Vector2.zero;
    }
    #endregion
#if UNITY_EDITOR
    private void OnDrawGizmosSelected()
    {
        UnityEditor.Handles.color = Color.red;
        UnityEditor.Handles.DrawLine(transform.position, transform.position + new Vector3(0, -1.25f, 0));
    }
#endif
}

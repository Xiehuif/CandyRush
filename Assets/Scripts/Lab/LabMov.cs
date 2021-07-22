using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LabMov : MonoBehaviour
{
    public float distance = 4f;
    public Vector2 offset = Vector2.zero;
    [SerializeField]
    private float Speed;
    [Header("碰撞体宽度")]
    public float Width;

    private float m_RealSpeed;
    private Rigidbody2D rigidbody2D;


    private bool HasLeave;
    private float LeaveTime;
    private int ChangeID;
    private List<Collider2D> colliders = new List<Collider2D>();



    public float GetRecentSpeed()
    {
        return m_RealSpeed;
    }
    //用于地图上机器（如加热器）临时的速度变化
    public void TempChangeSpeedByMachine(float speed)
    {
        ChangeSpeed(speed, ChangeID);
    }


    //记录改变ID
    public void ChangeSpeed(float speed, int ID)
    {
        if (speed < 0 || speed > 1e5) return;
        m_RealSpeed = speed;
        ChangeID = ID;
    }
    //判断控制权是否已经转移
    public void Recover(int ID)
    {
        if (ID == ChangeID)
            m_RealSpeed = Speed;
    }
    private void Start()
    {

        rigidbody2D = GetComponent<Rigidbody2D>();
        Recover(0);
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

        float angleOffset = Mathf.Abs(Mathf.Sin(transform.rotation.z)) * Width;
        RaycastHit2D[] hits = new RaycastHit2D[3];
        hits[0] = Physics2D.Raycast((Vector2)transform.position + offset, -1 * Vector3.up, distance + angleOffset);
        hits[2] = Physics2D.Raycast((Vector2)transform.position - offset, -1 * Vector3.up, distance + angleOffset);
        hits[1] = Physics2D.Raycast((Vector2)transform.position, -1 * Vector3.up, distance + angleOffset);
        bool r1 = (hits[0].collider != null) && (hits[0].collider.CompareTag("Lab")),
            r2 = (hits[1].collider != null) && (hits[1].collider.CompareTag("Lab")),
            r3 = (hits[2].collider != null) && hits[2].collider.CompareTag("Lab");
        if (!r1 && !r2 && !r3)
        {
            HasLeave = true;
            return;
        }
        //模拟重力.jpg
        bool OnGround = false;
        rigidbody2D.GetContacts(colliders);
        if (colliders != null)
        {
            foreach (Collider2D collider in colliders)
            {
                if (collider.CompareTag("Lab"))
                {
                    OnGround = true;
                    break;
                }
            }
        }
        //平均法线算一下
        Vector3 AverageNormal = Vector3.zero;
        if (r1) AverageNormal += (Vector3)hits[0].normal;
        if (r2) AverageNormal += (Vector3)hits[1].normal * 2;
        if (r3) AverageNormal += (Vector3)hits[2].normal;
        rigidbody2D.velocity = Vector3.Cross(AverageNormal.normalized, Vector3.back * -1f) * m_RealSpeed +
                                (OnGround ? Vector3.zero : AverageNormal.normalized * -1f);

        float LerpConstant = 0.2f;
        if (HasLeave && LeaveTime >= 0.2f)
        {
            // Debug.Log(transform.right);
            LerpConstant = 0.05f;
            HasLeave = false;
            LeaveTime = 0;
        }
        //转向
        transform.right = Vector3.Lerp(transform.right, Vector3.Cross(AverageNormal, Vector3.back * -1f), LerpConstant);

    }

#if UNITY_EDITOR
    protected void OnDrawGizmosSelected()
    {
        float angleOffset = Mathf.Abs(Mathf.Sin(transform.rotation.z)) * Width;
        for (int i = 0; i < 3; i++)
        {
            UnityEditor.Handles.DrawLine(transform.position + new Vector3((i - 1) * offset.x, 0, 0),
            transform.position + new Vector3((i - 1) * offset.x, -(distance + angleOffset), 0));
        }
    }
#endif
}

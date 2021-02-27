using UnityEngine;

public class HoldMoveTrack : MonoBehaviour
{
    public Transform origin;//初相
    public Transform end;//反相
    public float speed = 3f;//到位速度

    private Vector3 m_originPos;//初相位置
    private Vector3 m_endPos;//反相位置
    private Vector3 m_dir;//方向
    private readonly float m_backSpeed = 5f;//返回速度

    private void Start()
    {
        m_originPos = origin.position;
        m_endPos = end.position;
        m_dir = (m_endPos - m_originPos).normalized;
    }

    private void Update()
    {
        if (Input.GetMouseButton(0))
        {
            if (Vector3.Dot((transform.position - m_endPos), m_dir) > 0f)
                transform.position = m_endPos;
            else if (transform.position != m_endPos)
                transform.position += speed * m_dir * Time.deltaTime;
        }
        else
        {
            if (Vector3.Dot((transform.position - m_originPos), m_dir) < 0)
                transform.position = m_originPos;
            else if (transform.position != m_originPos)
                transform.position -= m_backSpeed * m_dir * Time.deltaTime;
        }
    }

#if UNITY_EDITOR

    protected void OnDrawGizmosSelected()
    {
        UnityEditor.Handles.color = Color.red;
        UnityEditor.Handles.DrawLine(origin.position, end.position);
    }

#endif
}
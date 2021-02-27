using UnityEngine;

/// <summary>
/// 一个尺子
/// </summary>
[ExecuteInEditMode]
public class EditorRuler : MonoBehaviour
{
    public Vector2 distance;    //两点的距离（按一定比例缩放）

    private Transform m_point1;
    private Transform m_point2;
    private float m_scaleFactor = 1f / 0.16f;

    private void Awake()
    {
        m_point1 = transform.GetChild(0);
        m_point2 = transform.GetChild(1);
        if (m_point1 == null || m_point2 == null)
            Debug.LogError("point not assgined!");
    }

    private void Update()
    {
        distance.x = (m_point1.position.x - m_point2.position.x) * m_scaleFactor;
        distance.y = (m_point1.position.y - m_point2.position.y) * m_scaleFactor;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawLine(m_point1.position, m_point2.position);
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(m_point1.position, 0.1f);
        Gizmos.DrawWireSphere(m_point2.position, 0.1f);
    }
}
using UnityEngine;

/// <summary>
/// 按住旋转组件
/// </summary>
public class HoldSpin : MonoBehaviour
{
    [Tooltip("旋转角度，可以是负数")]
    public float rotateAngle = 90f;
    [Tooltip("旋转中心")]
    public Transform rotateCenter;
    [Tooltip("旋转速度，正数")]
    public float rotateSpeed = 90f;

    private float m_backSpeed = 180f; //回旋速度
    private Vector3 m_originPos;    //记录原位置

    private void Start()
    {
        m_originPos = transform.position;
    }

    private void Update()
    {
        float curAngle = Vector3.SignedAngle(m_originPos - rotateCenter.position,
                transform.position - rotateCenter.position,Vector3.forward);
        if (Input.GetMouseButton(0))
        {
            if (Mathf.Abs(curAngle) > Mathf.Abs(rotateAngle))
                transform.RotateAround(rotateCenter.position, Vector3.forward, rotateAngle - curAngle);
            else if (Mathf.Abs(curAngle) < Mathf.Abs(rotateAngle))
                transform.RotateAround(rotateCenter.position, Vector3.forward, rotateSpeed * Mathf.Sign(rotateAngle) * Time.deltaTime);
        }else
        {
            float res = Mathf.Sign(rotateAngle) * curAngle;
            if (res > 0)
                transform.RotateAround(rotateCenter.position, Vector3.back, m_backSpeed * Mathf.Sign(rotateAngle) * Time.deltaTime);
            else if (res < 0)
                transform.RotateAround(rotateCenter.position, Vector3.back,curAngle);
        }
    }

#if UNITY_EDITOR

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, rotateCenter.position);
        Gizmos.DrawWireSphere(rotateCenter.position, 0.2f);
    }

#endif
}

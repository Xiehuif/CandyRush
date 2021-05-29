using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpinLab : MonoBehaviour
{
    public float speed = 1.0f;//到位速度
    public bool status = true;//关卡状态
    public Vector3 center = Vector3.zero;
    [Tooltip("旋转角度")]
    public float rotateAngle = 90f;
    private float oriAngle;//初始角度
    private bool coroutineOpen = false;//协程状态
    public bool IsDirctionOp = false;
    private Transform m_PlayerTransform;
    [SerializeField]
    float playerSpeed;
    void Start()
    {
        InputHandler.Instance.StartListener(this.gameObject, OnClick);
        center = transform.position + center;
        oriAngle = this.transform.localEulerAngles.z;//记录初始角度
        if (!status)//status为false时初始化为放倒
        {
            this.transform.Rotate(0, 0, -rotateAngle);
        }

    }
    private void OnDisable()
    {
        if (InputHandler.IsInitialized)
            InputHandler.Instance.StopListener(this.gameObject, OnClick);
    }

    private void OnClick()
    {
        if (!coroutineOpen)//无协程进行
        {
            if (status)
            {
                
                StartCoroutine("ToEnd");
            }
            else
            {
                StartCoroutine("ToOri");
                
            }
            status = !status;
            coroutineOpen = true;//协程进行中
        }
    }

    private IEnumerator ToEnd()
    {
        if (m_PlayerTransform)
        {
            m_PlayerTransform.SetParent(transform);
            m_PlayerTransform.GetComponent<LabMov>().ChangeSpeed(0, gameObject.GetInstanceID());
        }
        for (float schedule = 0; schedule < 2; schedule += speed * Time.deltaTime)
        {
            if (schedule > 1)//末尾去除误差
            {
                break;
            }
            this.transform.RotateAround(center, Vector3.forward, -rotateAngle * speed * Time.deltaTime);
            yield return 0;
        }
        this.transform.RotateAround(center, Vector3.forward, oriAngle - rotateAngle - this.transform.localEulerAngles.z);
        coroutineOpen = false;//无协程进行
        if (m_PlayerTransform)
        {
            m_PlayerTransform.SetParent(null);
            m_PlayerTransform.GetComponent<LabMov>().ChangeSpeed(playerSpeed, gameObject.GetInstanceID());
        }
        yield break;
    }

    private IEnumerator ToOri()
    {
        if (m_PlayerTransform)
        {
            m_PlayerTransform.SetParent(transform);
            m_PlayerTransform.GetComponent<LabMov>().ChangeSpeed(0, gameObject.GetInstanceID());
        }
        for (float schedule = 0; schedule < 2; schedule += speed * Time.deltaTime)
        {
            if (schedule > 1)//末尾去除误差
            {
                break;
            }
            this.transform.RotateAround(center, Vector3.forward, rotateAngle * speed * Time.deltaTime);
            yield return 0;
        }
        this.transform.RotateAround(center, Vector3.forward, oriAngle - this.transform.localEulerAngles.z);

        if (IsDirctionOp) { this.GetComponentInChildren<SurfaceEffector2D>().speed *= -1; }
        coroutineOpen = false;//无协程进行
        if (m_PlayerTransform)
        {
            m_PlayerTransform.SetParent(null);
            m_PlayerTransform.GetComponent<LabMov>().ChangeSpeed(playerSpeed, gameObject.GetInstanceID());
        }
        yield break;
    }
    
    public void disableSpin()
    {
        if (InputHandler.IsInitialized)
            InputHandler.Instance.StopListener(this.gameObject, OnClick);
    }

    public void enableSpin()
    {
        InputHandler.Instance.StartListener(this.gameObject, OnClick);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            m_PlayerTransform = collision.transform;
            m_PlayerTransform.rotation =new Quaternion(transform.rotation.x, transform.rotation.y,transform.rotation.z, transform.rotation.w);
            collision.collider.GetComponent<LabMov>().ChangeSpeed(playerSpeed, gameObject.GetInstanceID());
        }
    }

    


#if UNITY_EDITOR
    protected void OnDrawGizmosSelected()
    {
        UnityEditor.Handles.color = Color.red;
        UnityEditor.Handles.DrawWireDisc(transform.position + center, Vector3.back, 0.2f);
    }
#endif
}

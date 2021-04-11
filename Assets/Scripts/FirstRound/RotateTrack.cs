using UnityEngine;

public class RotateTrack : MonoBehaviour, IResetable
{
    [SerializeField]
    private Transform m_track;  //传送带部分

    [SerializeField]
    private Transform m_link;   //连接物部分

    [SerializeField]
    private Transform m_detect; //检测部分

    [SerializeField]
    private float m_startAngle; //初始旋转角

    [SerializeField]
    private bool m_isClockWise = true;

    [SerializeField]
    private float m_rotateSpeed;    //旋转速度

    private RotateTrackDetect m_rtk;    //由玩家是否在范围内来决定是否对玩家输入进行检测
    private SurfaceEffector2D m_trackEffector; //track的效应器
    private float m_curAngle;   //当前旋转角
    private bool m_isTrackRunning;  //track是否在运动
    private const float m_disBtTrack = 4f;    //旋转中心与track中点的距离
    private Rigidbody2D m_playerRig;    //玩家的刚体
    private Transform m_playerTrans;    //玩家的位移

    void IResetable.Reset()
    {
        m_curAngle = m_startAngle;
        SetTrackState(true);
        InitPos();
    }

    private void Start()
    {
        m_trackEffector = m_track.GetComponent<SurfaceEffector2D>();
        m_rtk = m_detect.GetComponent<RotateTrackDetect>();
        m_playerTrans = GameObject.FindWithTag("Player").transform;
        m_playerRig = m_playerTrans.GetComponent<Rigidbody2D>();

        Death.Instance.Add(gameObject);

        m_curAngle = m_startAngle;
        SetTrackState(true);
        InitPos();
    }

    private void Update()
    {
        if (m_rtk.IsPlayerIn)
        {
            if (Input.GetMouseButton(0))
            {
                m_curAngle = m_curAngle + (m_isClockWise ? -1 : 1) * m_rotateSpeed * Time.deltaTime;
                UpdatePosition();
                if (m_isTrackRunning)
                    m_playerRig.velocity = Vector3.zero;
                SetTrackState(false);
            }
            else
            {
                SetTrackState(true);
            }
        }
    }

    private void SetTrackState(bool state)
    {
        if (state == m_isTrackRunning)
            return;
        m_isTrackRunning = state;
        m_trackEffector.enabled = state;
    }

    /// <summary>
    /// 更新track与link的位置
    /// </summary>
    private void UpdatePosition()
    {
        Vector3 preDis = m_playerTrans.position - m_track.position;

        float radian = Mathf.Deg2Rad * m_curAngle;
        m_track.localPosition = new Vector3(m_disBtTrack * Mathf.Cos(radian),
            m_disBtTrack * Mathf.Sin(radian));

        m_playerTrans.position = m_track.position + preDis;

        m_link.localPosition = m_track.localPosition / 2;
        m_link.localRotation = Quaternion.Euler(0, 0, m_curAngle);
    }

    private void InitPos()
    {
        float radian = Mathf.Deg2Rad * m_curAngle;
        m_track.localPosition = new Vector3(m_disBtTrack * Mathf.Cos(radian),
            m_disBtTrack * Mathf.Sin(radian));
        m_link.localPosition = m_track.localPosition / 2;
        m_link.localRotation = Quaternion.Euler(0, 0, m_curAngle);
    }
}
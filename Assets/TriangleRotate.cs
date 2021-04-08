using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class TriangleRotate : MonoBehaviour,IResetable
{
    [SerializeField]
    public enum TriangleState
    {
        Thron,
        Steam,
        Road
    }

    /// <summary>
    /// 按顺时针计算的目标三角面元素（因为旋转是逆时针的）
    /// </summary>
    [SerializeField]
    private TriangleState[] m_containState = new TriangleState[3];

    [SerializeField]
    private GameObject m_steam; //蒸汽样本

    [SerializeField]
    private GameObject m_thron; //刺样本

    [SerializeField]
    private Transform m_signPart;  //指示标记部

    [SerializeField]
    private Sprite[] m_signSprites=new Sprite[3];   //0是刺，1是蒸汽，2是路，与enum顺序对应

    [SerializeField]
    private Transform m_animPart;   //动画部分

    private int m_curState;   //当前状态对应containeState的索引，只存在0，1，2三个值

    private int m_changeTimes;  //改变的次数，决定旋转次数

    private bool m_corouOpen; //协程是否开启

    private const float RotateFactor = 120f;    //基本角度

    private void Start()
    {
        Death.Instance.Add(gameObject);
        if (InputHandler.IsInitialized)
            InputHandler.Instance.StartListener(gameObject, OnClick);

        //错误检测
        if (m_containState.Length != 3)
            Debug.LogError("ERROR:The count of the state must be 3!");
        if (m_signSprites.Length != 3)
            Debug.LogError("ERROR:The count of the sprites must be 3!");

        //初始化图标
        for(int i=0;i<3;i++)
        {
            m_signPart.GetChild(i).Rotate(new Vector3(0, 0, -i * RotateFactor));
            m_signPart.GetChild(i).GetComponent<SpriteRenderer>().sprite = m_signSprites[(int)m_containState[i]];
        }

        m_curState = 0;
        m_changeTimes = 0;
        m_corouOpen = false;
        ChangeActiveByState();
        m_signPart.transform.rotation = Quaternion.Euler(0, 0, m_curState*RotateFactor);
    }

    public void Reset()
    {
        m_animPart.gameObject.SetActive(true);
        m_changeTimes = 0;
        StopAllCoroutines();
        m_corouOpen = false;
        m_signPart.transform.rotation = Quaternion.Euler(0, 0, m_curState * RotateFactor);
    }

    private void Update()
    {
        if(m_changeTimes!=0&&!m_corouOpen)
        {
            StartCoroutine(SignRotate());
        }
    }

    private void OnDisable()
    {
        if (InputHandler.IsInitialized)
            InputHandler.Instance.StopListener(gameObject, OnClick);
    }

    private void OnClick()
    {
        m_curState =(m_curState + 1) % 3;
        SelfRotate();
    }

    private void ChangeActiveByState()
    {
        for (int i = 0; i < 3; i++)
            m_animPart.GetChild(i).gameObject.SetActive(false);
        m_animPart.GetChild((int)m_containState[m_curState]).gameObject.SetActive(true);
        switch(m_containState[m_curState])
        {
            case TriangleState.Road:
                m_thron.SetActive(false);
                m_steam.SetActive(false);
                break;
            case TriangleState.Steam:
                m_steam.SetActive(true);
                m_thron.SetActive(false);
                break;
            case TriangleState.Thron:
                m_steam.SetActive(false);
                m_thron.SetActive(true);
                break;
            default:
                Debug.Log("curState not valid!");
                break;
        }
    }

    private void SelfRotate()
    {
        ChangeActiveByState();
        m_changeTimes++;
        m_animPart.gameObject.SetActive(false);
    }

    private IEnumerator SignRotate()
    {
        m_corouOpen = true;
        const float rotateTimeCost = 0.2f;
        const float rotateSpeed = RotateFactor / rotateTimeCost;
        var ori = m_signPart.transform.rotation.eulerAngles;
        for(float ti=0;ti<rotateTimeCost;ti+=Time.deltaTime)
        {
            m_signPart.transform.Rotate(new Vector3(0, 0,Time.deltaTime*rotateSpeed));
            yield return null;
        }
        m_signPart.transform.rotation = Quaternion.Euler(ori + new Vector3(0, 0, RotateFactor));
        m_changeTimes--;
        if (m_changeTimes == 0)
            m_animPart.gameObject.SetActive(true);
        m_corouOpen = false;
    }
}
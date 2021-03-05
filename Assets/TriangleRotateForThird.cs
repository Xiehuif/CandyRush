using UnityEngine;
using System.Collections.Generic;

public class TriangleRotateForThird : MonoBehaviour
{
    [SerializeField]
    public enum TriangleState
    {
        Thron,
        Road,
        Steam
    }

    public TriangleState startState;  //初始状态，仅仅在inspector内修改

    [SerializeField]
    private GameObject m_steamPart; //蒸汽部分

    [SerializeField]
    private GameObject m_thronPart; //刺部分

    [SerializeField]
    private Transform m_signPart;  //指示标记部分

    private TriangleState m_curState;   //当前状态

    private static readonly List<int> s_rotateAngles = new List<int>() { 120, 0, -120 };    //不同状态下指示部分应该旋转的角度

    private void Start()
    {
        if (InputHandler.IsInitialized)
            InputHandler.Instance.StartListener(gameObject, OnClick);
        m_curState = startState;
        ChangeActiveByState();
    }

    private void OnDisable()
    {
        if (InputHandler.IsInitialized)
            InputHandler.Instance.StopListener(gameObject, OnClick);
    }

    private void OnClick()
    {
        m_curState =(TriangleState)((int)(m_curState + 1) % 3);
        ChangeActiveByState();
    }

    private void ChangeActiveByState()
    {
        switch(m_curState)
        {
            case TriangleState.Road:
                m_thronPart.SetActive(false);
                m_steamPart.SetActive(false);
                break;
            case TriangleState.Steam:
                m_thronPart.SetActive(true);
                break;
            case TriangleState.Thron:
                m_thronPart.SetActive(true);
                break;
            default:
                Debug.Log("curState not valid!");
                break;
        }
        m_signPart.rotation = Quaternion.Euler(0, 0, s_rotateAngles[(int)m_curState]);
    }
}
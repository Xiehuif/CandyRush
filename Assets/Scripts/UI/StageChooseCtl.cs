using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using UnityEngine.UI;

/// <summary>
/// 关卡选择控制器
/// </summary>
public class StageChooseCtl : MonoBehaviour
{
    /// <summary>
    /// 当前选择的关卡
    /// </summary>
    private int curChooseStage=0;

    /// <summary>
    /// 数字图片渲染器
    /// </summary>
    [SerializeField]
    private Image number;

    /// <summary>
    /// 数字图片集
    /// </summary>
    [SerializeField]
    private List<Sprite> numberSprites;

    /// <summary>
    /// 场景总数量
    /// </summary>
    private int m_sceneCount;

    private void Start()
    {
        m_sceneCount = SceneManager.sceneCountInBuildSettings;
    }

    /// <summary>
    /// 开始游戏按钮回调
    /// </summary>
    public void Begin()
    {
        SceneManager.LoadScene(curChooseStage+1);
    }

    /// <summary>
    /// 向左选关按钮回调
    /// </summary>
    public void LeftStage()
    {
        if (curChooseStage > 0)
            curChooseStage--;
        OnStageChanged();
    }

    /// <summary>
    /// 向右选关按钮回调
    /// </summary>
    public void RightStage()
    {
        if (curChooseStage < m_sceneCount - 2)
            curChooseStage++;
        OnStageChanged();
    }

    /// <summary>
    /// 选择关卡改变回调
    /// </summary>
    private void OnStageChanged()
    {
        number.sprite = numberSprites[curChooseStage];
    }
}

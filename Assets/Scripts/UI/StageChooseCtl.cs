using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public class StageChooseCtl : MonoBehaviour
{
    [SerializeField]
    private int curChooseStage;

    [SerializeField]
    private GameObject BG;

    //[SerializeField]
    //private List<Sprite> BGSprites;

    private int m_sceneCount;

    //private SpriteRenderer m_BGSpriteRenderer;

    private void Start()
    {
        //m_BGSpriteRenderer = BG.GetComponent<SpriteRenderer>();
#if UNITY_EDITOR
        m_sceneCount = 3;
#else
        m_sceneCount = SceneManager.sceneCount;
#endif
        if (curChooseStage <= 0 || curChooseStage >= m_sceneCount)
            Debug.LogError("Invalid initialized curChooseStage!");
    }

    public void Begin()
    {
#if UNITY_EDITOR
        switch(curChooseStage)
        {
            case 1:
                SceneManager.LoadScene("FirstRound");
                break;
            case 2:
                SceneManager.LoadScene("NewSecondRound");
                break;
            case 3:
                SceneManager.LoadScene("NewThirdRound");
                break;
            default:
                Debug.LogError("Stage not exist!");
                break;
        }
#else
        SceneManager.LoadScene(curChooseStage);
#endif
    }

    public void LeftStage()
    {
        if (curChooseStage > 0)
            curChooseStage--;
        OnStageChanged();
    }

    public void RightStage()
    {
        if (curChooseStage < m_sceneCount - 1)
            curChooseStage++;
        OnStageChanged();
    }

    private void OnStageChanged()
    {
        //m_BGSpriteRenderer.sprite = BGSprites[curChooseStage - 1];
    }
}

using System;
using UnityEngine;

public class DyeCreator : MonoBehaviour
{
    public Transform[] PreCreatePoints;
    public Transform PiecesTransform;
    public Transform CreatePoint;
    public Transform Player;
    public float LeftEdge;
    public GameObject DyeSliderPrefab;
    public float CreateInterVal;
    public int CreateTimes;
    public float BaseWidth;
    private float m_time;
    private float m_score,m_wholeTimes;
    void Start()
    {
        if (Player == null) Debug.LogError("Player Transform IsMising!");
        m_score = 0;
        m_wholeTimes = CreateTimes;
        m_time = CreateInterVal;
        if (PreCreatePoints.Length == 0) Debug.Log("The Dye Don't Have Any PreCreatPoints!");
        else
        {
            for(int i = 0;i < PreCreatePoints.Length;i++)
            {
                GameObject temp = Instantiate(DyeSliderPrefab, PreCreatePoints[i]);
                temp.transform.parent = PiecesTransform;
                temp.transform.localScale = new Vector3(UnityEngine.Random.Range(0.4f, 1f), temp.transform.localScale.y, 1);
                DyeSlider dyeSlider = temp.GetComponent<DyeSlider>();

                dyeSlider.LeftEdge = LeftEdge + Player.transform.localPosition.x;
                dyeSlider.player = Player;
            }
        }
    }
    void Update()
    {
        if(m_time >= CreateInterVal)
        {
            m_wholeTimes--;
            m_time = 0;
            if(m_wholeTimes <= -3)
            {
                TimeManager.Instance.DelayDo(
                () =>
                {
                    TimeManager.Instance.Continue();
                    BasePlayer.Instance.GenerateSmoke();
                    AppearanceManager.Instance.ChangeAppearance(3);
                    AudioManager.Instance.PlaySoundByName("complete");
                }, 0.3f);
                this.gameObject.SetActive(false);
            }
            else if(m_wholeTimes <= 0) return;
            GameObject temp = Instantiate(DyeSliderPrefab,CreatePoint);
            temp.transform.parent = PiecesTransform; 
            temp.transform.localScale = new Vector3(UnityEngine.Random.Range(0.4f, 1f), temp.transform.localScale.y, 1);
            DyeSlider dyeSlider = temp.GetComponent<DyeSlider>();

            dyeSlider.LeftEdge = LeftEdge + Player.transform.localPosition.x;
            dyeSlider.player = Player;
        }
        else  m_time += Time.unscaledDeltaTime;
    }
    private void OnDisable()
    {
        if (m_score > CreateInterVal * 0.7f) ScoreManager.Instance.AddScore("Dye_Perfect");  
        else if (m_score > CreateInterVal * 0.5f) ScoreManager.Instance.AddScore("Dye_Nice");
        else ScoreManager.Instance.AddScore("Dye_Normal");
    }
    public void Add(float delta)
    {
        if (delta > 0) m_score += delta;
        else return;
    }
}

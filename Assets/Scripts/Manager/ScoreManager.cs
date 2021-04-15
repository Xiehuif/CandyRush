using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ScoreManager : Singleton<ScoreManager>
{
    [SerializeField]
    private Text TextUI;
    private int m_score;
    private const int MaxScore = 30000;

    //得分规范表
    private static readonly Dictionary<string, int> s_scoresDic = new Dictionary<string, int>()
    {
        {"CandyCut",5 },            //每次切糖
        {"NormalItem",10 },         //普通道具
        {"SpecialItem",30 },        //特殊道具
        {"CenterWind",5 },          //吹风从中心吹起

        {"Dye_Perfect",70 },        //染色 完美
        {"Dye_Nice",45 },           //优秀
        {"Dye_Normal",30 },         //合格

        {"Package_Perfect",60 },    //包装 完美
        {"Package_Nice",40 },       //优秀
        {"Package_Normal",30 },     //合格

        {"TC_Perfect",25 },         //温控 完美
        {"TC_Normal",15 }           //合格
    };

    private void SetScore(int tarS)
    {
        m_score = Mathf.Clamp(tarS, 0, MaxScore);
        UpdateUI(tarS);
    }

    private void UpdateUI(int tarS)
    {
        TextUI.text = tarS.ToString();
    }

    private void Start()
    {
        SetScore(0);
    }


    //唯一外界接口,增加分数
    public void AddScore(string name)
    {
        SetScore(m_score + s_scoresDic[name]);
    }
    public int GetScore() { return m_score; }
    public void SetScore(Death die) { SetScore(die.GetLastScore()); }

    /// <summary>
    /// 得到关卡总结评级
    /// </summary>
    /// <returns>0是S(或SS+),1是A+(或S),2是A,3是B,4是C,5是F</returns>
    public int GetRank()
    {
        //TODO
        return 0;
    }
}

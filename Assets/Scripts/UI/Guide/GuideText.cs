using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GuideText : MonoBehaviour
{
    public struct SingleContent
    {
        public List<string> contents;
        public SingleContent(List<string> contents)
        {
            this.contents = contents;
        }
    }
    /// <summary>
    /// 依赖contentloader为其注入信息
    /// </summary>
    public static SingleContent[][] s_contentLists;

    [SerializeField]
    private GuideAnimation m_guideAnimation;

    private Text m_text;

    private void Start()
    {
        m_text = GetComponent<Text>();
        ShowText(2, 0, 1);  //测试
    }

    /// <summary>
    /// 展示某一句话
    /// </summary>
    /// <param name="section">节数</param>
    /// <param name="index">对应的索引</param>
    /// <param name="sentence">某一句</param>
    public void ShowText(int section,int index,int sentence)
    {
        m_text.text = s_contentLists[section][index].contents[sentence];
    }
}

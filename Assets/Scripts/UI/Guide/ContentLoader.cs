using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml;

public class ContentLoader : MonoBehaviour
{
    private void Awake()
    {
        XmlDocument doc = new XmlDocument();
        doc.LoadXml((Resources.Load("GuideText") as TextAsset).text);
        XmlElement root = doc.DocumentElement;
        XmlNodeList lists = root.ChildNodes;
        GuideText.s_contentLists = new GuideText.SingleContent[lists.Count][];
        for(int i=0;i<lists.Count;i++)
        {
            XmlNodeList clists = lists[i].ChildNodes;
            GuideText.s_contentLists[i] = new GuideText.SingleContent[clists.Count];
            for(int j=0;j<clists.Count;j++)
            {
                GuideText.s_contentLists[i][j] = new GuideText.SingleContent(new List<string>());
                XmlNodeList sentences = clists[j].ChildNodes;
                foreach(XmlNode v in sentences)
                {
                    GuideText.s_contentLists[i][j].contents.Add(v.InnerText);
                }
            }
        }
    }

    private void Start()
    {
        Destroy(gameObject);
    }
}

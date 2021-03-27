using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
public class UITips : MonoBehaviour
{
    public int CurrentIndex = -1;
    public string FilePath;
    Dictionary<int, string> content = new Dictionary<int, string>();
    void Awake()
    {
        string[] strs = File.ReadAllLines(Application.streamingAssetsPath + FilePath);
        foreach(string s in strs)
        {
            if (s.Length < 2) continue;
            int temp = 0,i = 0;
            for(;i < s.Length; i++)
            {
                if (s[i] >= '0' && s[i] <= '9') temp = temp * 10 + (s[i] - '0');
                else break;
            }
            if (!content.ContainsKey(temp))
            {
                content.Add(temp, s.Substring(i + 1));
                Debug.Log(temp.ToString() + " " + content[temp]);
            }
        }
        if (strs.Length == 0) Debug.LogError("未找到对应文件！");
    }
    public string Read(int index)
    {
        if (index >= content.Count || index < 0) return "";
        CurrentIndex = index;
        return content[index];
    }
}

using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 关卡判断更简单
/// </summary>
public enum SceneIndex
{
    MENU,
    FIRST,
    SECOND,
    THIRD
}

public class UIManager : Singleton<UIManager>
{ 
    public void Succeed()
    {
        var s = (GameObject)Resources.Load("SuccessCanvas");
        Instantiate(s);
    }

    public void Fail()
    {
        var f = (GameObject)Resources.Load("FailCanvas");
        Instantiate(f);
    }
}
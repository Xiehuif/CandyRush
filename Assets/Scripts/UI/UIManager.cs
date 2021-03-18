using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

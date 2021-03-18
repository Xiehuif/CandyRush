using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadUIGenerator : MonoBehaviour
{
    private GameObject m_targetUI;

    private void OnLevelWasLoaded(int level)
    {
        var f = (GameObject)Resources.Load("LoadingCanvas");
        m_targetUI=Instantiate(f);
        TimeManager.Instance.Pause();
    }

    private void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            TimeManager.Instance.Continue();
            Destroy(m_targetUI);
            Destroy(this);
        }
    }
}

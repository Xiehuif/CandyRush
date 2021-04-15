using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadUIGenerator : MonoBehaviour
{
    private GameObject m_targetUI;
    private bool HasUI = false;
    private void Start()
    {
        var f = (GameObject)Resources.Load("LoadingCanvas");
        m_targetUI=Instantiate(f);
        TimeManager.Instance.Pause();
        HasUI = true;
    }

    private void Update()
    {
        if(Input.GetMouseButtonDown(0)&&HasUI)
        {
            HasUI = false;
            TimeManager.Instance.Continue();
            Destroy(m_targetUI);
            Destroy(this);
        }
    }
}

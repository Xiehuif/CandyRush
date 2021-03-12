using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StopButtom : MonoBehaviour
{
    private float previousTimeScale;
    private bool inGameStop;
    // Start is called before the first frame update
    void Start()
    {
        inGameStop = false;
        previousTimeScale = Time.timeScale;
    }

    public void BeingClick()
    {
        Debug.Log("CLICK");
        if (inGameStop)
        {
            Debug.Log("GAME RESTART FROM STOP");
            TimeManager.Instance.Continue();
            Time.timeScale = previousTimeScale;//流速恢复
            inGameStop = false;
        }
        else
        {
            Debug.Log("GAME STOP");
            previousTimeScale = Time.timeScale;
            TimeManager.Instance.Pause();
            inGameStop = true;
        }

    }

    // Update is called once per frame
    void Update()
    {

    }
}

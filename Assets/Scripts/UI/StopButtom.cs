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
            Time.timeScale = previousTimeScale;
            inGameStop = false;
        }
        else
        {
            Debug.Log("GAME STOP");
            previousTimeScale = Time.timeScale;
            Time.timeScale = 0;
            inGameStop = true;
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TmpSwitchLevels : MonoBehaviour
{

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            SceneManager.LoadScene("FirstRound");
        }
        if (Input.GetKeyDown(KeyCode.W))
        {
            SceneManager.LoadScene("NewSecondRound");
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            SceneManager.LoadScene("NewThirdRound");
        }
    }
}

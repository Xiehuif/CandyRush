using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameEndCtrl : MonoBehaviour
{
    [SerializeField]
    private GameObject parent;

    public void Restart()
    {
        GameObject.FindWithTag("Player").GetComponentInChildren<Death>().OnRestart();
        Destroy(parent);
    }

    public void Exit()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void NextStage()
    {
        int index = SceneTranlater.GetCurrentBuildIndex();
        if (index < 4)
            SceneManager.LoadScene(index + 1);
    }
}

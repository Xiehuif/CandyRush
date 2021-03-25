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
        string cur = SceneManager.GetActiveScene().name;
        int index = UIManager.Instance.stagesName.IndexOf(cur);
        if (index < 2)
            SceneManager.LoadScene(index + 1);
    }
}

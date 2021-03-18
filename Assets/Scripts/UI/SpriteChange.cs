using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SpriteChange : MonoBehaviour
{
    private string cur;

    [SerializeField]
    List<GameObject> objs;

    private void Start()
    {
        cur = SceneManager.GetActiveScene().name;
        int t = 0;
        switch (cur)
        {
            case "FirstRound":
                t=0;
                break;
            case "NewSecondRound":
                t = 1;
                break;
            case "NewThirdRound":
                t = 2;
                break;
        }
        objs[t].SetActive(true);
    }
}

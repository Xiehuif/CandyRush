using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InPackage : MonoBehaviour
{
    public bool locked;
    public bool inActive;
    public PackageArea.Quality checkQuality;
    public float length;
    public float speed;
    public GameObject box;
    // Start is called before the first frame update
    void Start()
    {
        locked = false;
        inActive = false;
        InputHandler.Instance.StartListener(this.gameObject, check);
    }
    private void OnDisable()
    {
        if (InputHandler.IsInitialized)
            InputHandler.Instance.StopListener(this.gameObject, check);
    }
    void check()
    {
        
        if (!locked && inActive)
        {
            box.SetActive(false);
            StartCoroutine("To");
            locked = true;
        }
    }

    public void GetScore()
    {
        int score = 0;
        switch (checkQuality)
        {
            case PackageArea.Quality.best:
                score = ScoreManager.s_scoresDic["Package_Perfect"];
                break;
            case PackageArea.Quality.great:
                score = ScoreManager.s_scoresDic["Package_Nice"];
                break;
            case PackageArea.Quality.bad:
                score = ScoreManager.s_scoresDic["Package_Normal"];
                break;
            default:
                Debug.LogWarning("Invalid quality!");
                break;
        }
        ScoreManager.Instance.AddScore(score);

    }

    private IEnumerator To()
    {
        for (float schedule = 0; schedule < 2; schedule += speed * Time.deltaTime)
        {
            if (schedule > 1)//末尾去除误差
            {
                break;
            }
            this.transform.position  = this.transform.position - new Vector3(0,length * schedule / 2,0);
            yield return 0;
        }


        Time.timeScale = 0; //Game End
        yield break;
    }
}

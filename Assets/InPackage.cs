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

    public GameObject Player;

    bool gameHasEnded;
    // Start is called before the first frame update
    void Start()
    {
        locked = false;
        inActive = false;
        gameHasEnded = false;
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
            Player.GetComponent<Rigidbody2D>().isKinematic = true;
            Player.GetComponent<Rigidbody2D>().velocity = new Vector2(0,0);
            
        }
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

        this.transform.position = this.transform.position - new Vector3(0, length  / 2, 0);
        switch (checkQuality)
        {
            case PackageArea.Quality.bad:
                ScoreManager.Instance.AddScore(ScoreManager.s_scoresDic["Package_Normal"]);
                break;
            case PackageArea.Quality.great:
                ScoreManager.Instance.AddScore(ScoreManager.s_scoresDic["Package_Nice"]);
                break;
            case PackageArea.Quality.best:
                ScoreManager.Instance.AddScore(ScoreManager.s_scoresDic["Package_Perfect"]);
                break;
            default:
                Debug.LogWarning("NO CORRECT QUALITY");
                break;
        }
        GameInterface.Succeed();
        yield break;
    }
}

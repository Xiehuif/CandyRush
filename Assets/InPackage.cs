using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InPackage : MonoBehaviour,IResetable
{
    public bool locked;
    public bool inActive;
    public PackageArea.Quality checkQuality;
    public float speed;
    public GameObject box;

    public GameObject Player;

    private Vector3 m_OriPos = Vector3.zero;
    public void Reset()
    {
        if (m_OriPos == Vector3.zero) return;
        this.transform.position = m_OriPos;
        gameObject.GetComponent<SpriteRenderer>().enabled = true;   
        locked = false;
        inActive = false;
        box.SetActive(true);
    }
    void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
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
            Player.GetComponent<Rigidbody2D>().isKinematic = true;
            Player.GetComponent<Rigidbody2D>().velocity = new Vector2(0,0);
            
        }
    }

    private IEnumerator To()
    {
        this.transform.position = Player.transform.position + new Vector3(0, 10, 0);
        gameObject.GetComponent<SpriteRenderer>().enabled = true;
        m_OriPos = this.transform.position;
        for (float schedule = 0; schedule < 1; schedule += speed * Time.deltaTime)
        {
            this.transform.position  = m_OriPos + (Player.transform.position - m_OriPos)*schedule;
            yield return 0;
        }

        this.transform.position = Player.transform.position;
        switch (checkQuality)
        {
            case PackageArea.Quality.bad:
                ScoreManager.Instance.AddScore("Package_Normal");
                break;
            case PackageArea.Quality.great:
                ScoreManager.Instance.AddScore("Package_Nice");
                break;
            case PackageArea.Quality.best:
                ScoreManager.Instance.AddScore("Package_Perfect");
                break;
            default:
                Debug.LogWarning("NO CORRECT QUALITY");
                break;
        }
        GameInterface.Succeed();
        yield break;
    }
}

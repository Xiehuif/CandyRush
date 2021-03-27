using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Death : Singleton<Death>
{
    private int m_score;
    private Vector3 m_RebirthPos;
    private int m_RebirthState;
    private bool m_deathing=false;
    public int GetLastScore() { return m_score; }
    public void ChangeReBirthPos(Vector3 pos)
    {
        m_score = ScoreManager.Instance.GetScore();
        m_RebirthPos = pos;
        m_RebirthState = AppearanceManager.Instance.OriAppearance;
    }
    public GameObject[] ItemsToReset;
    public GameObject player;
    void Start()
    {
        m_score = ScoreManager.Instance.GetScore();
        m_RebirthPos = transform.position;
        player = transform.parent.gameObject;
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Track"&&!m_deathing)
        {
            IsDeath();
        }
    }
    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.tag == "Track"&&!m_deathing)
        {
            IsDeath();
        }
    }
    public void IsDeath()
    {
        m_deathing = true;
        AudioManager.Instance.PlaySoundByName("dead");
        AppearanceManager.Instance.ReturnToOri();
        foreach (GameObject item in ItemsToReset)
        {
            item.SetActive(true);
        }
        StopMoving();
        UIManager.Instance.Fail();
    }

    public void OnRestart()
    {
        ScoreManager.Instance.SetScore(this);
        player.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.None;
        player.transform.position = new Vector3(m_RebirthPos.x, m_RebirthPos.y, 0);//回归初始位置
        player.transform.rotation = new Quaternion(0, 0, 0, 0);//初始旋转角
        player.GetComponent<Rigidbody2D>().isKinematic = false;
        TimeManager.Instance.Continue();
        m_deathing = false;
    }

    public void StopMoving()
    {
        player.GetComponent<Rigidbody2D>().velocity = Vector3.zero;//质心速度清零
        player.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;//角速度清零
    }
}

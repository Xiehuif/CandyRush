using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IResetable
{
    void Reset();
}

public class Death : Singleton<Death>
{
    private int m_score;
    private Vector3 m_RebirthPos;
    private int m_RebirthState;
    private bool m_deathing = false;
    public int GetLastScore() { return m_score; }
    public void ChangeReBirthPos(Vector3 pos)
    {
        m_score = ScoreManager.Instance.GetScore();
        m_RebirthPos = pos;
        m_RebirthState = AppearanceManager.Instance.OriAppearance;
    }
    public List<GameObject> ItemsToReset = new List<GameObject>();
    public GameObject player;
    void Start()
    {
        m_score = 0;
        m_RebirthPos = transform.position;
        if (transform.parent == null) player = GameObject.FindGameObjectWithTag("Player");
        else player = transform.parent.gameObject;
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Track" && !m_deathing)
        {
            IsDeath();
        }
    }
    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.tag == "Track" && !m_deathing)
        {
            IsDeath();
        }
    }
    public void IsDeath()
    {
        Debug.Log("Death");
        m_deathing = true;
        AudioManager.Instance.PlaySoundByName("dead");
        AppearanceManager.Instance.ReturnToOri();
        StopMoving();
        UIManager.Instance.Fail();
    }

    public void OnRestart()
    {
        foreach (GameObject item in ItemsToReset)
        {
            item.SetActive(true);
            IResetable resetable = item.GetComponentInChildren<IResetable>();
            if (resetable != null) resetable.Reset();
        }
        ScoreManager.Instance.SetScore(this);
        TimeManager.Instance.Continue();
        
        player.transform.position = new Vector3(m_RebirthPos.x, m_RebirthPos.y, 0);//回归初始位置
        player.transform.rotation = new Quaternion(0, 0, 0, 0);//初始旋转角
        player.GetComponent<Rigidbody2D>().velocity = Vector3.zero;//质心速度清零
        player.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;//角速度清零
        player.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.None;
        player.GetComponent<Rigidbody2D>().isKinematic = false;
        m_deathing = false;
    }
    public void Add(GameObject itemToReset)
    {
        if (ItemsToReset.Contains(itemToReset))
        {
            return;
        }
        else ItemsToReset.Add(itemToReset);
    }
    public void StopMoving()
    {
        player.GetComponent<Rigidbody2D>().velocity = Vector3.zero;//质心速度清零
        player.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;//角速度清零
    }
}

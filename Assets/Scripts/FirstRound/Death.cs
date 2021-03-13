using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Death : Singleton<Death>
{
    private Vector3 m_RebirthPos;
    public void ChangeReBirthPos(Vector3 pos)
    {
        m_RebirthPos = pos;
    }
    public GameObject[] ItemsToReset;
    public GameObject player;
    void Start()
    {
        m_RebirthPos = transform.position;
        player = transform.parent.gameObject;
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Track")
        {
            IsDeath();
        }
    }
    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.tag == "Track")
        {
            IsDeath();
        }
    }
    public void IsDeath()
    {
        AppearanceManager.Instance.ReturnToOri();
        foreach (GameObject item in ItemsToReset)
        {
            item.SetActive(true);
        }
        player.transform.position = new Vector3(m_RebirthPos.x, m_RebirthPos.y, 0);//回归初始位置
        player.transform.rotation = new Quaternion(0, 0, 0, 0);//初始旋转角
        player.GetComponent<Rigidbody2D>().velocity = Vector3.zero;//质心速度清零
        player.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;//角速度清零
        player.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.None;
        TimeManager.Instance.Continue();
    }
}

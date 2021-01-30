using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Death : MonoBehaviour
{
    public GameObject player;
    void Start()
    {
        player = transform.parent.gameObject;
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Track")
        {
            IsDeath();
        }
    }
    private void IsDeath()
    {
        player.transform.position = new Vector3(0, 0, 0);//回归初始位置
        player.transform.rotation = new Quaternion(0, 0, 0, 0);//初始旋转角
        player.GetComponent<Rigidbody2D>().velocity = Vector3.zero;//质心速度清零
        player.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;//角速度清零
        player.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.None;
    }
}

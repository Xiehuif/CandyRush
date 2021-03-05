using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxScrolling : MonoBehaviour
{
    //简陋的视差滚动
    public Transform target;//玩家位置
    void Update()
    {
        this.transform.position = target.position / 1.5f;
    }
}

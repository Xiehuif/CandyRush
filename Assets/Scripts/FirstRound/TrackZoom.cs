using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrackZoom : MonoBehaviour
{
    public SpriteRenderer head;//左
    public SpriteRenderer middle;//中
    public SpriteRenderer back;//右
    private float defaultZoom = 15;
    private float fixZoom = 1;//修正缩放
    void Start()
    {
        fixZoom = defaultZoom / this.transform.localScale.x;
        head.transform.localScale = new Vector3(head.transform.localScale.x * fixZoom, head.transform.localScale.y, head.transform.localScale.z);
        back.transform.localScale = new Vector3(back.transform.localScale.x * fixZoom, back.transform.localScale.y, back.transform.localScale.z);
    }
}

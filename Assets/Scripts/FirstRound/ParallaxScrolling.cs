using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxScrolling : MonoBehaviour
{
    public Transform target;//主摄位置
    public Transform[] backgroundLayer;
    private float parallaxCoefficient = 0.1f;
    void Update()
    {
        for (int i = 0; i < 4; i++)
            backgroundLayer[i].transform.position = target.position * (0.6f + i * parallaxCoefficient);
    }
}

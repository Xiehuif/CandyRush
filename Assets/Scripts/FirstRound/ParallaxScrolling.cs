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
        test();
        for (int i = 0; i < 4; i++)
            backgroundLayer[i].transform.position = target.position * (0.6f + i * parallaxCoefficient);
    }
    private void test()
    {
        if (Input.GetMouseButton(0))
        {
            Debug.Log(backgroundLayer[0].GetComponent<SpriteRenderer>().bounds.size.x);

            backgroundLayer[0].transform.position = new Vector3(backgroundLayer[0].GetComponent<SpriteRenderer>().bounds.size.x + backgroundLayer[0].transform.position.x, backgroundLayer[0].transform.position.y, backgroundLayer[0].transform.position.z);
        }
    }
}

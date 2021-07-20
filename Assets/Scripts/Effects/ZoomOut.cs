using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZoomOut : MonoBehaviour
{
    public float cameraZoomEndPoint = 10.0f;//摄像机拉远终点
    bool inZoomOut = false;


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")//检测碰撞物体是否为主角
        {
            StartCoroutine("ToBegin");
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Player")//检测碰撞物体是否为主角
        {
            StartCoroutine("ToEnd");
        }
    }
    private IEnumerator ToBegin()
    {
        while (inZoomOut) yield return 0;
        inZoomOut = true;
        //摄像机拉近
        for (float schedule = 0; schedule <= 1; schedule += 0.7f * Time.deltaTime)
        {
            Camera.main.orthographicSize = 5f - (5f - cameraZoomEndPoint) * schedule;
            yield return 0;
        }
        inZoomOut = false;
        yield break;
    }

    private IEnumerator ToEnd()
    {
        while (inZoomOut) yield return 0;
        inZoomOut = true;
        //摄像机归位
        for (float schedule = 0; schedule <= 1; schedule += 0.7f * Time.deltaTime)
        {
            Camera.main.orthographicSize = cameraZoomEndPoint + (5f - cameraZoomEndPoint) * schedule;
            yield return 0;
        }
        inZoomOut = false;
        yield break;
    }

}

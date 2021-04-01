using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamSet : MonoBehaviour
{
    public float speed;
    public float changeSize;
    private float oriSize;
    private float nowSize;
    // Start is called before the first frame update
    void Start()
    {
        oriSize = Camera.main.orthographicSize;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        StartCoroutine("changeCam");
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        StartCoroutine("changeCamBack");
    }
    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator changeCam()
    {
        for (float schedule = 0; schedule < 1; schedule += speed * Time.deltaTime)
        {
            Camera.main.orthographicSize = oriSize + schedule * changeSize;
            yield return 0;
        }
        Camera.main.orthographicSize = oriSize + changeSize;
        nowSize = Camera.main.orthographicSize;
        yield break;
    }
    IEnumerator changeCamBack()
    {
        for (float schedule = 0; schedule < 1; schedule += speed * Time.deltaTime)
        {
            Camera.main.orthographicSize = nowSize - schedule * changeSize;
            yield return 0;
        }
        Camera.main.orthographicSize = oriSize;
        yield break;
    }
}

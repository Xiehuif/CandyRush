using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CandyCutterKnifeController : MonoBehaviour
{
    private bool coroutineOpen;
    private Vector3 oriPosition;
    public float downLength;

    public float speed;
    public int cutTimes;
    public GameObject checker;
    // Start is called before the first frame update
    void Start()
    {
        coroutineOpen = false;
        oriPosition = this.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        Click();
    }

    void Click()
    {
        Debug.Log("TryStart");
        Debug.Log(checker.GetComponent<CandyCutterCheck>().onStop);
        if (DetectClick() && !coroutineOpen && checker.GetComponent<CandyCutterCheck>().onStop)
        {
            Debug.Log("start");
            cutTimes += 1;
            StartCoroutine("Cut");
            coroutineOpen = true;
        }
    }

    private IEnumerator Cut()
    {
        Debug.Log("StartCutDown");
        for (float schedule = 0; schedule <= 1; schedule += speed * Time.deltaTime)
        {
            if(schedule > 0.5)
            {
                this.transform.position += new Vector3(0, downLength * speed * Time.deltaTime);
            }
            else
            {
                this.transform.position -= new Vector3(0, downLength * speed * Time.deltaTime);
            }
            yield return 0;
        }
        this.transform.position = oriPosition;
        coroutineOpen = false;//无协程进行
        yield break;
    }

    private bool DetectClick()//单击函数,先用鼠标模拟,后期再换成触屏
    {
        if (Input.GetMouseButtonDown(0))
            return true;
        else
            return false;
    }
}

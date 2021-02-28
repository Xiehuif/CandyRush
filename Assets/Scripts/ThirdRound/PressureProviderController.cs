using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressureProviderController : MonoBehaviour
{
    public enum packageQuality
    {
        perfect,
        great,
        good,
    };

    public GameObject checker;
    public GameObject self;
    public Transform player;
    public float speed;

    private bool coroutineOpen;
    private Vector3 oriPosition;
    private Vector3 endPosition;
    public Transform end;
    public GameObject deathController;
    private float packagePrecision; //max = 3.6
    public packageQuality quality;
    // Start is called before the first frame update
    void Start()
    {
        coroutineOpen = false;
        oriPosition = this.transform.position;
        endPosition = end.position;
        InputHandler.Instance.StartListener(this.gameObject, Click);
    }

    private void OnDisable()
    {
        if (InputHandler.IsInitialized)
            InputHandler.Instance.StopListener(this.gameObject, Click);
    }



    // Update is called once per frame
    void Update()
    {
        Click();
    }

    void Click()
    {
        if(!coroutineOpen && checker.GetComponent<PackageAreaCheck>().startPackage)
        {
            StartCoroutine("MoveTo");
            coroutineOpen = true;
            packagePrecision = Mathf.Abs(this.transform.position.x - player.position.x);
            deathController.SetActive(false);
        }
    }
    /*
    private bool DetectClick()//单击函数,先用鼠标模拟,后期再换成触屏
    {
        if (Input.GetMouseButtonDown(0))
            return true;
        else
            return false;
    }
    */
    private IEnumerator MoveTo()
    {
        for (float schedule = 0; schedule <= 1; schedule += speed * Time.deltaTime)
        {
            if(schedule <= 0.5)
            {
                this.transform.position += (endPosition - oriPosition) * speed * Time.deltaTime;
            }
            else
            {
                this.transform.position += (oriPosition - endPosition) * speed * Time.deltaTime;
            }
            yield return 0;
        }
        this.transform.position = oriPosition;
        float score = ((float)3.6 - packagePrecision) / (float)3.6;
        if(score > 0.8)
        {
            quality = packageQuality.perfect;
            Debug.Log("perfect");
        }
        else if(score > 0.4)
        {
            quality = packageQuality.great;
            Debug.Log("great");
        }
        else
        {
            quality = packageQuality.good;
            Debug.Log("good");
        }
        QualityEffect();
        //coroutineOpen = false;  //无协程进行
        yield break;
    }
    private void QualityEffect()
    {
        if(quality == packageQuality.perfect)
        {
            self.GetComponent<Renderer>().material.color = Color.green;
        }
        if(quality == packageQuality.great)
        {
            self.GetComponent<Renderer>().material.color = Color.yellow;
        }
        if(quality == packageQuality.good)
        {
            self.GetComponent<Renderer>().material.color = Color.red;
        }
    }

}

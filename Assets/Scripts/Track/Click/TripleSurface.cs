using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TripleSurface : MonoBehaviour
{
    // Start is called before the first frame update
    private float deltaAngle;
    private float changeTimes;
    private bool coroutineOpen;
    private float inRotate;
    public float speed;
    private float z;
    bool AllowPass()
    {
        return (changeTimes % 3 == 0);
    }
    void Start()
    {
        Init();
        InputHandler.Instance.StartListener(this.gameObject, Click);
    }

    private void OnDisable()
    {
        if (InputHandler.IsInitialized)
            InputHandler.Instance.StopListener(this.gameObject, Click);
    }
    void Init()
    {
        changeTimes = 1;
        deltaAngle = -120;
        coroutineOpen = false;
        inRotate = 0;
        z = 0;
    }
    // Update is called once per frame

    private void Click()
    {
        if(!coroutineOpen)
        {
            inRotate = 0;
            StartCoroutine("MoveTo");
            changeTimes++;
            if (AllowPass())
            {
                this.tag = "Untagged";
            }
            else
            {
                this.tag = "Track";
            }
            coroutineOpen = true;
        }
    }

    private bool DetectClick()//单击函数,先用鼠标模拟,后期再换成触屏
    {
        if (Input.GetMouseButtonDown(0))
            return true;
        else
            return false;
    }

    private IEnumerator MoveTo()
    {
        for (float schedule = 0; schedule <= 1; schedule += speed * Time.deltaTime)
        {
            this.transform.Rotate(0, 0, (schedule - inRotate) * deltaAngle);
            inRotate = schedule;
            yield return 0;
        }
        z += deltaAngle;
        this.transform.rotation = Quaternion.Euler(0, 0, z);
        coroutineOpen = false;//无协程进行
        yield break;
    }
}

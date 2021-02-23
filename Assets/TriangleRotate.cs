using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriangleRotate : MonoBehaviour
{
    private bool coroutineOpen;
    public float speed;
    private float deltaAngle;
    private float inRotate;
    private float z;
    // Start is called before the first frame update
    void Start()
    {
        coroutineOpen = false;
        deltaAngle = 120;
        InputHandler.Instance.StartListener(this.gameObject, Click);
    }
    private void OnDisable()
    {
        if (InputHandler.IsInitialized)
            InputHandler.Instance.StopListener(this.gameObject, Click);
    }

    void Click()
    {
        if (!coroutineOpen)
        {
            z = this.transform.rotation.eulerAngles.z;
            inRotate = 0;
            StartCoroutine("Rotate");
            coroutineOpen = true;
            
        }
    }
    private IEnumerator Rotate()
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
    // Update is called once per frame
    void Update()
    {
        
    }
}

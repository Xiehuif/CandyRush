using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnifeMove : MonoBehaviour
{
    public enum KnifeStatus
    {
        Up,
        Down,
    };
    public KnifeStatus thisStatus;
    public CandyCutterCheck check;
    public Sprite cutUp;
    public Sprite cutDown;
    public bool tie;
    public KnifeMove another;
    public CandyCutterCheck scorer;
    public void PutDown()
    {
        thisStatus = KnifeStatus.Down;
        this.GetComponent<SpriteRenderer>().sprite = cutDown;
    }

    public void GetUp()
    {
        thisStatus = KnifeStatus.Up;
        this.GetComponent<SpriteRenderer>().sprite = cutUp;
    }
    // Start is called before the first frame update
    void Start()
    {
        if(this.name == "prev")
        {
            PutDown();
            tie = true;
        }
        else
        {
            GetUp();
            tie = false;
        }
    }
    bool DetectLeft()
    {
        if (Input.GetMouseButtonDown(0) && Input.mousePosition.x < (Screen.width / 2)) {
            Debug.Log("TouchLeft");
            return true; 
        }
        else return false;
    }

    bool DetectRight()
    {
        if (Input.GetMouseButtonDown(0) && Input.mousePosition.x > (Screen.width / 2)) {
            Debug.Log("TouchRight");
            return true;
            
        }
        else return false;
    }
    // Update is called once per frame
    void Update()
    {
        if (check.inCutting)
        {
            if (tie && DetectLeft() && thisStatus == KnifeStatus.Up)
            {
                Debug.Log("GettingUp");
                PutDown();
                another.GetUp();
                scorer.score += 1;
            }
            if (!tie && DetectRight() && thisStatus == KnifeStatus.Up)
            {
                Debug.Log("GettingUp");
                PutDown();
                another.GetUp();
                scorer.score += 1;
            }
        }
    }
}

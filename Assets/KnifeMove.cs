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
    public Sprite upFrame;
    public Sprite downFrame;
    public bool tie;
    public KnifeMove another;
    public CandyCutterCheck scorer;

    public RuntimeAnimatorController upController;
    public RuntimeAnimatorController downController;
    bool inTrans;
    public GameObject lightOfKnife;

    private Emit emitScript;
    private IEnumerator Down()
    {
        while(this.gameObject.GetComponent<SpriteRenderer>().sprite != downFrame)
        {
            yield return 0;
        }
        this.gameObject.GetComponent<Animator>().speed = 0;
        inTrans = false;
        Debug.Log("End Down  " + gameObject.name);
        yield break;
    }

    private IEnumerator Up()
    {
        while (this.gameObject.GetComponent<SpriteRenderer>().sprite != upFrame)
        {
            yield return 0;
        }
        this.gameObject.GetComponent<Animator>().speed = 0;
        inTrans = false;
        Debug.Log("End Up  " + gameObject.name);
        yield break;
    }
    public void PutDown()
    {
        thisStatus = KnifeStatus.Down;
        this.gameObject.GetComponent<Animator>().runtimeAnimatorController = downController;
        this.gameObject.GetComponent<Animator>().speed = 3f;
        lightOfKnife.SetActive(true);
        inTrans = true;
        StartCoroutine("Down");
        Debug.Log(gameObject.name + "  PutDown");
    }

    public void GetUp()
    {
        thisStatus = KnifeStatus.Up;
        this.gameObject.GetComponent<Animator>().runtimeAnimatorController = upController;
        this.gameObject.GetComponent<Animator>().speed = 3f;
        lightOfKnife.SetActive(false);
        inTrans = true;
        StartCoroutine("Up");
   
        Debug.Log(gameObject.name + "  GetUp");
    }
    // Start is called before the first frame update
    void Start()
    {
        emitScript = FindObjectOfType<Emit>();
        if (this.name == "right") 
        {
            PutDown();
            tie = true;
        }
        else
        {
            GetUp();
            tie = false;
        }
        inTrans = false;
    }
    bool DetectLeft()
    {
        if (Input.GetMouseButtonDown(0) && Input.mousePosition.x < (Screen.width / 2)) {
            return true;

        }
        else return false;
    }

    bool DetectRight()
    {
        if (Input.GetMouseButtonDown(0) && Input.mousePosition.x > (Screen.width / 2)) {
            return true;
            
        }
        else return false;
    }
    // Update is called once per frame
    void Update()
    {
        if (check.inCutting)
        {
            if (tie && DetectLeft() && thisStatus == KnifeStatus.Up && !inTrans)
            {
                PutDown();
                another.GetUp();
                emitScript.emitCreate();
                ScoreManager.Instance.AddScore("CandyCut");
            }
            if (!tie && DetectRight() && thisStatus == KnifeStatus.Up && !inTrans)
            {
                PutDown();
                another.GetUp();
                emitScript.emitCreate();
                ScoreManager.Instance.AddScore("CandyCut");
            }
        }
    }
}

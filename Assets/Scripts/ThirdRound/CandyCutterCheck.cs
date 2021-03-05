using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CandyCutterCheck : MonoBehaviour
{
    public bool inCutting;
    private Transform player;
    private Vector3 ori;
    public int score;
    // Start is called before the first frame update
    public Vector3 getDelta()
    {
        return (player.position - ori);
    }
    private void Start()
    {
        inCutting = false;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log(collision.gameObject.tag);
        if (collision.gameObject.tag == "Player")
        {
            player = collision.gameObject.transform;
            ori = player.position;
            inCutting = true;
            score = 0;
            Debug.Log("inCutting");
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            inCutting = false;
            Debug.Log("outCutting");
        }
    }
}

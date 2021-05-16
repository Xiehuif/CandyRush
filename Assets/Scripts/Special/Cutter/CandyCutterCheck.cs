using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CandyCutterCheck : MonoBehaviour
{
    public bool inCutting;
    private Transform player;
    private Vector3 ori;
    public int score;
    public SurfaceEffector2D trackEffector;
    private float oriEffectorSpeed;
    private Emit emitScript;
    public int targetAppearance = 5;//目标状态
    public GameObject smokeEffect;//烟雾特效

    public Vector3 getDelta()
    {
        return (player.position - ori);
    }
    private void Start()
    {
        oriEffectorSpeed = trackEffector.speed;
        inCutting = false;
        emitScript = FindObjectOfType<Emit>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            emitScript.emitInit();
            trackEffector.speed = 1;
            player = collision.gameObject.transform;
            ori = player.position;
            inCutting = true;
            score = 0;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            emitScript.clearEmits();
            trackEffector.speed = oriEffectorSpeed;
            inCutting = false;
            NextAppearance();
            Debug.Log("outCutting");
        }
    }

    private void NextAppearance()
    {
        smokeEffect.SetActive(true);
        AppearanceManager.Instance.ChangeAppearance(targetAppearance);
    }
}

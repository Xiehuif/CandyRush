using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CandyCutterCheck : MonoBehaviour
{
    public bool inCutting;
    private Transform player;
    private Vector3 ori;
    public int score;
    //public SurfaceEffector2D trackEffector; 旧版
    public LabMov trackEffector;
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
        trackEffector = GameObject.FindGameObjectWithTag("Player").GetComponent<LabMov>();
        
        inCutting = false;
        emitScript = FindObjectOfType<Emit>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            emitScript.emitInit();
            oriEffectorSpeed = trackEffector.GetRecentSpeed();
            trackEffector.TempChangeSpeedByMachine(1f);
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
            trackEffector.TempChangeSpeedByMachine(oriEffectorSpeed);
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

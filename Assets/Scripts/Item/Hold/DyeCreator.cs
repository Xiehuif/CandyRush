using System;
using System.Collections.Generic;
using UnityEngine;

public class DyeCreator : MonoBehaviour
{
    public Action OnEnd;
    public Transform CreatePoint;
    public Transform Player;
    public float LeftEdge,Offset;
    public GameObject DyeSliderPrefab;
    public float CreateInterVal,CreateTimes;
    private Vector3 m_LastCreatePos;
    private float time,upBoundry,bottomBoundry;
    private Vector2 m_canvasScale;
    void Start()
    {
        if(Player == null) Debug.LogError("Player Transform IsMising!");
        RectTransform rect = transform.parent.GetComponent<RectTransform>();
        m_canvasScale = new Vector2(rect.localScale.x,rect.localScale.y);
        LeftEdge = (-1) * rect.rect.width;
        time = CreateInterVal;
        m_LastCreatePos = transform.position;
    }
    void Update()
    {
        if(time > CreateInterVal)
        {
            CreateTimes--;
            time = 0;
            if(CreateTimes<=-3)
            {
                Debug.Log("Dye End");
                OnEnd();
                Destroy(gameObject);
            }
            else if(CreateTimes <= 0) return;
            GameObject temp = Instantiate(DyeSliderPrefab,CreatePoint);
            temp.transform.position += new Vector3(0,UnityEngine.Random.Range(-1,1f)*Offset,0);
            DyeSlider dyeSlider = temp.GetComponent<DyeSlider>();
            dyeSlider.left *= m_canvasScale.x;
            dyeSlider.right *= m_canvasScale.x;

            dyeSlider.LeftEdge = LeftEdge;
            dyeSlider.player = Player;
            m_LastCreatePos = temp.transform.position;
        }
        else  time += Time.unscaledDeltaTime;
    }
}

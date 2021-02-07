using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DyeCreator : MonoBehaviour
{
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
        RectTransform rect = transform.parent.GetComponent<RectTransform>();
        m_canvasScale = new Vector2(rect.localScale.x,rect.localScale.y);
        LeftEdge = (-1) * rect.rect.width;
        time = 0;
        m_LastCreatePos = transform.position;
    }
    void Update()
    {
        if(time > CreateInterVal)
        {
            if(CreateTimes <= 0)
            {
                Debug.Log("Dye End");
                Destroy(gameObject);
            }
            GameObject temp = Instantiate(DyeSliderPrefab,CreatePoint);
            temp.transform.position += new Vector3(0,Random.Range(-1,1f)*Offset,0);
            DyeSlider dyeSlider = temp.GetComponent<DyeSlider>();
            dyeSlider.left *= m_canvasScale.x;
            dyeSlider.right *= m_canvasScale.x;

            dyeSlider.LeftEdge = LeftEdge;
            dyeSlider.player = Player;
            m_LastCreatePos = temp.transform.position;
            CreateTimes--;
            time = 0;
        }
        else  time += Time.deltaTime;
    }
}

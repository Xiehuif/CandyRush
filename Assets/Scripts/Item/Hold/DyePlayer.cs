using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DyePlayer : MonoBehaviour
{
    public float MaxSpeed = 10f;
    public float UpBoundry,BottomBoundry;
    public float ratio,Gravity;
    private float m_verSpeed;
    void Start()
    {
        Debug.Log(transform.parent.parent.name);
        UpBoundry = (transform.parent.parent.GetComponent<RectTransform>().rect.height / 2f);
        BottomBoundry = (-1) * UpBoundry;
    }
    void Update()
    {
        if(Input.GetMouseButton(0))
        {
            m_verSpeed =  ratio;
        }
        else m_verSpeed =  Gravity;
        Mathf.Clamp(m_verSpeed,(-1) * MaxSpeed,MaxSpeed);
        if(transform.localPosition.y < UpBoundry)
        {
            if(transform.localPosition.y > BottomBoundry)
            {
                transform.localPosition += new Vector3(0,m_verSpeed * Time.unscaledDeltaTime,0);
            }
            else transform.localPosition = new Vector3(transform.localPosition.x, BottomBoundry,transform.localPosition.z);
            
        }
        else transform.localPosition = new Vector3(transform.localPosition.x, UpBoundry,transform.localPosition.z);
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DyePlayer : MonoBehaviour
{
    public float UpBoundry,BottomBoundry;
    public float ratio,Gravity;
    private float m_verSpeed;
    private bool ReachBoundary = false;
    void Start()
    {
        Debug.Log(transform.parent.parent.name);
    }
    void Update()
    {
        if(Input.GetMouseButton(0))
        {
            m_verSpeed =  ratio;
        }
        else m_verSpeed =  Gravity;
        transform.localPosition += new Vector3(0,m_verSpeed * Time.unscaledDeltaTime,0);
     }
    private void LateUpdate()
    {
        if (transform.localPosition.y >=  UpBoundry)
            transform.localPosition = new Vector3(transform.localPosition.x, UpBoundry, transform.localPosition.z);
        if (transform.localPosition.y <= BottomBoundry)
              transform.localPosition = new Vector3(transform.localPosition.x, BottomBoundry, transform.localPosition.z);


    }

}

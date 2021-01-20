using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Belt : Clickable
{
    private BeltArea m_beltArea; 
    private void Start()
    {
        m_beltArea = GetComponentInChildren<BeltArea>();
    }
    protected override void ClickEvent()
    {
        m_beltArea.ChangeDirection(m_beltArea.Force * (-1));
        Debug.Log(gameObject.name + " Change Its Direction");
    }
}

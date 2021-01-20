using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Clickable : MonoBehaviour
{
    virtual protected void ClickEvent() {}
    virtual protected void HoldEvent() { }
    private void OnMouseDown()
    {
        ClickEvent();
    }
    private void OnMouseDrag()
    {
        HoldEvent();
    }
}

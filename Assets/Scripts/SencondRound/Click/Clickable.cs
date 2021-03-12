using UnityEngine;

public class Clickable : MonoBehaviour
{
    virtual protected void ClickEvent() {}
    private void OnMouseDown()
    {
        ClickEvent();
    }
}

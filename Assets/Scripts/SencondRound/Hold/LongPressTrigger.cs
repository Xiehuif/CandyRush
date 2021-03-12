using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using System;
public class LongPressTrigger : UIBehaviour, IPointerDownHandler, IPointerUpHandler, IPointerExitHandler, IPointerClickHandler
{
    public float duration = 1.0f;
    public Action onLongPress;
    public Action<float> onRelease;
    private bool isPointerDown = false;
    private bool longPressTriggered = false;
    private float startTime;
    private void Update()
    {
        if (isPointerDown && !longPressTriggered)
        {
            if (Time.time - startTime > duration)
            {
                longPressTriggered = true;
                onLongPress.Invoke();
            }
        }
    }
    public void OnPointerDown(PointerEventData eventData)
    {
        startTime = Time.time;
        isPointerDown = true;
        longPressTriggered = false;
    }
    public void OnPointerUp(PointerEventData eventData)
    {
        isPointerDown = false;
        onRelease(Time.time - startTime);
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        isPointerDown = false;
    }
    public void OnPointerClick(PointerEventData eventData)
    {

    }
}
using UnityEngine;

public class HoldItem : MonoBehaviour
{
    public float duration;
    virtual protected void HoldEvent (){}
    virtual protected void Start()
    {
        LongPressTrigger trigger = gameObject.AddComponent<LongPressTrigger>();
        trigger.onLongPress.AddListener(HoldEvent);
        trigger.duration = duration;
    }
}

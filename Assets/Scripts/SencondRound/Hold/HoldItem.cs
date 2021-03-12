using UnityEngine;

public class HoldItem : MonoBehaviour
{
    public float duration;
    virtual protected void HoldEvent (){}
    virtual protected void Start()
    {
        LongPressTrigger trigger = gameObject.AddComponent<LongPressTrigger>();
        trigger.onLongPress += (HoldEvent);
        trigger.duration = duration;
    }
    virtual protected  void OnDisable()
    {
        gameObject.GetComponent<LongPressTrigger>().enabled = false;
    }
    virtual protected void OnDestroy()
    {
        Destroy(gameObject.GetComponent<LongPressTrigger>());
    }
}

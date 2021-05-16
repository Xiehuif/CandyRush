using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasePlayer : Singleton<BasePlayer>
{
    public bool OnGround = false,flag = false;
    public GameObject SmokeEffect;
    public float DectDistance;
    private Rigidbody2D rig;
    private List<Collider2D> colliders = new List<Collider2D>();
    void Start()
    {
        if (DectDistance <= 0)
        {
            Debug.LogError("The DectDistance Is Invaild!");
            return;
        }
        rig = GetComponent<Rigidbody2D>();
    }
    public void GenerateSmoke()
    {
        GameObject smoke = Instantiate(SmokeEffect, transform.position + new Vector3(-0.41f,1.185f,0), Quaternion.identity);
        smoke.GetComponent<AnimationAutoExit>().ShouldInitHide = false;
        TimeManager.Instance.DelayDo(() => { Destroy(smoke); },1f);
    }
    void Update()
    {
        flag = false;
        if (OnGround)
        {
            rig.freezeRotation = false;
            return;
        }
        for (int i = 0; i < 3; i++)
        {
            RaycastHit2D hit = Physics2D.Raycast(transform.position, new Vector2((i - 1) * 0.7f, -1f), DectDistance);
            if (hit)
            {
                GameObject road = hit.collider.gameObject;
                if (road.CompareTag("Track"))
                    flag = true;
            }
        }
        if (!flag) rig.freezeRotation = true;
        else rig.freezeRotation = false;
    }
    private void FixedUpdate()
    {
        OnGround = false;
        rig.GetContacts(colliders);
        foreach(Collider2D collider in colliders)
        {
            if(collider.CompareTag("Track"))
            {
                OnGround = true;
                break;
            }
        }

    }


#if UNITY_EDITOR
    protected void OnDrawGizmosSelected()
    {
        for (int i = 0; i < 3; i++)
        {
            UnityEditor.Handles.DrawLine(transform.position + new Vector3((i - 1) * 0.7f, 0, 0),
            transform.position + new Vector3((i - 1) * 0.7f, -DectDistance, 0));
        }
    }
#endif
}

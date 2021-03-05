using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationAutoExit : MonoBehaviour
{
    private Animator anim;
    void Start()
    {
        anim = this.GetComponent<Animator>();
        this.gameObject.SetActive(false);//初始隐藏
    }

    void Update()
    {
        if (anim.GetCurrentAnimatorStateInfo(0).normalizedTime > 1.0f)//播完隐藏
            this.gameObject.SetActive(false);
    }
}

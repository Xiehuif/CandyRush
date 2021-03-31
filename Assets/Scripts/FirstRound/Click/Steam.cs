using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Steam : MonoBehaviour
{
    public GameObject collisionBody;//碰撞体
    public GameObject wheel;//滚轮
    public Animator steam;//动画所在
    public float speed = 1.0f;//到位速度
    public float rotatingSpeed = 80.0f;//滚轮转速
    private bool coroutineOpen = false;//协程状态
    void Start()
    {
        InputHandler.Instance.StartListener(this.gameObject, OnClick);
        collisionBody.SetActive(false);//初始隐藏
    }
    private void OnClick()
    {
        if (!coroutineOpen)//无协程进行
        {
            StartCoroutine("ToStay");
            coroutineOpen = true;//协程进行中
        }
    }
    void Update()
    {
        //滚轮旋转
        wheel.transform.RotateAround(wheel.transform.position, Vector3.forward, rotatingSpeed * Time.deltaTime);
    }
    private IEnumerator ToStay()
    {
        collisionBody.SetActive(true);
        rotatingSpeed *= 4;//加速旋转
        yield return new WaitForSeconds(GetLengthByName(steam, "蒸汽开启"));
        // for (float schedule = 0; schedule <= 1; schedule += speed * Time.deltaTime)
        // {
        //     yield return 0;
        // }
        rotatingSpeed /= 4;//归速
        collisionBody.SetActive(false);
        coroutineOpen = false;//无协程进行
        yield break;
    }

    private float GetLengthByName(Animator animator, string name)
    {
        float length = 0;
        AnimationClip[] clips = animator.runtimeAnimatorController.animationClips;
        foreach (AnimationClip clip in clips)
        {
            if (clip.name.Equals(name))
            {
                length = clip.length;
                break;
            }
        }
        return length;
    }

    private void OnDisable()
    {
        if (InputHandler.IsInitialized)
            InputHandler.Instance.StopListener(this.gameObject, OnClick);
    }
}

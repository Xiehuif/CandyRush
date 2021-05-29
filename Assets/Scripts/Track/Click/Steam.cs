using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Steam : MonoBehaviour,IResetable,IScoreGiver
{
    public float GetScore() { return 5; }
    public string GetTag() { return ""; }
    public GameObject collisionBody;//碰撞体
    public GameObject wheel;//滚轮
    public GameObject steamStandby;//待机蒸汽
    public Animator steam;//蒸汽动画
    private const float StartRotateSpeed = 100.0f;  //初始转速
    private float rotatingSpeed;//滚轮转速
    private bool coroutineOpen = false;//协程状态

    public bool fixedPath; //fixed path enabled
    public void Reset()
    {
        StopAllCoroutines();
        rotatingSpeed = StartRotateSpeed;
        collisionBody.SetActive(false);
        steamStandby.SetActive(true);
        coroutineOpen = false;//无协程进行
    }

    void Start()
    {
        InputHandler.Instance.StartListener(this.gameObject, OnClick);
        collisionBody.SetActive(false);//初始隐藏
        rotatingSpeed = StartRotateSpeed;
        Death.Instance.Add(gameObject);
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
        steamStandby.SetActive(false);
        collisionBody.SetActive(true);
        rotatingSpeed *= 4;//加速旋转
        yield return new WaitForSeconds(GetLengthByName(steam, "蒸汽开启"));
        rotatingSpeed /= 4;//归速
        if(fixedPath && !collisionBody.GetComponent<FixedPath>().isPlayerIn())collisionBody.SetActive(false);
        steamStandby.SetActive(true);
        coroutineOpen = false;//无协程进行
        yield break;
    }

    //获取动画长度
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

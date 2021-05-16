using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuideAnimation : Singleton<GuideAnimation>
{
    [SerializeField] private Animator guideHappy;
    [SerializeField] private Animator guideMad;
    [SerializeField] private Animator click;
    [SerializeField] private Animator hold;

    //点击引导
    public void ClickAnimation(float schedule)
    {
        AnimationPlayBySchedule(guideHappy, "guideHappy", schedule);
        AnimationPlayBySchedule(click, "Click", schedule);
    }

    //长按引导
    public void HoldAnimation(float schedule)
    {
        AnimationPlayBySchedule(guideHappy, "guideHappy", schedule);
        AnimationPlayBySchedule(hold, "Hold", schedule);
    }

    private void AnimationPlayBySchedule(Animator aniamation, string str, float schedule)
    {

        aniamation.Play(str, 0, schedule);
    }

}

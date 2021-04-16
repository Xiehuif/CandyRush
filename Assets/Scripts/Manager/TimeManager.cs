using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TimeManager : Singleton<TimeManager>
{
    public void ChangeRate(float ratio)
    {
        if(ratio > 0&&ratio < 2)
        {
            InputHandler.Instance.CanClick = false;
            Time.timeScale = ratio;
        }
    }
    public void Pause()
    {
        InputHandler.Instance.CanClick = false;
        Time.timeScale = 0;
    }
    public void Continue()
    {
        DelayDo(() =>
        {
            InputHandler.Instance.CanClick = true;
        }, 0.02f);
        Time.timeScale = 1;
    }
    protected IEnumerator DelayDoTimeCoroutine(UnityAction action, float time)
    {
        yield return new WaitForSecondsRealtime(time);
        if (action != null)
            action.Invoke();
    }

    public void DelayDo(UnityAction action, float time)
    {
        StartCoroutine(DelayDoTimeCoroutine(action, time));
    }
}

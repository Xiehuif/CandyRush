using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeManager : Singleton<TimeManager>
{
    public void Pause()
    {
        InputHandler.Instance.CanClick = false;
        Time.timeScale = 0;
    }
    public void Continue()
    {
        InputHandler.Instance.CanClick = true;
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

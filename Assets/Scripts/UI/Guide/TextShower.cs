﻿using System.Collections;
using UnityEngine;
using UnityEngine.UI;
public class TextShower : Singleton<TextShower>
{
    public GameObject TextGuider;
    public Text text;
    private Vector2 WorldToCanvasPos(Canvas canvas, Vector3 world)
    {
        Vector2 position;
        world = Camera.main.WorldToScreenPoint(world);
        RectTransformUtility.ScreenPointToLocalPointInRectangle(canvas.transform as RectTransform,
            world, Camera.main, out position);
        return position;
    }
    public void SetTarget(Vector3 WorldPos,string content)
    {
        text.text = content;
        Canvas canvas = this.GetComponent<Canvas>();
        TextGuider.GetComponent<RectTransform>().anchoredPosition = WorldToCanvasPos(canvas, WorldPos);
        TextGuider.SetActive(true);
        TimeManager.Instance.Pause();
        float time = content.Length / 20f;
        Mathf.Clamp(time, 1.2f, 4f);
        StartCoroutine(Hide(time));
    }

    IEnumerator Hide(float time)
    {
        yield return new WaitForSecondsRealtime(time);
        TextGuider.SetActive(false);
        TimeManager.Instance.Continue();
    }
    private void Update()
    {
    }
}

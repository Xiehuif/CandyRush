using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using System;
public class InputHandler : Singleton<InputHandler>
{
    public GameObject ClickEffect, HoldEffect;
    private GameObject m_ClickEffect, m_HoldEffect;
    public bool CanClick = true;
    Dictionary<GameObject, Action> Listeners;
    List<GameObject> ListenerName = new List<GameObject>();
    Action OnClick;
    private bool IsEffectExit = false, IsHoldExit = false;
    private float m_ExitTime, m_HoldTime, m_HoldExitTime;
    override protected void Awake()
    {
        m_ClickEffect = Instantiate(ClickEffect, Camera.main.transform);
        m_ClickEffect.SetActive(false);
        m_HoldEffect = Instantiate(HoldEffect, Camera.main.transform);
        m_HoldEffect.SetActive(false);
        base.Awake();
        m_ExitTime = 0;
        m_HoldTime = 0;
    }
    void Update()
    {
        if (IsEffectExit)
        {
            if (m_ExitTime > 0) m_ExitTime -= Time.unscaledDeltaTime;
            else
            {
                IsEffectExit = false;
                m_ClickEffect.SetActive(false);
            }
        }
        if (IsHoldExit)
        {
            if (m_HoldExitTime > 0) m_HoldExitTime -= Time.unscaledDeltaTime;
            else
            {
                IsHoldExit = false;
                m_HoldEffect.SetActive(false);
            }

        }
        if (CanClick)
        {
            if (Input.GetMouseButton(0) && IsPointUI(Input.mousePosition))
            {
                m_HoldTime += Time.unscaledDeltaTime;
                if (m_HoldTime > 0.6f)
                {
                    if (!IsHoldExit)
                    {
                        m_HoldEffect.SetActive(true);
                        m_HoldEffect.transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                        m_HoldEffect.transform.position = new Vector3(m_HoldEffect.transform.position.x, m_HoldEffect.transform.position.y, 1);
                        m_HoldExitTime = 0.5f;
                        IsHoldExit = true;
                        m_HoldTime = 0;
                    }
                }
            }
            else m_HoldTime = 0;
            if (Input.GetMouseButtonDown(0) && IsPointUI(Input.mousePosition))//未点击到UI上
            {

                OnClick();
                if (!AudioManager.Instance.IsSoundPlaying)
                    AudioManager.Instance.PlaySoundByName("click", 0.2f);
                if (!IsEffectExit)
                {
                    m_ClickEffect.SetActive(true);
                    m_ClickEffect.transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                    m_ClickEffect.transform.position = new Vector3(m_ClickEffect.transform.position.x, m_ClickEffect.transform.position.y, 1);
                    IsEffectExit = true;
                    m_ExitTime = 0.41f;
                }
            }
        }
    }

    private bool IsPointUI(Vector2 screenPosition)
    {
        PointerEventData eventData = new PointerEventData(UnityEngine.EventSystems.EventSystem.current);
        eventData.position = new Vector2(screenPosition.x, screenPosition.y);
        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventData, results);
        return !(results.Count > 0);
    }

    public void StartListener(GameObject obj, Action action)
    {
        if (!ListenerName.Contains(obj))
            ListenerName.Add(obj);
        else
        {
            Debug.Log("Listener " + obj + "Has Existed!");
            return;
        }
        if (action != null) OnClick += action;
        else Debug.LogError("The Given Method Is NULL");
    }
    public void StopListener(GameObject obj, Action action)
    {
        if (ListenerName.Contains(obj))
        {
            if (action != null) OnClick -= action;
            ListenerName.Remove(obj);
        }
        else
        {
            Debug.LogError(obj + "Don't Exist!");
        }
    }

}

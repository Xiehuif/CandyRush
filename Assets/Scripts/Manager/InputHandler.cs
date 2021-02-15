using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class InputHandler : Singleton<InputHandler>
{
    public bool CanClick = true;
    Dictionary<GameObject,Action> Listeners;
    List<GameObject> ListenerName = new List<GameObject>();
    Action OnClick;
   override protected void Awake()
   {
       base.Awake();
   }
   void Update()
   {
       if(CanClick)
       {
           if(Input.GetMouseButtonDown(0))
            OnClick();
       }
   }
   public void StartListener(GameObject obj,Action action)
   {
       if(!ListenerName.Contains(obj))
            ListenerName.Add(obj);
        else
        {
            Debug.Log("Listener " + obj + "Has Existed!");
            return ;
        }
        if(action != null) OnClick += action;
        else Debug.LogError("The Given Method Is NULL");
   }
   public void StopListener(GameObject obj,Action action)
   {
       if(ListenerName.Contains(obj))
       {
           if(action != null) OnClick -= action;
           ListenerName.Remove(obj);
       }
       else
       {
           Debug.LogError(obj + "Don't Exist!");
       }
   }
   
}

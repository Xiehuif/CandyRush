using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AppearanceManager : Singleton<AppearanceManager>
{
    public int OriAppearance;
    public GameObject Player;
    public Sprite[] appearance;
    public int CurrentAppearance;
    override protected void Awake()
    {
        base.Awake();
        ReturnToOri();
        CurrentAppearance = OriAppearance;
    }
    public int AppearanceCount 
   {
        get
        {
            return appearance.Length;
        }
    }
    public void ReturnToOri()
    {
        Player.GetComponent<SpriteRenderer>().sprite = appearance[OriAppearance];
    }
    public bool ChangeAppearance(int index)
    {
        if(index > AppearanceCount || index < 0)
            return false;
        Player.GetComponent<SpriteRenderer>().sprite = appearance[index];
        CurrentAppearance = index;
        return true;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AppearanceManager : Singleton<AppearanceManager>
{
   Sprite[] appearance;
   public int AppearanceCount 
   {
        get;
        set;
    }
}

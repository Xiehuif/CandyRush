using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DyeItem : MonoBehaviour
{
   private void OnTriggerEnter2D(Collider2D other)
   {
       if(other.CompareTag("Player"))
       {
           AppearanceManager.Instance.ChangeAppearance(3);
           gameObject.SetActive(false);
       }
   }
}

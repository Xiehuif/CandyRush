using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RealPullSugar : MonoBehaviour
{
   public Transform[] itemsToRotate;
   public float PullDuartion;
   private GameObject m_player;
   private bool m_BeginToPull = false;
   private float m_wholetime = 0;
   void Update()
   {
       if(m_BeginToPull)
       {
           if(Input.GetMouseButton(0))
           {
               m_wholetime += Time.unscaledDeltaTime;
               if(m_wholetime < PullDuartion)
               { 
                   foreach(Transform t in itemsToRotate)
                   {
                        t.rotation *= Quaternion.Euler(0, 0, Time.unscaledDeltaTime * 180f);
                   }
                   m_player.transform.rotation *= Quaternion.Euler(0, 0, Time.unscaledDeltaTime * 180f);
               }
               else
               {
                   m_BeginToPull = false;
                   TimeManager.Instance.Continue();
                    AppearanceManager.Instance.ChangeAppearance(4);
                   m_player.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
               }
           }
       }
   }
   private void OnTriggerEnter2D(Collider2D other)
   {
       if (other.CompareTag("Player"))
        {
            TimeManager.Instance.Pause();
            m_player = other.gameObject;
            m_player.transform.position = this.transform.position;
            m_player.transform.rotation = Quaternion.Euler(0,0,0);
            m_player.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Kinematic;
            m_BeginToPull = true;
            Debug.Log("Enter");
        }
       
   }
}

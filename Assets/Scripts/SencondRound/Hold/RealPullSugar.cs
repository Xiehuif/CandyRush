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


    private bool coroutineOpen;
    public Animator pullSugar;

    private bool end;

    public Sprite startFrame;
    public Sprite endFrame;
    private GameObject player;

    public Transform outPosition;
    public SpriteRenderer renderer;
    private void Reset()
    {
        coroutineOpen = false;
        end = false;
        pullSugar.speed = 0;
        gameObject.GetComponent<SpriteRenderer>().sprite = startFrame;
    }
    void Update()
   {
       if(m_BeginToPull)
       {
           
       }
   }

    private void Start()
    {
        coroutineOpen = false;
        end = false;
        gameObject.GetComponent<SpriteRenderer>().sprite = startFrame;
    }
    private void OnTriggerEnter2D(Collider2D other)
   {
       if (other.CompareTag("Player") && !coroutineOpen)
        {
            other.gameObject.SetActive(false);
            player = other.gameObject;
            AudioManager.Instance.PlaySoundByName("complete");
            m_player = other.gameObject;
            m_player.transform.position = this.transform.position;
            m_player.transform.rotation = Quaternion.Euler(0, 0, 0);
            coroutineOpen = true;
            StartCoroutine("StartPulling");
            Debug.Log("Enter");
        }
       
   }

    private IEnumerator StartPulling()
    {
        while (true)
        {
            if (end) yield break;
            if (Input.GetMouseButton(0))
            {
                string name = renderer.sprite.name;
                if (name == "拉糖机关后段 (44)")
                {
                    endCheck();
                }
                pullSugar.speed = 1;
                yield return 0;
            }
            else
            {
                pullSugar.speed = 0;
                yield return 0;
            }
        }  
    }

    void endCheck()
    {
        end = true;
        player.transform.position = outPosition.position;
        player.SetActive(true);
        pullSugar.speed = 0;
    }
}

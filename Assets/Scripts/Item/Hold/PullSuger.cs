using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PullSuger : MonoBehaviour
{
    public float PullDuration;
    public bool BeginToPull = false;
    private GameObject m_player;
    private float  m_wholetime = 0;
    private void Update()
    {
        if(BeginToPull)
        {
            if(Input.GetMouseButton(0))
            {
                if(m_wholetime < PullDuration)
                {
                    transform.rotation *= Quaternion.Euler(0,0,Time.unscaledDeltaTime * 45f);
                    m_player.transform.rotation *= Quaternion.Euler(0,0,Time.unscaledDeltaTime * 45f);
                    m_wholetime += Time.unscaledDeltaTime;
                }
                else
                {
                    Time.timeScale = 1;
                    BeginToPull = false;
                    m_player.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
                }

            }
        }
    }
    private void OnTriggerEnter2D(Collider2D other) 
    {
        if(other.CompareTag("Player"))
        {
            Time.timeScale = 0;
            m_player = other.gameObject; 
            m_player.transform.position = this.transform.position;
            m_player.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Kinematic;
            BeginToPull = true;
        }   
    }
}

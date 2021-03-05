using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PullSuger : MonoBehaviour
{
    public float PullDuration;
    public bool BeginToPull = false,HasEnter = false;
    private GameObject m_player;
    private float m_wholetime = 0;
    private Transform m_PullPerson;
    private float m_circleRatio;
    
    private  Vector3 m_originPos;    
    void Start()
    {
        m_PullPerson = transform.parent.Find("Begin").Find("Forward").transform;
        m_originPos = m_PullPerson.localPosition;
    }
    private void Update()
    {
        if (BeginToPull&&!HasEnter)
        {
            if (Input.GetMouseButton(0))
            {
                if (m_wholetime < PullDuration)
                {
                    transform.rotation *= Quaternion.Euler(0, 0, Time.unscaledDeltaTime * 180f);
                    m_player.transform.rotation *= Quaternion.Euler(0, 0, Time.unscaledDeltaTime * 180f);
                    m_wholetime += Time.unscaledDeltaTime;
                    m_circleRatio = (m_wholetime / PullDuration) * 0.2f;
                    float degrees =(m_circleRatio - Mathf.Floor(m_circleRatio)) *Mathf.PI* Mathf.Rad2Deg;
                    m_PullPerson.localPosition = m_originPos +
                     new Vector3(Mathf.Cos(degrees)*m_PullPerson.transform.localScale.x*0.2f,
                      Mathf.Sin(degrees)*m_PullPerson.transform.localScale.y * 0.2f,0);
                }
                else
                {
                    Time.timeScale = 1;
                    BeginToPull = false;
                    HasEnter = true;
                    m_player.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
                    
                    transform.parent.Find("Pause").gameObject.SetActive(true);
                    transform.parent.Find("Begin").gameObject.SetActive(false);
                }

            }
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player")&&!HasEnter)
        {
            Time.timeScale = 0;
            m_player = other.gameObject;
            m_player.transform.position = this.transform.position;
            m_player.transform.rotation = Quaternion.Euler(0,0,0);
            m_player.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Kinematic;
            BeginToPull = true;
            transform.parent.Find("Pause").gameObject.SetActive(false);
            transform.parent.Find("Begin").gameObject.SetActive(true);
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            BeginToPull = false;
        }
    }
}

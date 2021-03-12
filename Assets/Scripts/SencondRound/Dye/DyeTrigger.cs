using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DyeTrigger : MonoBehaviour
{
    public GameObject Dye;
    private GameObject m_player;
    private bool HasEnter = false;
    private void Update()
    {
        if(HasEnter)
            BeginDye(m_player); 
    }
    private void BeginDye(GameObject obj) 
    {
        HasEnter = false;
        Dye.SetActive(true);
        TimeManager.Instance.ChangeRate(0.2f);
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            HasEnter = true;
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            HasEnter = false;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DyeTrigger : MonoBehaviour
{
    public GameObject DyePrefab;
    private GameObject m_player;
    private bool HasEnter = false;
    private void Update()
    {
        if(HasEnter&&Input.GetMouseButton(0))
            BeginDye(m_player); 
    }
    private void BeginDye(GameObject obj) 
    {
        HasEnter = false;
        GameObject dyePrefab = Instantiate(DyePrefab);
        TimeManager.Instance.Pause();
        DyeCreator creator = dyePrefab.GetComponentInChildren<DyeCreator>();
        dyePrefab.transform.GetComponentInChildren<DyeCreator>().OnEnd =
            () => { TimeManager.Instance.DelayDo(
                ()=> { TimeManager.Instance.Continue(); },1f); };
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

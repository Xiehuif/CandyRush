using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DyeTrigger : MonoBehaviour
{
    public GameObject DyePrefab;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            GameObject dyePrefab = Instantiate(DyePrefab);
            Time.timeScale = 0;
            DyeCreator creator = dyePrefab.GetComponentInChildren<DyeCreator>();
            dyePrefab.transform.GetComponentInChildren<DyeCreator>().OnEnd =
                () => { Time.timeScale = 1; };
        }
    }
}

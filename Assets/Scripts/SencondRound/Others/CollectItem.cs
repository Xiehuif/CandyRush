using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectItem : MonoBehaviour
{
    private void Start()
    {
        Death.Instance.Add(this.gameObject);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            ScoreManager.Instance.AddScore("NormalItem");
            AudioManager.Instance.PlaySoundByName("collect");
            this.gameObject.SetActive(false);
        }
    }
}

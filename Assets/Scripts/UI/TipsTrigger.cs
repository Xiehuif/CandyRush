using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class TipsTrigger : MonoBehaviour
{
    static int count = 20;
    public GameObject TipLoader,TipShower;
    public int TipIndex;
    private UITips ui;
    private void Start()
    {
        ui = TipLoader.GetComponentInChildren<UITips>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            string t = ui.Read(TipIndex);
            if (t != "")
            {
                TipShower.GetComponentInChildren<Text>().text = t;
                TipShower.SetActive(true);
                StartCoroutine("Fade");
            }
        }
    }
    IEnumerator Fade()
    {
        for (int i = 0; i < count; i++)
        {
            if (ui.CurrentIndex != TipIndex) break;
            TipShower.GetComponentInChildren<Text>().color = new Color(0,0,0,(count - i) / count);
            TipShower.GetComponentInChildren<Image>().color = new Color(1, 1, 1, (count - i) / count);
            yield return new WaitForSeconds(0.1f);
        }
        TipShower.SetActive(false);
        yield return null;
    }
}

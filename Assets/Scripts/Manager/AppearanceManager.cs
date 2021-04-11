using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AppearanceManager : Singleton<AppearanceManager>
{
    public int OriAppearance;
    public GameObject Player, box;
    public Sprite[] appearance;
    public int CurrentAppearance;
    private Vector3 m_OriPos;
    override protected void Awake()
    {
        base.Awake();
        m_OriPos = Player.transform.localPosition;
        ChangeAppearance(OriAppearance);
        CurrentAppearance = OriAppearance;
    }
    public int AppearanceCount
    {
        get
        {
            return appearance.Length;
        }
    }
    public void ReturnToOri()
    {
        ChangeAppearance(OriAppearance);
    }
    public bool ChangeAppearance(int index)
    {
        if (index > AppearanceCount || index < 0)
            return false;
        Player.GetComponent<SpriteRenderer>().sprite = appearance[index];

        CurrentAppearance = index;
        if (index == 1)
        {
            Player.transform.localPosition = m_OriPos + new Vector3(0, 0.1f, 0);
            Player.GetComponentInChildren<Animator>().enabled = true;
            box.SetActive(true);
        }
        else if (index >= 4)
        {
            Player.GetComponentInChildren<Animator>().enabled = false;
            Player.transform.localPosition = box.transform.localPosition - new Vector3(0, 0.05f, 0);
            box.SetActive(false);
        }
        else
        {
            Player.GetComponentInChildren<Animator>().enabled = false;
            Player.transform.localPosition = m_OriPos;
            box.SetActive(true);
        }
        return true;
    }
}

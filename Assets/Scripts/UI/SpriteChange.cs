using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SpriteChange : MonoBehaviour
{
    public float BiggerTime { get { return 1f; } }

    [SerializeField]
    List<GameObject> objs;

    [SerializeField]
    GameObject m_car;

    [SerializeField]
    Sprite[] m_rankSprites;

    [SerializeField]
    Image m_rankImage;

    private void Start()
    {
        int index = SceneTranlater.GetCurrentBuildIndex();


        objs[Mathf.Clamp(index-2,0,objs.Count-1)].SetActive(true);

        if (index == (int)SceneIndex.THIRD)
            StartCoroutine(BecomeLarger());  //第三关UI变大

        m_rankImage.sprite = m_rankSprites[ScoreManager.Instance.GetRank()];
    }

    //第三关结束时UI逐渐放大
    IEnumerator BecomeLarger()
    {
        for(float i=0;i<BiggerTime;i+=Time.unscaledDeltaTime)
        {
            this.transform.localScale = new Vector3(i/BiggerTime, i/BiggerTime, 1);
            yield return null;
        }
        this.transform.localScale = Vector3.one;
    }
}

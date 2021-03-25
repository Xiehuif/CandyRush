using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SpriteChange : MonoBehaviour
{
    private string cur;

    [SerializeField]
    List<GameObject> objs;

    [SerializeField]
    GameObject m_car;

    private void Start()
    {
        cur = SceneManager.GetActiveScene().name;
        int index = UIManager.Instance.stagesName.IndexOf(cur);

        if (index == 1)
            m_car.SetActive(false); //第二关没车

        objs[index].SetActive(true);

        if (index == 2)
            StartCoroutine(BecomeLarger());  //第三关UI变大
    }

    //第三关结束时UI逐渐放大
    IEnumerator BecomeLarger()
    {
        for(float i=0;i<1;i+=Time.deltaTime)
        {
            this.transform.localScale = new Vector3(i, i, 1);
            yield return null;
        }
        this.transform.localScale = Vector3.one;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GradeJugde:Singleton<GradeJugde>
{
    [SerializeField]
    private Sprite[] m_rankSprites=new Sprite[3];

    private static Vector3 m_offset = new Vector3(0, 1, 0);
    
    /// <summary>
    /// 生成评级图
    /// </summary>
    /// <param name="rank">0是Good,1是Great,2是Perfect</param>
    /// <param name="smokePos">烟雾位置</param>
    public void GenerateRank(int rank,Vector3 smokePos)
    {
        var prefab = Resources.Load<GameObject>("RankPic");
        var go = GameObject.Instantiate(prefab, smokePos + m_offset,Quaternion.identity);
        go.GetComponent<SpriteRenderer>().sprite = m_rankSprites[rank];
        TimeManager.Instance.DelayDo((() => GameObject.Destroy(go)),2f);
    }

    /// <summary>
    /// 缺省烟雾位置，用角色位置代替
    /// </summary>
    /// <param name="rank"></param>
    public void GenerateRank(int rank)
    {
        var prefab = Resources.Load<GameObject>("RankPic");
        var go = GameObject.Instantiate(prefab, Death.Instance.transform.position + m_offset, Quaternion.identity);
        go.GetComponent<SpriteRenderer>().sprite = m_rankSprites[rank];
        TimeManager.Instance.DelayDo((() => GameObject.Destroy(go)), 2f);
    }
}

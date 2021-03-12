
using UnityEngine;
using UnityEngine.UI;
public class DyeSlider : MonoBehaviour
{
    public float Speed;
    public float LeftEdge;
    public float left,right;
    private float m_range , m_res;
    public Transform player;
    public Material baseMt;
    private Material mt;
    private void Start() 
    {
        mt = Instantiate(baseMt);
        this.GetComponent<SpriteRenderer>().material = mt;
        right *= transform.localScale.x;
        left *= transform.localScale.x;
        m_range = right - left;
        m_res = 0;
    }
    void Update()
    {
        if(player.localPosition.x >= transform.localPosition.x + left&&
            player.localPosition.x  <= transform.localPosition.x + right)
        {
            if(Mathf.Abs(player.localPosition.y  + 0.5f- this.transform.localPosition.y) < 0.1f)
            {
                m_res += Time.unscaledDeltaTime * Speed;
                mt.SetFloat("_Range", m_res / m_range);
            }
        } 
        if(transform.localPosition.x >=LeftEdge)
        {
            transform.localPosition += new Vector3((-1) * Speed *Time.unscaledDeltaTime,0,0);
        }
        else
        {
            //Debug.Log("Destory Piece!");
            Destroy(gameObject);
        }
    }
}

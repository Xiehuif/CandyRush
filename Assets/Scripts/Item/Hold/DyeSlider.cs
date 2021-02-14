
using UnityEngine;
using UnityEngine.UI;
public class DyeSlider : MonoBehaviour
{
    public float Speed;
    public float LeftEdge;
    public float left,right;
    private float m_range , m_res;
    public Transform player;
    private void Start() 
    {
        m_range = right - left;
        m_res = 0;
    }
    void Update()
    {
        if(player.localPosition.x >= transform.localPosition.x + left&&
            player.localPosition.x <= transform.localPosition.x + right)
        {
            if(Mathf.Abs(player.localPosition.y - this.transform.localPosition.y) < 20)
            {
                m_res += Time.unscaledDeltaTime * Speed;
                GetComponent<Slider>().value =(m_res / m_range);
            }
        } 
        if(transform.localPosition.x >=LeftEdge)
        {
            transform.localPosition += new Vector3((-1) * Speed *Time.unscaledDeltaTime,0,0);
        }
        else
        {
            Debug.Log("Destory Piece!");
            Destroy(gameObject);
        }
    }
}

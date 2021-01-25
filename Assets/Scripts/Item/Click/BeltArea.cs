using UnityEngine;

public class BeltArea : MonoBehaviour
{
    public Vector2 Force;
    private GameObject m_player;
    public void ChangeDirection(Vector2 force)
    {
        Force = force;
        if(m_player != null)
        {
            m_player.GetComponent<PlayerController>().AddForce(Force);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            m_player = collision.gameObject;
            m_player.GetComponent<PlayerController>().AddForce(Force);
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            m_player.GetComponent<PlayerController>().ClearForce();
            m_player = null;
        }
    }
}

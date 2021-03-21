using UnityEngine;
using UnityEngine.UI;
public class ScoreManager : MonoBehaviour
{
    public Text TextUI;
    private int m_score;
    private const int MaxScore = 30000;
    public int Score
    {
        get { return m_score; }
        private set 
        {
            if (value < 0) m_score = 0;
            else if (value > MaxScore) m_score = MaxScore;
            else m_score = value;
            TextUI.text = value.ToString();
        }
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
interface IScoreGiver
{
    float GetScore();
    string GetTag();
}

public class ScoreSliderManager : MonoBehaviour
{
    public float slider;
    [SerializeField] private float WholeScore = 0f;
    [SerializeField] private GameObject star;
    [SerializeField] private GameObject starWithScore;
    private ScoreManager scoreManager;
    private List<string> tags;
    private void Awake()
    {
        tags = new List<string>();
        GameObject[] gameObjects = GameObject.FindGameObjectsWithTag("ScoreGiver");
        foreach (GameObject obj in gameObjects)
        {
            IScoreGiver giver = obj.GetComponent<IScoreGiver>();
            if (giver != null)
            {
                string res = giver.GetTag();
                if (!tags.Contains(res))
                {
                    WholeScore += giver.GetScore();
                    if (res != "") tags.Add(res);
                }
            }
        }
    }
    private void Start()
    {
        scoreManager = ScoreManager.Instance;
    }
    private void Update()
    {
        slider = scoreManager.GetScore() / WholeScore;
        Mathf.Clamp(slider, 0, 1);
        StarAnim();
        this.GetComponent<Slider>().value += (slider - this.GetComponent<Slider>().value) / 20;//缓动
        Mathf.Clamp(this.GetComponent<Slider>().value, 0, slider);
    }

    //星星动画
    private void StarAnim()
    {
        if (slider - this.GetComponent<Slider>().value > 0.005)
        {
            star.SetActive(false);
            starWithScore.SetActive(true);
        }
        else
        {
            star.SetActive(true);
            starWithScore.SetActive(false);
            //TODO
            //旋转,翻转等
        }
    }
}

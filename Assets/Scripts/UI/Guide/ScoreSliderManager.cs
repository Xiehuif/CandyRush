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
    [SerializeField]
    private float WholeScore = 0f;
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
        this.GetComponent<Slider>().value = slider;
    }
}

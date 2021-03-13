using System;
using UnityEngine;

public class DyeCreator : MonoBehaviour
{
    public Action OnEnd;
    public Transform[] PreCreatePoints;
    public Transform PiecesTransform;
    public Transform CreatePoint;
    public Transform Player;
    public float LeftEdge;
    public GameObject DyeSliderPrefab;
    public float CreateInterVal,CreateTimes;
    public float BaseWidth;
    private float time;
    void Start()
    {
        if (Player == null) Debug.LogError("Player Transform IsMising!");

        time = CreateInterVal;
        if (PreCreatePoints.Length == 0) Debug.Log("The Dye Don't Have Any PreCreatPoints!");
        else
        {
            for(int i = 0;i < PreCreatePoints.Length;i++)
            {
                GameObject temp = Instantiate(DyeSliderPrefab, PreCreatePoints[i]);
                temp.transform.parent = PiecesTransform;
                temp.transform.localScale = new Vector3(UnityEngine.Random.Range(0.4f, 1f), temp.transform.localScale.y, 1);
                DyeSlider dyeSlider = temp.GetComponent<DyeSlider>();

                dyeSlider.LeftEdge = LeftEdge + Player.transform.localPosition.x;
                dyeSlider.player = Player;
            }
        }
    }
    void Update()
    {
        if(time >= CreateInterVal)
        {
            CreateTimes--;
            time = 0;
            if(CreateTimes<=-3)
            {
                //Debug.Log("Dye End");
                TimeManager.Instance.DelayDo(
                () =>
                {
                    TimeManager.Instance.Continue();
                    BasePlayer.Instance.GenerateSmoke();
                    AppearanceManager.Instance.ChangeAppearance(3);
                    AudioManager.Instance.PlaySoundByName("complete");
                }, 0.3f);
                this.gameObject.SetActive(false);
            }
            else if(CreateTimes <= 0) return;
            GameObject temp = Instantiate(DyeSliderPrefab,CreatePoint);
            temp.transform.parent = PiecesTransform; 
            temp.transform.localScale = new Vector3(UnityEngine.Random.Range(0.4f, 1f), temp.transform.localScale.y, 1);
            DyeSlider dyeSlider = temp.GetComponent<DyeSlider>();

            dyeSlider.LeftEdge = LeftEdge + Player.transform.localPosition.x;
            dyeSlider.player = Player;
        }
        else  time += Time.unscaledDeltaTime;
    }
}

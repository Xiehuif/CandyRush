using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PackageArea : MonoBehaviour
{
    public enum Quality
    {
        bad,
        great,
        best
    }
    public InPackage final;
    private Quality selfQuality;
    // Start is called before the first frame update
    void Start()
    {
        string name = this.gameObject.name;
        if (name == "Bad") selfQuality = Quality.bad;
        if (name == "Great") selfQuality = Quality.great;
        if (name == "Best") selfQuality = Quality.best;
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (selfQuality == Quality.bad) final.inActive = true;
        if (final.locked || collision.tag != "Player") return;
        else
        {
            Debug.Log(selfQuality);
            final.checkQuality = selfQuality;
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}

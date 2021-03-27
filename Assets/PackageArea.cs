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
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (selfQuality == Quality.bad) final.inActive = true;
        if (final.locked || collision.tag != "Player") return;
        else
        {
            string name = this.gameObject.name;
            if (name == "Bad") selfQuality = Quality.bad;
            if (name == "Great") selfQuality = Quality.great;
            if (name == "Best") selfQuality = Quality.best;
            final.checkQuality = selfQuality;
        }
    }
    void Update()
    {
        
    }
}

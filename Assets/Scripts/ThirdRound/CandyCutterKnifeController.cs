using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CandyCutterKnifeController : MonoBehaviour
{
    public CandyCutterCheck check;
    private Vector3 ori;
    private void Start()
    {
        ori = this.transform.position;
    }
    private void Update()
    {
        if (check.inCutting)
        {
            this.transform.position = ori + check.getDelta();
        }
    }
}

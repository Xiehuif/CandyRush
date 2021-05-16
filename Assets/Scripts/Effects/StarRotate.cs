using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarRotate : MonoBehaviour
{
    private GameObject rotateSpace;
    private GameObject position;
    private Transform oriParent;
    public SurfaceEffector2D speedRelative;
    private float speed = -4 * 30f;//转速
    void Start()
    {
        oriParent = this.transform.parent;


        position = new GameObject();
        position.transform.SetParent(oriParent);
        position.transform.position = this.transform.position;

        rotateSpace = new GameObject();
        this.transform.SetParent(rotateSpace.transform);
    }



    // Update is called once per frame
    void Update()
    {
        if (speedRelative != null)
            speed = -speedRelative.speed * 30;
        else
            speed = -4 * 30f;
        this.transform.RotateAround(this.transform.position, new Vector3(0, 0, 1), speed * Time.deltaTime);
        this.transform.position = position.transform.position;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarRotate : MonoBehaviour
{
    private GameObject rotateSpace;
    private GameObject position;
    private Transform oriParent;
    public SurfaceEffector2D speedRelative
    // Start is called before the first frame update
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
        
        this.transform.RotateAround(this.transform.position, new Vector3(0, 0, 1), speedRelative.speed * 15 * Time.deltaTime;);
        this.transform.position = position.transform.position;
    }
}

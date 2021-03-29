using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Emit : MonoBehaviour
{
    public GameObject emission;
    private List<GameObject> emits;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void emitInit()
    {
        emits = new List<GameObject>();
    }

    public void emitCreate()
    {
        GameObject newEmit = Instantiate(emission,this.transform.position, Quaternion.identity, this.gameObject.transform);
        emits.Add(newEmit);
    }

    public void clearEmits()
    {
        foreach(GameObject i in emits)
        {
            Destroy(i);
        }
    }
}

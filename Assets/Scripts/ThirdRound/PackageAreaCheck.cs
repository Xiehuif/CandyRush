using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PackageAreaCheck : MonoBehaviour
{
    public bool startPackage;
    public bool completePackage;
    public GameObject deathController;
    // Start is called before the first frame update
    void Start()
    {
        
        startPackage = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            startPackage = true;
        }

    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            startPackage = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CandyCutterCheck : MonoBehaviour
{
    private float oriSpeed;
    private float stopStartTime;
    
    public bool onStop;
    public float lastTime;
    public GameObject player;
    public SurfaceEffector2D platform;
    // Start is called before the first frame update
    void Start()
    {
        onStop = false;
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
            Debug.Log("StartCutting");
            oriSpeed = platform.speed;
            stopStartTime = Time.realtimeSinceStartup;
            platform.speed = 0;
            player.GetComponent<Rigidbody2D>().velocity = Vector3.zero;
            onStop = true;
        }
    }
    // Update is called once per frame
    void Update()
    {
        if (onStop)
        {
            if(stopStartTime + lastTime < Time.realtimeSinceStartup)
            {
                restorePhysicsSystem();
                onStop = false;
                return;
            }
        }
    }

    private void restorePhysicsSystem()
    {
        platform.speed = oriSpeed;
    }
}

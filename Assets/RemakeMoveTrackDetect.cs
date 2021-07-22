using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RemakeMoveTrackDetect : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    private LabMoveTrack track;
    void Start()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        track.Enter(collision);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        track.Exit(collision);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}

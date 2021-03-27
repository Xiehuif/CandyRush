﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SteamAudio : MonoBehaviour
{
    private const float centerDistance = 0.5f;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (Mathf.Abs(collision.transform.position.x - transform.position.x )< centerDistance)
                ScoreManager.Instance.AddScore("CenterWind");
            AudioManager.Instance.PlaySoundByName("steam");
        }
    }
}

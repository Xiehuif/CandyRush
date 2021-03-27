using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// 封装过关
/// </summary>
public static class GameInterface
{    
    public static void Succeed()
    {
        AudioManager.Instance.PlaySoundByName("win");
        GameObject.FindWithTag("Player").GetComponentInChildren<Death>().StopMoving();
        UIManager.Instance.Succeed();
        TimeManager.Instance.Pause();
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class tmpSceneSwitching : MonoBehaviour
{
    public string nextScene;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            AudioManager.Instance.PlaySoundByName("win");
            GameObject.FindWithTag("Player").GetComponentInChildren<Death>().StopMoving();
            UIManager.Instance.Succeed();
        }
    }
    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            //SceneManager.LoadScene(nextScene);
        }
    }
}


using UnityEngine.SceneManagement;

public class SceneTranlater
{
    public int CurrentScene;
    public void LoadSceneByCount(int SceneCount)
    {
        SceneManager.LoadScene(SceneCount);
    }
    public void LoadSceneByName(string SceneName)
    {
        SceneManager.LoadScene(SceneName);
    }
   
}

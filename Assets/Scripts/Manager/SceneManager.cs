
using UnityEngine.SceneManagement;

public static class SceneTranlater
{
    public static void LoadSceneByCount(int SceneCount)
    {
        SceneManager.LoadScene(SceneCount);
    }
    public static void LoadSceneByName(string SceneName)
    {
        SceneManager.LoadScene(SceneName);
    }
    public static int GetCurrentBuildIndex()
    {
        return SceneManager.GetActiveScene().buildIndex;
    }
}

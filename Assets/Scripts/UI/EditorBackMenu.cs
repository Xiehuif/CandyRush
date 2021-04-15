using UnityEngine;
using UnityEngine.UI;

public class EditorBackMenu : MonoBehaviour
{
    void Start()
    {
#if UNITY_EDITOR
        var backButtonPrefab = Resources.Load<GameObject>("BackButton");
        var backButton = Instantiate(backButtonPrefab, transform);
        if (backButton == null)
            Debug.LogError("Failed to load BackButton!");
        backButton.transform.SetParent(transform);
        backButton.GetComponent<Button>().onClick.AddListener(() => { SceneTranlater.LoadSceneByCount(0); });
#endif
    }
}

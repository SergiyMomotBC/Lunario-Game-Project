using UnityEngine;
using UnityEngine.SceneManagement;

public class PopUpsManager : MonoBehaviour
{
	public void Awake()
    {
        if (FindObjectsOfType<PopUpsManager>().Length > 1)
            Destroy(gameObject);

        DontDestroyOnLoad(gameObject);

        SceneManager.sceneLoaded += OnSceneLoaded;
	}

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (mode == LoadSceneMode.Single && scene.name != "Level_1")
            Destroy(gameObject);
    }

    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
}

using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenuButtonsManager : MonoBehaviour
{
    public void Awake()
    {
        var canvas = GameObject.Find("UI").GetComponent<Canvas>();
        canvas.worldCamera = Camera.main;
        canvas.sortingLayerName = "Overlay";
        canvas.sortingOrder = 1;
        canvas.planeDistance = 1.0f;
    }

    public void ResumeButtonClicked()
    {
        FindObjectOfType<GameplayManager>().Resume();
        Invoke("Resume", 0.1f);
    }

    public void HowToPlayButtonClicked()
    {
        SceneManager.UnloadSceneAsync(gameObject.scene);
        SceneManager.LoadScene("How To Play", LoadSceneMode.Additive);
    }

    public void ReturnToHomeButtonClicked()
    {
        Time.timeScale = 1.0f;
        Transition.Instance.MakeTransitionTo("Main Menu");
    }
    
    public void QuitButtonClicked()
    {
        Application.Quit();
    }

    private void Resume()
    {
        SceneManager.UnloadSceneAsync(gameObject.scene.name);
    }
}

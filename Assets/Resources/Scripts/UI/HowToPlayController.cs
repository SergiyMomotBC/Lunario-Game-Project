using UnityEngine;
using UnityEngine.SceneManagement;

public class HowToPlayController : MonoBehaviour
{
	public void Awake()
    {
        if (FindObjectOfType<GameplayManager>() != null)
            GameObject.Find("Camera").GetComponent<AudioListener>().enabled = false;
    }
	
	public void Update()
    {
        if (Input.GetButtonDown("Cancel"))
            Close();
	}

    public void Close()
    {
        if (FindObjectOfType<GameplayManager>() != null) {
            SceneManager.UnloadSceneAsync(gameObject.scene);
            SceneManager.LoadScene("Pause Menu", LoadSceneMode.Additive);
        } else
           Transition.Instance.MakeTransitionTo("Main Menu");
    }
}

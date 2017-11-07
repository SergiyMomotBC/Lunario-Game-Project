using UnityEngine;

public class Initialization : MonoBehaviour
{
	public void Awake()
    {
        QualitySettings.vSyncCount = 1;
        Cursor.lockState = CursorLockMode.Locked;
        BackgroundMusicController.Instance.ChangeBackgroundMusic(Resources.Load("Sounds/menu_background_music") as AudioClip);
        DontDestroyOnLoad(GameObject.Find("Settings"));
        var transition = Instantiate(Resources.Load("Prefabs/UI/FadeTransition") as GameObject);
        DontDestroyOnLoad(transition);
	}
}

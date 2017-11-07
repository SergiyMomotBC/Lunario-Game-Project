using UnityEngine;

public class LevelSelectionControls : MonoBehaviour
{
    public void Awake()
    {
        BackgroundMusicController.Instance.ChangeBackgroundMusic(Resources.Load<AudioClip>("Sounds/menu_background_music"));
    }

    public void Update()
    {
        if (Input.GetButtonDown("Cancel"))
            Transition.Instance.MakeTransitionTo("Main Menu");
        else if (Input.GetButtonDown("Tab"))
            Transition.Instance.MakeTransitionTo("Chemical Table");
	}
}
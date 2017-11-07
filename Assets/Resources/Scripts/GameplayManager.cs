using UnityEngine;
using UnityEngine.SceneManagement;

public class GameplayManager : MonoBehaviour 
{
	private bool isGamePaused;

    public int failureCode = 0;

    public static PopUpMessage PopUp { get; private set; }

    public void Awake()
    {
        var sceneName = SceneManager.GetActiveScene().name;
        var levelIndex = int.Parse(sceneName[sceneName.Length - 1].ToString());

        switch (levelIndex)
        {
            case 1:
                BackgroundMusicController.Instance.ChangeBackgroundMusic(Resources.Load("Sounds/level_background_music") as AudioClip);
                break;
            case 2:
                BackgroundMusicController.Instance.ChangeBackgroundMusic(Resources.Load("Sounds/cave-loop") as AudioClip);
                break;
            case 3:
                BackgroundMusicController.Instance.ChangeBackgroundMusic(Resources.Load("Sounds/loop3") as AudioClip);
                break;
        }

        PopUp = FindObjectOfType<PopUpMessage>();

        var levelName = SceneManager.GetActiveScene().name;
        var text = Game.Compounds[int.Parse(levelName[levelName.Length - 1].ToString()) - 1];
        var index = text.IndexOf('\n');
        PopUp.PresentMessage(text.Substring(0, index), text.Substring(index + 1, text.Length - index - 1));
    }

    public void Update()
    {
        if (Input.GetButtonDown("Cancel") && !PopUp.isDisplayingMessage)
            if (!isGamePaused)
            {
                isGamePaused = true;
                Pause();
                SceneManager.LoadScene("Pause Menu", LoadSceneMode.Additive);
            }
            else if(SceneManager.GetSceneByName("Pause Menu").isLoaded)
            {
                isGamePaused = false;
                Resume();
                SceneManager.UnloadSceneAsync("Pause Menu");
            }
	}

	public void Pause()
	{
		Time.timeScale = 0.0f;
        BackgroundMusicController.Instance.Stop();
		isGamePaused = true;
	}

	public void Resume()
	{
		Time.timeScale = 1.0f;
        BackgroundMusicController.Instance.Resume();
        isGamePaused = false;
	}

    public void LevelCompleted()
    {
        Pause();
        enabled = false;
        SceneManager.LoadScene("Level Completed", LoadSceneMode.Additive);
    }
}
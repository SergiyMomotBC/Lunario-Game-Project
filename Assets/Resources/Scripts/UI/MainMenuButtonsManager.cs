using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class MainMenuButtonsManager : MonoBehaviour 
{
    private Button continueButton;

    public void Awake()
    {
        Time.timeScale = 1.0f;
        BackgroundMusicController.Instance.ChangeBackgroundMusic(Resources.Load<AudioClip>("Sounds/menu_background_music"));
        continueButton = GameObject.Find("Continue Button").GetComponent<Button>();
        continueButton.interactable = Game.PlayerStats.wasGameStarted;
    }

    public void NewGameButtonClicked()
    {
        var path = Application.persistentDataPath + "/savegame.sav";
        if (File.Exists(path))
            File.Delete(path);

        Game.ResetPlayerStats();

        Game.PlayerStats.wasGameStarted = true;
        Game.SavePlayerStats();

        Game.nextLevel = 1;
        Transition.Instance.MakeTransitionTo("Loading");      
    }

    public void ContinueButtonClicked()
    {
        Transition.Instance.MakeTransitionTo("Level Selection");
    }

    public void HowToPlayButtonClicked()
    {
        Transition.Instance.MakeTransitionTo("How To Play");
    }

    public void QuitButtonClicked()
    {
        Application.Quit();
    }

    public void SettingsButtonClicked()
    {
        Transition.Instance.MakeTransitionTo("Settings");
    }
}
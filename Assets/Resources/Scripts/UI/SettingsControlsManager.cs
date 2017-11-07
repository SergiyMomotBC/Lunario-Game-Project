using UnityEngine;
using UnityEngine.UI;

public class SettingsControlsManager : MonoBehaviour
{
    private Slider qualitySlider;
    private Toggle fullscreenToggle;
    private Toggle backgroundMusicToggle;
    private Settings settings;

    public void Awake()
    {
        qualitySlider = GameObject.Find("Slider").GetComponent<Slider>();
        fullscreenToggle = GameObject.Find("Fullscreen").transform.FindChild("Toggle").GetComponent<Toggle>();
        backgroundMusicToggle = GameObject.Find("Background Music Control").transform.FindChild("Toggle").GetComponent<Toggle>();
        settings = Settings.Instance;

        qualitySlider.value = settings.graphicsQuality;
        fullscreenToggle.isOn = settings.fullscreen;
        backgroundMusicToggle.isOn = settings.backgroundMusic;
    }

	public void GraphicsQualityLevelChanged()
    {
        settings.graphicsQuality = (int) qualitySlider.value;
    }

    public void FullscreenToggleChanged()
    {
        settings.fullscreen = fullscreenToggle.isOn;
    }

    public void BackgroundMusicChanged()
    {
        settings.backgroundMusic = backgroundMusicToggle.isOn;
    }

    public void SaveButtonPressed()
    {
        settings.Save();
    }

    public void BackButtonPressed()
    {
        Transition.Instance.MakeTransitionTo("Main Menu");
    }
}

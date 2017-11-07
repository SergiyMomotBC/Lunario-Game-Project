using UnityEngine;
using UnityEngine.SceneManagement;

public class BackgroundMusicController : Singleton<BackgroundMusicController>
{
    private BackgroundMusicController() { }

    private AudioSource audioPlayer;

	public void Awake()
    {
        audioPlayer = gameObject.AddComponent<AudioSource>();
        audioPlayer.loop = true;
        SceneManager.sceneLoaded += SceneManager_sceneLoaded;
	}

    private void SceneManager_sceneLoaded(Scene scene, LoadSceneMode mode)
    {
        var index = scene.buildIndex;

        if(index != 6 && index != 7 && index != 9)
            if (Settings.Instance.backgroundMusic && !audioPlayer.enabled)
                audioPlayer.enabled = true;
    }

    public void Stop()
    {
        if (audioPlayer != null)
            audioPlayer.enabled = false;
    }

    public void Resume()
    {
        if(audioPlayer != null)
            audioPlayer.enabled = true;
    }

    public void ChangeBackgroundMusic(AudioClip musicFile)
    {
        if (musicFile != null && musicFile != audioPlayer.clip)
        {
            audioPlayer.clip = musicFile;
            if(audioPlayer.enabled)
                audioPlayer.Play();
        }
    }
}

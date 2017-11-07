using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class LoadingController : MonoBehaviour 
{
    public Text levelName;

    private AsyncOperation loadingOperation;
    private Slider loadingBar;
    private bool loaded;

	public void Start() 
    {
        Time.timeScale = 1.0f;

        levelName.text = "Level " + Game.nextLevel;

        loadingBar = GameObject.Find("Loading Bar").GetComponent<Slider>();
        loadingOperation = SceneManager.LoadSceneAsync("Level_" + Game.nextLevel);
        loadingOperation.allowSceneActivation = false;
	}

    public void Update()
    {
        if(!loaded)
            if (loadingOperation.progress >= 0.9f)
            {
                loaded = true;
                loadingBar.value = 1.0f;
                StartCoroutine("AnyKeyToContinue");
            }
            else
                loadingBar.value = loadingOperation.progress;
    }

    private IEnumerator AnyKeyToContinue()
    {
        var direction = -1.25f;
        var text = GameObject.Find("Message").GetComponent<Text>();
        text.enabled = true;

        while (!Input.anyKey)
        {
            var color = text.color;

            color.a += direction * Time.deltaTime;
            if (color.a <= 0.0f || color.a >= 1.0f)
                direction *= -1;

            text.color = color;

            yield return null;
        }

        loadingOperation.allowSceneActivation = true;
    }
}
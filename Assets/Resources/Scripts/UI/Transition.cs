using UnityEngine;
using UnityEngine.SceneManagement;

public class Transition : Singleton<Transition>
{
    private Transition() { }

    private string nextSceneName;
    private sbyte direction;
    private SpriteRenderer image;
    private const float SPEED = 0.02f;

	public void Awake()
    {
        this.enabled = false;
    }

	public void Update() 
    {
        if (image == null)
            return;

        image.transform.position = new Vector3(
            Camera.main.transform.position.x,
            Camera.main.transform.position.y,
            image.transform.position.z
        );

        image.color = new Color(image.color.r, image.color.g, image.color.b, image.color.a + direction * SPEED);

        if (direction > 0 && image.color.a >= 1.0f) 
        {
            direction = -1;
            SceneManager.LoadScene(nextSceneName);
        } 
        else if (direction < 0 && image.color.a <= 0.0f) 
        {
            Destroy(image.gameObject);
            this.enabled = false;   
        }
	}

    public void MakeTransitionTo(string sceneName)
    {
        nextSceneName = sceneName;
        if(image == null)
            InstantiateFadeImage();
        direction = 1;
        this.enabled = true;
    }

    private void InstantiateFadeImage()
    {
        image = (Instantiate(Resources.Load("Prefabs/Fade Image")) as GameObject).GetComponent<SpriteRenderer>();
        image.color = Color.clear;
        DontDestroyOnLoad(image.gameObject);
    }
}
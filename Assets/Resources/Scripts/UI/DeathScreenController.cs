using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class DeathScreenController : MonoBehaviour
{
    public Text title;

    private Text livesLabel;
    private int direction;
    private bool allowAnimation;
    private float timer;

    public void Awake()
    {
        var canvas = GameObject.Find("UI").GetComponent<Canvas>();
        canvas.worldCamera = Camera.main;
        canvas.renderMode = RenderMode.ScreenSpaceCamera;
        canvas.sortingLayerName = "Overlay";
        canvas.sortingOrder = 1;
        canvas.planeDistance = 1.0f;

        livesLabel = GameObject.Find("Lives Count").GetComponent<Text>();
        livesLabel.text = Game.PlayerStats.lives.ToString();
        Game.PlayerStats.lives--;

        GameObject.Find("Replay Button").GetComponent<Button>().interactable = Game.PlayerStats.lives > 0;

        var code = FindObjectOfType<GameplayManager>().failureCode;
        title.text = code == 1 ? "Lunario Died" : (code == 2 ? "Not Enough Atoms" : "Time Is Up");

        direction = -1;
        allowAnimation = true;
        timer = Time.time;
    }

    public void Update()
    {
        if (!allowAnimation || Time.unscaledTime - timer < 2.0f)
            return;

        var color = livesLabel.color;
        color.a += direction * 3 * Time.unscaledDeltaTime;
        livesLabel.color = color;

        var scale = livesLabel.transform.localScale;
        scale.x += direction * 3 * Time.unscaledDeltaTime;
        scale.y = scale.x;
        livesLabel.transform.localScale = scale;

        if (color.a <= 0.0f)
        {
            direction = 1;
            livesLabel.text = Game.PlayerStats.lives.ToString();
        }

        if (scale.x >= 1.5f)
        {
            livesLabel.transform.localScale = Vector3.one;
            allowAnimation = false;
        }
    }

    public void LevelSelectionButtonPressed()
    {
        Transition.Instance.MakeTransitionTo("Level Selection");
    }

    public void ReplayButtonPressed()
    {
        Transition.Instance.MakeTransitionTo(SceneManager.GetActiveScene().name);
    }

    public void MainMenuButtonPressed()
    {
        Transition.Instance.MakeTransitionTo("Main Menu");
    }

    public void OnDestroy()
    {
        Time.timeScale = 1.0f;
    }
}
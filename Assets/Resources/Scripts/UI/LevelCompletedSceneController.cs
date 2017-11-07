using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Text;

public class LevelCompletedSceneController : MonoBehaviour
{
    public Text redGemsCount;
    public Text blueGemsCount;
    public Text greenGemsCount;
    public Text yellowGemsCount;
    public Text atomName;
    public Image atomIcon;

    public Button nextButton;

    public GameObject[] hits;

    private PlayerCollectedItems player;
    private float timer;

    public void Awake()
    {
        var canvas = GameObject.Find("UI").GetComponent<Canvas>();
        canvas.worldCamera = Camera.main;
        canvas.sortingLayerName = "Overlay";
        canvas.sortingOrder = 1;
        canvas.planeDistance = 1.0f;

        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerCollectedItems>();

        var index = FindObjectOfType<RuneController>().indexOfElementItUnlocks;
        atomName.text = Game.Atoms[index].name;
        atomIcon.sprite = Resources.Load<Sprite>("Sprites/Atoms/atomSprite" + Game.Atoms[index].acronym);

        redGemsCount.text = "+" + (player.redGems - Game.PlayerStats.redGems).ToString();
        blueGemsCount.text = "+" + (player.blueGems - Game.PlayerStats.blueGems).ToString();
        greenGemsCount.text = "+" + (player.greenGems - Game.PlayerStats.greenGems).ToString();
        yellowGemsCount.text = "+" + (player.yellowGems - Game.PlayerStats.yellowGems).ToString();

        if (Game.CurrentLevelIndex == 3)
            nextButton.interactable = false;

        UpdatePlayerStats();
    }

    public void Update()
    {
        timer += Time.unscaledDeltaTime;

        if(timer > 1.0f) {
            timer = 0.0f;
            SpawnHitEffects();
        }
    }

    public void SpawnHitEffects()
    {
        var index = Random.Range(0, hits.Length - 1);
        var x = Camera.main.transform.position.x + Random.value * 16 - 8.0f;
        var y = Camera.main.transform.position.y + Random.value * 8 - 4.0f;
        Instantiate(hits[index], new Vector3(x, y, 0.0f), Quaternion.identity);
    }

    public void MainMenuButtonPressed()
    {
       Transition.Instance.MakeTransitionTo("Main Menu");
    }

    public void LevelSelectionButtonPressed()
    {
        Transition.Instance.MakeTransitionTo("Level Selection");
    }

    public void NextLevelButtonPressed()
    {
        Game.nextLevel = Game.CurrentLevelIndex + 1;
        Transition.Instance.MakeTransitionTo("Loading");
    }

    private void UpdatePlayerStats()
    {
        Game.PlayerStats.redGems = player.redGems;
        Game.PlayerStats.blueGems = player.blueGems;
        Game.PlayerStats.greenGems = player.greenGems;
        Game.PlayerStats.yellowGems = player.yellowGems;
        Game.PlayerStats.coins = player.coins;

        if(Game.PlayerStats.levelsUnlocked == Game.CurrentLevelIndex)
            Game.PlayerStats.levelsUnlocked++;

        var stringStream = new StringBuilder(Game.PlayerStats.elementsUnlockString);
        stringStream[FindObjectOfType<RuneController>().indexOfElementItUnlocks] = '1';
        Game.PlayerStats.elementsUnlockString = stringStream.ToString();

        Game.SavePlayerStats();
    }
}

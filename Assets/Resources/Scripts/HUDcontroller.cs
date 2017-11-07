using UnityEngine;
using UnityEngine.UI;
using System;

public class HUDcontroller : MonoBehaviour 
{
    private PlayerLifeController lifeController;
    private PlayerCollectedItems collectibles;
    private PlayerAtomCollectionController atoms;

    private Text livesLeftLabel;
    private Text redGemsLabel;
    private Text blueGemsLabel;
    private Text yellowGemsLabel;
    private Text greenGemsLabel;
    private Text coinsLabel;
    private Text clockLabel;
    private Text atomsLabel;

    private Image[] hearts;
    private Image redKeyIcon;
    private Image blueKeyIcon;
    private Image yellowKeyIcon;
    private Image greenKeyIcon;
	
    public Sprite blueKeyEmpty;
    public Sprite blueKeyCollected;
    public Sprite redKeyEmpty;
    public Sprite redKeyCollected;
    public Sprite greenKeyEmpty;
    public Sprite greenKeyCollected;
    public Sprite yellowKeyEmpty;
    public Sprite yellowKeyCollected;
    public Sprite heartEmpty;
    public Sprite heartFull;

    private const int LEVEL_TIME_LIMIT = 180;
    private int timeForLevel;
    private int neededAtomsCount;

	public void Awake() 
    {
        var player = GameObject.Find("Player");
        lifeController = player.GetComponent<PlayerLifeController>();
        collectibles = player.GetComponent<PlayerCollectedItems>();
        atoms = player.GetComponent<PlayerAtomCollectionController>();

        neededAtomsCount = FindObjectOfType<RuneController>().totalAtomsNeeded;

        livesLeftLabel = GameObject.Find("Lives Remaining Label").GetComponent<Text>();
        redGemsLabel = GameObject.Find("Red Gems Label").GetComponent<Text>();
        blueGemsLabel = GameObject.Find("Blue Gems Label").GetComponent<Text>();
        yellowGemsLabel = GameObject.Find("Yellow Gems Label").GetComponent<Text>();
        greenGemsLabel = GameObject.Find("Green Gems Label").GetComponent<Text>();
        coinsLabel = GameObject.Find("Coins Label").GetComponent<Text>();
        clockLabel = GameObject.Find("Clock Label").GetComponent<Text>();
        atomsLabel = GameObject.Find("Atoms Label").GetComponent<Text>();

        hearts = new Image[3];
        for(int i = 0; i < 3; i++)
            hearts[i] = GameObject.Find("Heart Bar " + Convert.ToString(i+1)).GetComponent<Image>();

        redKeyIcon = GameObject.Find("Red Key Icon").GetComponent<Image>();
        yellowKeyIcon = GameObject.Find("Yellow Key Icon").GetComponent<Image>();
        blueKeyIcon = GameObject.Find("Blue Key Icon").GetComponent<Image>();
        greenKeyIcon = GameObject.Find("Green Key Icon").GetComponent<Image>();

        timeForLevel = LEVEL_TIME_LIMIT;

        UpdateHUD();
    }
	
	public void Update() 
    {
        if (Time.timeScale == 0.0f)
            return;

        UpdateHUD();
	}

    public void AddTime(int addSeconds)
    {
        timeForLevel += addSeconds;
    }

    private void UpdateHUD()
    {
        livesLeftLabel.text = Convert.ToString(lifeController.lives);
        redGemsLabel.text = Convert.ToString(collectibles.redGems);
        yellowGemsLabel.text = Convert.ToString(collectibles.yellowGems);
        blueGemsLabel.text = Convert.ToString(collectibles.blueGems);
        greenGemsLabel.text = Convert.ToString(collectibles.greenGems);
        coinsLabel.text = Convert.ToString(collectibles.coins);
        atomsLabel.text = atoms.collectedAtomsCount + "/" + neededAtomsCount;

        var seconds = timeForLevel - Mathf.RoundToInt(Time.timeSinceLevelLoad);
        clockLabel.text = Convert.ToString(seconds / 60) + ":" + (seconds % 60 < 10 ? "0" : "") + Convert.ToString(seconds % 60);

        if (seconds == 0)
            GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerLifeController>().OnDeath(3);

        greenKeyIcon.sprite = collectibles.greenKey ? greenKeyCollected : greenKeyEmpty;
        redKeyIcon.sprite = collectibles.redKey ? redKeyCollected : redKeyEmpty;
        blueKeyIcon.sprite = collectibles.blueKey ? blueKeyCollected : blueKeyEmpty;
        yellowKeyIcon.sprite = collectibles.yellowKey ? yellowKeyCollected : yellowKeyEmpty;

        var hp = lifeController.healthPoints;
        for (int i = 0; i < 3; i++)
        {
            hearts[i].sprite = hp > 0 ? heartFull : heartEmpty;
            hp--;
        }
    }
}

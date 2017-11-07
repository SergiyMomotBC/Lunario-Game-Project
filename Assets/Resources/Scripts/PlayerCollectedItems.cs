using UnityEngine;

public class PlayerCollectedItems : MonoBehaviour 
{
    public int coins { get; private set; }
    public int redGems { get; private set; }
    public int blueGems { get; private set; }
    public int greenGems { get; private set; }
    public int yellowGems { get; private set; }
    public bool redKey { get; private set; }
    public bool blueKey { get; private set; }
    public bool greenKey { get; private set; }
    public bool yellowKey { get; private set; }

    public void Awake()
    {
        yellowKey = false;
        redKey = false;
        blueKey = false;
        greenKey = false;

        coins = Game.PlayerStats.coins;
        redGems = Game.PlayerStats.redGems;
        blueGems = Game.PlayerStats.blueGems;
        greenGems = Game.PlayerStats.greenGems;
        yellowGems = Game.PlayerStats.yellowGems;
    }

    public void ItemCollected(CollectableItems item)
    {
        switch (item)
        {
            case CollectableItems.Coin:
                coins++;
                if (coins == 100) {
                    GetComponent<PlayerLifeController>().AddLife();
                    coins = 0;
                }
                break;
            case CollectableItems.RedGem:
                redGems++;
                break;
            case CollectableItems.BlueGem:
                blueGems++;
                break;
            case CollectableItems.GreenGem:
                greenGems++;
                break; 
            case CollectableItems.YellowGem:
                yellowGems++;
                break;
            case CollectableItems.RedKey:
                redKey = true;
                break;
            case CollectableItems.BlueKey:
                blueKey = true;
                break;
            case CollectableItems.GreenKey:
                greenKey = true;
                break; 
            case CollectableItems.YellowKey:
                yellowKey = true;
                break;
        }
    }
}

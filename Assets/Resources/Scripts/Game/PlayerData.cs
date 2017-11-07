using System.Text;

[System.Serializable]
public class PlayerData
{
    public PlayerData()
    {
        lives = 10;
        levelsUnlocked = 1;
        elementsUnlockString = new string('0', 119);

        var stringStream = new StringBuilder(elementsUnlockString);
        stringStream[1] = '1';
        stringStream[8] = '1';
        elementsUnlockString = stringStream.ToString();
    }

    public int lives;
    public int coins;
    public int redGems;
    public int blueGems;
    public int greenGems;
    public int yellowGems;
    public int levelsUnlocked;
    public string elementsUnlockString;
    public bool wasGameStarted;
}
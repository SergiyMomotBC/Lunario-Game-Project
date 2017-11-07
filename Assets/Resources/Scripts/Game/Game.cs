using UnityEngine;
using System.IO;
using UnityEngine.SceneManagement;
using System.Runtime.Serialization.Formatters.Binary;

public static class Game 
{
    private static PlayerData playerStats;
    private static AtomsList atomsList;
    private static CompoundsList compoundsList;

    public static PlayerData PlayerStats
    {
        get { return playerStats; }
        private set { playerStats = value; }
    }

    public static AtomsList Atoms
    {
        get { return atomsList; }
        private set { atomsList = value; }
    }

    public static CompoundsList Compounds
    {
        get { return compoundsList; }
        private set { compoundsList = value; }
    }

    public static int CurrentLevelIndex { get; private set; }

    public static int nextLevel = 0;

    static Game()
    {
        Initialize();
    }

    public static void Initialize()
    {
        if (!Load("savegame.sav", ref playerStats))
            PlayerStats = new PlayerData();

        atomsList = new AtomsList(Application.dataPath + "/elements_data.txt");
        compoundsList = new CompoundsList(Application.dataPath + "/compounds_data.txt");

        SceneManager.sceneLoaded += SceneLoadedResponder;
    }

    private static void SceneLoadedResponder(Scene newScene, LoadSceneMode mode)
    {
        if (mode != LoadSceneMode.Additive)
            Camera.main.gameObject.AddComponent<ScaleToResolution>();

        if(newScene.name.StartsWith("Level_"))
        {
            var index = newScene.name.IndexOf('_');
            CurrentLevelIndex = int.Parse(newScene.name.Substring(index + 1, newScene.name.Length - index - 1));
        }
    }

    public static bool SavePlayerStats()
    {
        return Save("savegame.sav", playerStats);
    }

    public static bool LoadPlayerStats()
    {
        return Load("savegame.sav", ref playerStats);
    }

    public static void ResetPlayerStats()
    {
        playerStats = new PlayerData();
    }

    public static bool Save<T>(string fileName, T dataObject)
    {
        if (!dataObject.GetType().IsSerializable || dataObject == null)
            return false;

        var binaryFormatter = new BinaryFormatter();

        FileStream file = File.Open(Application.persistentDataPath + '/' + fileName, FileMode.OpenOrCreate, FileAccess.ReadWrite);
        if (file == null)
            return false;

        binaryFormatter.Serialize(file, dataObject);
        file.Close();

        return true;
    }

    public static bool Load<T>(string fileName, ref T dataObject)
    {
        if (!File.Exists(Application.persistentDataPath + '/' + fileName))
            return false;

        var binaryFormatter = new BinaryFormatter();

        var file = File.Open(Application.persistentDataPath + '/' + fileName, FileMode.Open);
        if (file == null)
            return false;

        dataObject = (T) binaryFormatter.Deserialize(file);

        file.Close();

        return dataObject != null;
    }
}

using UnityEngine;
using System.IO;
using System;

class Settings : Singleton<Settings>
{
    private Settings() { }

    private int m_Volume;
    private Resolution m_Resolution;
    private int m_GraphicsQualityLevel;
    private bool m_AllowBackgroundMusic;
    private bool m_AllowSoundEffects;
    private bool m_RunInFullscreen;
    private string saveFileName;
    private string folderPath;

    public int volume
    {
        get { return m_Volume; } 
        set
        {
            if(value >= 0 && value <= 10)
            {
                m_Volume = value;
                AudioListener.volume = m_Volume / 10.0f;
            }
        }
    }

    public Resolution resolution
    {
        get { return m_Resolution; }
        set
        {
            m_Resolution = value;
            Screen.SetResolution(m_Resolution.width, m_Resolution.height, m_RunInFullscreen);
        }
    }

    public int graphicsQuality
    {
        get { return m_GraphicsQualityLevel; }
        set
        {
            if(value >= 1 && value <= 3)
            {
                m_GraphicsQualityLevel = value;
                QualitySettings.SetQualityLevel(value == 1 ? 0 : (value == 2 ? 2 : 5));
            }
        }
    }

    public bool backgroundMusic
    {
        get { return m_AllowBackgroundMusic; }
        set
        {
            m_AllowBackgroundMusic = value;
            if (m_AllowBackgroundMusic)
                BackgroundMusicController.Instance.Resume();
            else
                BackgroundMusicController.Instance.Stop();
        }
    }

    public bool soundEffects
    {
        get { return m_AllowSoundEffects; }
        set
        {
            m_AllowSoundEffects = value;
            //do something
        }
    }

    public bool fullscreen
    {
        get { return m_RunInFullscreen; }
        set
        {
            m_RunInFullscreen = value;
            Screen.fullScreen = m_RunInFullscreen;
        }
    }

    public void Awake()
    {
        folderPath = Application.persistentDataPath + Path.DirectorySeparatorChar;
        LoadUserSettings("settings.usf");
    }

    public bool LoadUserSettings(string settingsFileName)
    {
        if(!File.Exists(folderPath + settingsFileName))
        {
            LoadDefaults();
            saveFileName = "settings.usf";
            return false;
        }

        saveFileName = settingsFileName;

        using (var reader = new StreamReader(folderPath + saveFileName))
        {
            string line;
            while((line = reader.ReadLine()) != null)
            {
                if (line == string.Empty)
                    continue;

                var index = line.IndexOf(' ');
                var fieldName = line.Substring(0, index);
                var fieldValue = line.Substring(index + 1, line.Length - index - 1);

                switch(fieldName)
                {
                    case "m_Volume":
                        volume = Convert.ToInt32(fieldValue);
                        break;
                    case "m_Resolution":
                        Resolution res = new Resolution();
                        var xIndex = fieldValue.IndexOf('x');
                        res.width = Convert.ToInt32(fieldValue.Substring(0, xIndex));
                        res.height = Convert.ToInt32(fieldValue.Substring(xIndex + 1, fieldValue.Length - xIndex - 1));
                        resolution = res;
                        break;
                    case "m_GraphicsQualityLevel":
                        graphicsQuality = Convert.ToInt32(fieldValue);
                        break;
                    case "m_AllowBackgroundMusic":
                        backgroundMusic = Convert.ToBoolean(fieldValue);
                        break;
                    case "m_AllowSoundEffects":
                        soundEffects = Convert.ToBoolean(fieldValue);
                        break;
                    case "m_RunInFullscreen":
                        fullscreen = Convert.ToBoolean(fieldValue);
                        break;
                }
            }
        }

        return true;
    }

    public void LoadDefaults()
    {
        volume = 10;
        resolution = Screen.resolutions[Screen.resolutions.Length - 1];
        graphicsQuality = 3;
        backgroundMusic = true;
        soundEffects = true;
        fullscreen = false;
    }

    public bool Save()
    {
        using (var writer = new StreamWriter(folderPath + saveFileName))
        {
            writer.WriteLine("m_Volume " + m_Volume);
            writer.WriteLine("m_Resolution " + m_Resolution.width + "x" + m_Resolution.height);
            writer.WriteLine("m_GraphicsQualityLevel " + m_GraphicsQualityLevel);
            writer.WriteLine("m_AllowBackgroundMusic " + m_AllowBackgroundMusic);
            writer.WriteLine("m_AllowSoundEffects " + m_AllowSoundEffects);
            writer.WriteLine("m_RunInFullscreen " + m_RunInFullscreen);
        }

        return true;
    }
}
 

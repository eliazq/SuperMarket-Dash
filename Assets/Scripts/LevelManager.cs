using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    [Header("Main Levels")]
    public Level[] levels;

    private const string mainMenuSceneName = "MainMenu";

    public static LevelManager Instance;

    void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(this.gameObject);

        DontDestroyOnLoad(this.gameObject);
    }

    private void Start()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {

    }

    /// <summary>
    /// Tällä haetaan tieto kentästä, onko se olemassa ja palautetaan Scene tiedosto 
    /// </summary>
    /// <param name="levelName"></param>
    /// <returns></returns>
    public SceneReference GetSceneDetails(string levelName)
    {
        foreach (Level level in levels)
        {
            if (level.levelName.Equals(levelName))
            {
                return level.scene;
            }
        }

        return null;
    }

    /// <summary>
    /// LevelManager.Instance.ChangeLevel("Level Name")
    /// Loads Scene If Scene Exist With That Name
    /// </summary>
    /// <param name="newLevelName"></param>
    public void ChangeLevel(string newLevelName)
    {
        if (GetSceneDetails(newLevelName) != null)
        {
            Debug.Log("LEVEL MANAGER - Loading Level: " + newLevelName);
            SceneManager.LoadScene(GetSceneDetails(newLevelName));
        }
        else
        {
            Debug.LogWarning("LEVEL MANAGER - Could not load level: " + newLevelName + ". Level does not exist in list");
        }
    }
    /// <summary>
    /// Loads Next Level If In Build Index Correctly
    /// </summary>
    public void LoadNextLevel()
    {
        // next scene buildindex is less or equal to last buildindex
        if (SceneManager.GetActiveScene().buildIndex + 1 <= SceneManager.sceneCountInBuildSettings)
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        else Debug.LogError("Does not exist in build index");
    }
}

[System.Serializable]
public class Level
{
    public string levelName;
    public SceneReference scene;
}
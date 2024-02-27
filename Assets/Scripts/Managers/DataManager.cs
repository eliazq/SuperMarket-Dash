using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataManager : MonoBehaviour
{
    public static DataManager Instance;

    void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(this.gameObject);

        DontDestroyOnLoad(this.gameObject);
    }

    public void SaveLevelProgress(Level level, float progress)
    {
        PlayerPrefs.SetFloat(level.levelName, progress);
    }

    public float GetLevelProgress(Level level)
    {
        if (level != null)
        {
            if (PlayerPrefs.HasKey(level.levelName))
            {
                return PlayerPrefs.GetFloat(level.levelName);
            }
            else return 0;
        }
        else {
            Debug.LogError("Given level is null");
            return 0;
        }
    }


}

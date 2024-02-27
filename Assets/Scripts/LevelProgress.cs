using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelProgress : MonoBehaviour
{
    [SerializeField] private Transform startPos;
    [SerializeField] private Goal goal;
    [SerializeField] private Transform player;

    private float levelLength;
    private float currentProgressPercent;

    private void Start()
    {
        levelLength = Vector3.Distance(startPos.position, goal.transform.position);
        PauseCanvas.Instance.OnPauseCanvasShown += PauseCanvasShown_Action;
    }

    private void PauseCanvasShown_Action(object sender, EventArgs e)
    {
        if (currentProgressPercent > DataManager.Instance.GetLevelProgress(LevelManager.Instance.CurrentLevel))
        {
            DataManager.Instance.SaveLevelProgress(LevelManager.Instance.CurrentLevel, currentProgressPercent);
        }
        PauseCanvas.Instance.progressBar.fillAmount = DataManager.Instance.GetLevelProgress(LevelManager.Instance.CurrentLevel) / 100f;
    }

    private void Update()
    {
        if (currentProgressPercent < 100f)
        {
        float distanceCovered = Vector3.Distance(startPos.position, player.position);
        currentProgressPercent = (distanceCovered / levelLength) * 100f;
        }
        if (goal.hasWon)
        {
            currentProgressPercent = 100f;
        }
    }
}

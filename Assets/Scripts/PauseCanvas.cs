using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseCanvas : MonoBehaviour
{
    public static PauseCanvas Instance;
    [SerializeField] private GameObject canvasVisuals;
    public Image progressBar;

    public event EventHandler OnPauseCanvasShown;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else if (this != Instance)
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);
    }
    public void Show()
    {
        OnPauseCanvasShown?.Invoke(this, EventArgs.Empty);
        canvasVisuals.SetActive(true);
    }

    public void Hide()
    {
        canvasVisuals.SetActive(false);
    }
}

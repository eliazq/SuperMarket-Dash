using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseCanvas : MonoBehaviour
{
    public static PauseCanvas Instance;
    [SerializeField] private GameObject canvasVisuals;

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
        canvasVisuals.SetActive(true);
    }

    public void Hide()
    {
        canvasVisuals.SetActive(false);
    }
}

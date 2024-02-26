using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goal : MonoBehaviour
{

    public bool hasWon {get; private set;}
    public void WonLevel()
    {
        hasWon = true;

        // Open Up Winning UI, Where You Can Navigate To The Main Menu Or Restart Level
        PauseCanvas.Instance.Show();

    }
}

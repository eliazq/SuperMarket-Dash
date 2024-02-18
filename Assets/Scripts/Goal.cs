using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goal : MonoBehaviour
{
    
    public void WonLevel()
    {
        // TODO: Open Up Winning UI, Where You Can Navigate To The Main Menu Or Restart Level
        PauseCanvas.Instance.Show();
    }
}

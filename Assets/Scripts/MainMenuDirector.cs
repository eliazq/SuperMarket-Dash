using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class MainMenuDirector : MonoBehaviour
{
    PlayableDirector director;
    //time to go back in playabledirector
    private float rewindDuration = 1f;

    private void Start() {
        director = GetComponent<PlayableDirector>();
    }

    public void MoveToLevels()
    {
        director.Play();
    }

    // Method to rewind the director to the start gradually
    public void RewindGradually()
    {
        // Start gradually rewinding
        StartCoroutine(GradualRewind());
    }

    public void LoadLevel(string level)
    {
        LevelManager.Instance.ChangeLevel(level);
    }

    IEnumerator GradualRewind()
    {
        float startTime = (float)director.time;
        float endTime = 0f;
        float elapsedTime = 0f;

        while (elapsedTime < rewindDuration)
        {
            elapsedTime += Time.deltaTime;
            float t = elapsedTime / rewindDuration;
            director.time = Mathf.Lerp(startTime, endTime, t);
            yield return null;
        }

        // Ensure the time is set to the start
        director.time = 0f;
        director.Stop();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video; // Required for VideoPlayer

public class ConditionalVideo : MonoBehaviour
{
    [Header("-- System Data --")]
    [SerializeField] SaveItem eventSave;
    public GameEvent _event;

    [Header("-- Video Player --")]
    public VideoPlayer videoPlayer;

    void Awake()
    {
        Time.timeScale = 0f;

        if (videoPlayer != null)
        {
            videoPlayer.Play();

            videoPlayer.loopPointReached += OnVideoEnd;
        }

        _event.hasaved = true;
        eventSave.Save();
    }

    void OnVideoEnd(VideoPlayer vp)
    {
        Time.timeScale = 1f;

        videoPlayer.loopPointReached -= OnVideoEnd;
    }
}

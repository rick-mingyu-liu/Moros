using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.SceneManagement;

public class VideoManager : MonoBehaviour
{
    public static VideoManager instance;
    public GameObject video; // Displayer or hide
    public GameObject video1; // Displayer or hide
    public float savePosition;
    public Transform Tranform1;
    public Transform Tranform2;
    public string currentSceneName;

    private bool played_Bad = false;
    private bool played_Start = false;
    private bool played_Good = false;
    
    public GameObject player;


    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            if (instance != this)
            {
                Destroy(gameObject);
            }
        }
    }
    private void Start()
    {
        played_Bad = false;
        player = GameObject.Find("Character_Moros");
        currentSceneName = SceneManager.GetActiveScene().name;
    }

    private void Update()
    {
        if (played_Bad && !video.GetComponent<VideoPlayer>().isPlaying)
        {
            Invoke("AudioCallBack_Bad", 0.5f);
        }

        if (played_Start && !video.GetComponent<VideoPlayer>().isPlaying)
        {
            Invoke("AudioCallBack_Start", 0.5f);
        }
        if (video1)
        {
            if (played_Good && !video1.GetComponent<VideoPlayer>().isPlaying)
            {
                Invoke("AudioCallBack_Good", 0.5f);
            }
        }
    }

    public void ShowVideo_BadEnd(bool isPlayerDead)
    {
        video.SetActive(isPlayerDead);
        played_Bad = true;
        video.GetComponent<VideoPlayer>().Play();
        BanPlayerMovement();

    }

    public void ShowVideo_StartGame(bool isGameStart)
    {
        video.SetActive(isGameStart);
        played_Start = true;
        video.GetComponent<VideoPlayer>().Play();
        BanPlayerMovement();
    }

    public void ShowVideo_GoodEnd(bool isWin)
    {
        video1.SetActive(isWin);
        played_Good = true;
        video1.GetComponent<VideoPlayer>().Play();
        BanPlayerMovement();
    }


    public void BanPlayerMovement()
    {
        //player.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        //savePosition = player.GetComponent<Rigidbody2D>().transform.position.x;
    }

    public void ActivationPlayerMovement()
    {
        float deltaPosition1 = Mathf.Abs(savePosition - Tranform1.position.x);
        float deltaPosition2 = Mathf.Abs(savePosition - Tranform2.position.x);
        if(deltaPosition1 > deltaPosition2)
        {
            player.GetComponent<Rigidbody2D>().transform.position = Tranform1.position;
        }
        else
        {
            player.GetComponent<Rigidbody2D>().transform.position = Tranform2.position;
        }
        SceneManager.LoadScene(currentSceneName);

    }

    public void StartGame()
    {
        SceneManager.LoadScene("City");
    }


    public void GoodEnd()
    {
        //SceneManager.LoadScene("Game Start");
        TransitionManager.Instance.TransitionToScene("Game Start",SceneTransitionDestination.DestinationTag.A);
    }
    private void AudioCallBack_Bad()
    {
        if (played_Bad && !video.GetComponent<VideoPlayer>().isPlaying)
        {
            played_Bad = false;
            //video.SetActive(false);
            ActivationPlayerMovement();
        }
    }

    private void AudioCallBack_Start()
    {
        if (played_Start && !video.GetComponent<VideoPlayer>().isPlaying)
        {
            played_Start = false;
            //video.SetActive(false);
            StartGame();
        }
    }

    private void AudioCallBack_Good()
    {
        if (played_Good && !video1.GetComponent<VideoPlayer>().isPlaying)
        {
            played_Good = false;
            //video1.SetActive(false);
            GoodEnd();
        }
    }
}

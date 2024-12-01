using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEditor;
using UnityEngine.Events;

public class SceneSwitcher : MonoBehaviour
{
    [Header("-- System Data --")]
    [SerializeField] SaveItem eventSave;
    public GameEvent _event;


    //public UnityEvent _ConditionalEndHandle;

    public GameObject promptUI;
    [SceneName] public string targetSceneName;
    [Tooltip("The tag of the SceneTransitionDestination script in the scene being transitioned to.")]
    public SceneTransitionDestination.DestinationTag transitionDestinationTag;
    public Item slotItem1;
    public Item slotItem2;
    public GameObject player;
    public GameObject boss;
    private bool isPlayerInRange = false;

    private void Start()
    {
        if (promptUI != null)
        {
            promptUI.SetActive(false);
        }
        player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            if (slotItem1)
            {
                if (!slotItem1.havePickup && SceneManager.GetActiveScene().name == "Forest" && gameObject.name == "Alter")
                {
                    if (gameObject.GetComponent<BoxCollider2D>())
                        gameObject.GetComponent<BoxCollider2D>().enabled = false;
                }
                else
                {
                    if (gameObject.GetComponent<BoxCollider2D>())
                        gameObject.GetComponent<BoxCollider2D>().enabled = true;
                }
            }
        }
    }

    private void Update()
    {
        if (isPlayerInRange && Input.GetKeyDown(KeyCode.E))
        {
            if (slotItem2)
            {
                if (slotItem2.havePickup && SceneManager.GetActiveScene().name == "Forest")
                {
                    if (boss)
                    {
                        boss.SetActive(false);
                        VideoManager.instance.ShowVideo_GoodEnd(true);
                    }
                }
                else
                {
                    TransitionManager.Instance.TransitionToScene(targetSceneName, transitionDestinationTag);
                }
            }
            else
            {
                TransitionManager.Instance.TransitionToScene(targetSceneName, transitionDestinationTag);
            }
        }
    }

    public void TransitionToLevel()
    {
        TransitionManager.Instance.TransitionToScene(targetSceneName, transitionDestinationTag);
    }

    public void TransitionToLevel_Start()
    {
        if (!_event.hasaved)
        {
            _event.hasaved = true;
            eventSave.Save();
            VideoManager.instance.ShowVideo_StartGame(true);
        }
        else
        {
            TransitionManager.Instance.TransitionToScene(targetSceneName, transitionDestinationTag);
        }
    }

    //public void TransitionTo_GoodEnd()
    //{
    //}

    public void GameEnd()
    {
        //EditorApplication.isPlaying = false;
        Application.Quit();
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInRange = true;
            if (promptUI != null)
            {
                promptUI.SetActive(true);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInRange = false;
            if (promptUI != null)
            {
                promptUI.SetActive(false);
            }
        }
    }

    private void OnDestroy()
    {
        player = null;
        boss = null;
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class TransitionManager : MonoBehaviour
{
    public static TransitionManager Instance { get; private set; }

    private CanvasGroup fadeCanvasGroup;
    public float fadeDuration = 1f;

    // 存储每个场景传送点的标签和位置
    private Dictionary<string, SceneTransitionDestination[]> sceneDestinations = new Dictionary<string, SceneTransitionDestination[]>();
    //private Dictionary<string, SceneTransitionDestination> destinations = new Dictionary<string, SceneTransitionDestination>();

    private SceneTransitionDestination.DestinationTag storedDestinationTag;
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);

        fadeCanvasGroup = GetComponentInChildren<CanvasGroup>();
        if (fadeCanvasGroup == null)
        {
            Debug.LogError("FadeCanvasGroup is missing! Ensure a CanvasGroup is attached to the TransitionManager or its children.");
        }
        else
        {
            fadeCanvasGroup.alpha = 0f;
        }

        // 注册场景加载事件
        SceneManager.sceneLoaded += OnSceneLoaded;
    }


    private void OnDestroy()
    {
        // 移除事件监听
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    // Cache the destination when switching scenes
    public void SetDestinationTag(SceneTransitionDestination.DestinationTag destinationTag)
    {
        storedDestinationTag = destinationTag;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // 每次新场景加载后更新传送点数据
        UpdateSceneDestinations(scene.name, storedDestinationTag);
    }

    private void UpdateSceneDestinations(string sceneName, SceneTransitionDestination.DestinationTag destinationTag)
    {
        StartCoroutine(WaitAndPlacePlayer(destinationTag));
    }

    private IEnumerator WaitAndPlacePlayer(SceneTransitionDestination.DestinationTag destinationTag)
    {
        GameObject player = null;

        // 等待直到玩家对象被加载
        while (player == null)
        {
            player = GameObject.FindWithTag("Player");
            yield return null; // 等待下一帧
        }

        // 获取目标传送点
        var destinationPoints = FindObjectsOfType<SceneTransitionDestination>();
        foreach (var point in destinationPoints)
        {
            if (point.destinationTag == destinationTag)
            {
                player.transform.position = point.transform.position;
                yield break;
            }
        }

        Debug.LogWarning("No matching destination point found for tag: " + destinationTag);
    }


    // 切换场景
    public void ChangeScene(string sceneName, SceneTransitionDestination.DestinationTag destinationTag)
    {
        StartCoroutine(FadeAndSwitchScene(sceneName, destinationTag));
    }

    // 淡入淡出效果和场景切换
    private IEnumerator FadeAndSwitchScene(string sceneName, SceneTransitionDestination.DestinationTag destinationTag)
    {
        yield return StartCoroutine(Fade(1));

        // 加载新场景
        SceneManager.LoadScene(sceneName);

        yield return new WaitForEndOfFrame();  // 确保场景完全加载

        // 将角色设置到目标位置
        SetPlayerPosition(sceneName, destinationTag);

        yield return StartCoroutine(Fade(0));
    }

    // 设置玩家的位置
    private void SetPlayerPosition(string sceneName, SceneTransitionDestination.DestinationTag destinationTag)
    {
        if (sceneDestinations.ContainsKey(sceneName))
        {
            SceneTransitionDestination[] destinations = sceneDestinations[sceneName];

            foreach (var destination in destinations)
            {
                if (destination.destinationTag == destinationTag)
                {
                    // 找到目标标签，设置角色位置
                    GameObject player = GameObject.FindGameObjectWithTag("Player");
                    if (player != null)
                    {
                        player.transform.position = destination.transform.position;
                        destination.OnPlayerReach(); // 调用到达事件
                    }
                    break;
                }
            }
        }
    }

    // 控制淡入淡出
    private IEnumerator Fade(float targetAlpha)
    {
        if (fadeCanvasGroup == null) yield break;

        // 启用 CanvasGroup 以确保其可见
        fadeCanvasGroup.gameObject.SetActive(true);

        float startAlpha = fadeCanvasGroup.alpha;
        float time = 0f;

        while (time < fadeDuration)
        {
            fadeCanvasGroup.alpha = Mathf.Lerp(startAlpha, targetAlpha, time / fadeDuration);
            time += Time.deltaTime;
            yield return null;
        }

        fadeCanvasGroup.alpha = targetAlpha;

        // 当完全淡出（Alpha 为 0）时，可以禁用 CanvasGroup
        if (targetAlpha == 0)
        {
            fadeCanvasGroup.gameObject.SetActive(false);
        }
    }


    public void TransitionToScene(string sceneName, SceneTransitionDestination.DestinationTag destinationTag)
    {
        // Store the destination tag before loading the scene
        TransitionManager.Instance.SetDestinationTag(destinationTag);
        StartCoroutine(FadeAndSwitchScene(sceneName, destinationTag));
    }
}

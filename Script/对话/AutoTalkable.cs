using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoTalkable : MonoBehaviour
{
    [Header("-- System Data --")]
    [SerializeField] SaveItem bagsave;


    private bool isEntered;

    public GameInput gameInput;
    [TextArea(1, 3)]
    public string[] lines;
    [SerializeField]
    private bool hasImage;

    public GameEvent _event;
    public GameObject player;

    [Header("Tip")]
    private GameObject talkableTip;

    private void Start()
    {

        if (_event.hasaved && _event.Eventid != 7)
        {
            Destroy(gameObject);
        }
        gameInput = GameObject.Find("GameInput").GetComponent<GameInput>(); // 自动找到场景中的 GameInput 组件
        if (gameInput == null)
        {
            Debug.LogError("GameInput not found in the scene!");
        }
        talkableTip = gameObject.transform.Find("TalkableTip").gameObject;
        talkableTip.SetActive(false);
        player = GameObject.FindGameObjectWithTag("Player");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        talkableTip.SetActive(true);
        if (Input.GetKey(KeyCode.T) && collision.gameObject.CompareTag("Player"))
        {
            if (!gameObject.GetComponent<CircleCollider2D>())
            {
                _event.hasaved = true;
                bagsave.Save();
            }
            gameInput.EnableInput(false); // 禁用输入
            isEntered = true;
            DialogueManager.talking = true;
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (Input.GetKey(KeyCode.T) && collision.gameObject.CompareTag("Player"))
        {
            if (!gameObject.GetComponent<CircleCollider2D>())
            {
                _event.hasaved = true;
                bagsave.Save();
            }
            isEntered = true;
            gameInput.EnableInput(false); // 禁用输入
            DialogueManager.talking = true;
        }
    }


    private void OnTriggerExit2D(Collider2D collision)
    {
        talkableTip.SetActive(false);
        if (collision.gameObject.CompareTag("Player"))
        {
            isEntered = false;
        }
    }

    private void Update()
    {
        if (DialogueManager.talking && isEntered && !DialogueManager.instance.dialogueBox.activeInHierarchy)
        {
            DialogueManager.instance.ShowDialogue(lines, hasImage);
        }
        else if(!DialogueManager.talking)
        {
            gameInput.EnableInput(true); // 禁用输入
        }
        DialogueManager.instance.CheckImage();

    }
}

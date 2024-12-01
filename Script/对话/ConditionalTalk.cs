using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConditionalTalk : MonoBehaviour
{
    [Header("-- Player Data --")]
    [SerializeField] SaveItem gamesave;

    public GameObject player;
    private BoxCollider2D boxCollider;
    public GameEvent _event;

    public Item slotItem1;
    private bool isEntered;

    [TextArea(1, 3)]
    public string[] lines;
    [SerializeField]
    private bool hasImage;
    [Header("Tip")]
    private GameObject talkableTip;
    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        boxCollider = gameObject.GetComponent<BoxCollider2D>();
        if (slotItem1.havePickup)
        {
            boxCollider.isTrigger = true;
            gameObject.GetComponent<CircleCollider2D>().enabled = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (gameObject.GetComponent<CircleCollider2D>().enabled)
        {
            isEntered = true;
            DialogueManager.talking = true;
            _event.hasaved = true;
            gamesave.Save();
        }

    }


    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && gameObject.GetComponent<CircleCollider2D>().enabled)
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
    }
}

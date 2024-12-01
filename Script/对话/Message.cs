using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Message : MonoBehaviour
{
    [TextArea(1, 3)]
    public string[] lines;

    private bool isentered = false;
    [SerializeField]
    private bool hasImage;
    private void Start()
    {
    }

    private void Update()
    {
        if (isentered && DialogueManager.talking && !DialogueManager.instance.dialogueBox.activeInHierarchy)
        {
            Debug.Log("可以说话");
            DialogueManager.instance.ShowDialogue(lines, hasImage);
            isentered = false;

        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            isentered = true;
        }
        if (Input.GetKey(KeyCode.T))
        {
            DialogueManager.talking = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            isentered = false;
        }
    }
}

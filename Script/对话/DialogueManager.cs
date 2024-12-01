using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



public class DialogueManager : MonoBehaviour
{
    [Header("-- System Data --")]
    [SerializeField] SaveItem eventSave;

    public static DialogueManager instance;
    public static bool talking;
    public GameObject dialogueBox; // Displayer or hide
    public Text dialogueText; 
    public Image characterImage;
    public Image mayorImage;
    public Image NPC1Image;
    public Image NPC2Image;
    public Image SisterImage;


    [TextArea(1, 3)]
    public string[] dialogueLines;
    private int currentLine;
    public bool isScrolling;
    [SerializeField]
    private float textSpeed;

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
        talking = false;
    }
    private void Start()
    {
        dialogueText.text = dialogueLines[currentLine];
    }

    private void Update()
    {
        if (dialogueBox.activeInHierarchy)
        {
            if (Input.GetKey(KeyCode.T) && dialogueText.text == dialogueLines[currentLine])
            {
                if (isScrolling == false)
                {
                    currentLine++;
                    if (currentLine < dialogueLines.Length)
                    {
                        talking = true;
                        CheckImage();
                        StartCoroutine(ScrollingText());
                    }
                    else
                    {
                        dialogueBox.SetActive(false); //Box Hiden
                        talking = false;
                    }
                }
            }
        }
    }

    public void ShowDialogue(string[] newLines, bool hasImage)
    {
        Debug.Log(newLines[0].ToString());
        
        talking = true;

        dialogueLines = newLines;
        currentLine = 0;
        Debug.Log(dialogueLines[0].ToString());
        CheckImage();
        
        StartCoroutine(ScrollingText());
        dialogueBox.SetActive(true);
        characterImage.gameObject.SetActive(hasImage);
    }

    public void CheckImage()
    {
        // 检查是否有对话内容
        if (currentLine >= dialogueLines.Length || string.IsNullOrEmpty(dialogueLines[currentLine]))
        {
            characterImage.enabled = false;
            mayorImage.enabled = false;
            NPC1Image.enabled = false;
            NPC2Image.enabled = false;
            SisterImage.enabled = false;

            // 初始化对话内容
            dialogueLines = new string[0];
            dialogueText.text = null;
            currentLine = 0;

            return;
        }

        if (dialogueLines[currentLine].StartsWith("Player"))
        {
            characterImage.enabled = true;
            mayorImage.enabled = false;
            NPC1Image.enabled = false;
            NPC2Image.enabled = false;
            SisterImage.enabled = false;
            currentLine++;
        }
        else if(dialogueLines[currentLine].StartsWith("Mayor"))
        {
            mayorImage.enabled = true;
            characterImage.enabled = false;
            NPC1Image.enabled = false;
            NPC2Image.enabled = false;
            SisterImage.enabled = false;
            currentLine++;
        }
        else if (dialogueLines[currentLine].StartsWith("NPC1"))
        {
            NPC1Image.enabled = true;
            NPC2Image.enabled = false;
            SisterImage.enabled = false;
            mayorImage.enabled = false;
            characterImage.enabled = false;
            currentLine++;
        }
        else if (dialogueLines[currentLine].StartsWith("NPC2"))
        {
            NPC2Image.enabled = true;
            characterImage.enabled = false;
            mayorImage.enabled = false;
            NPC1Image.enabled = false;
            SisterImage.enabled = false;

            currentLine++;
        }
        else if (dialogueLines[currentLine].StartsWith("Sister"))
        {
            SisterImage.enabled = true;
            NPC2Image.enabled = false;
            characterImage.enabled = false;
            mayorImage.enabled = false;
            NPC1Image.enabled = false;
            currentLine++;
        }
        else if(dialogueLines[currentLine].StartsWith("Narrator"))
        {
            SisterImage.enabled = false;
            NPC2Image.enabled = false;
            characterImage.enabled = false;
            mayorImage.enabled = false;
            NPC1Image.enabled = false;
            currentLine++;
        }
    }
    private IEnumerator ScrollingText()
    {
        isScrolling = true;
        dialogueText.text = "";

        foreach (char letter in dialogueLines[currentLine].ToCharArray())
        {
            dialogueText.text += letter; // letter by letter show
            yield return new WaitForSeconds(textSpeed);
        }
        isScrolling = false;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

// just need to click to proceed to the next line / para
// assign the Text Component and Text Speed to the DialogueBox
// the content need to be plugged in

// public class Dialogue : MonoBehaviour
// {
//     public TextMeshProUGUI textComponent;
//     public string[] lines;
//     public float textSpeed;

//     private int index;

//     void Start() {
//         textComponent.text = string.Empty;
//         StartDialogue();
//     }

//     void Update() {
//         if (Input.GetMouseButtonDown(0)) {
//             if (textComponent.text == lines[index]) {
//                 NextLine();
//             } else {
//                 StopAllCoroutines();
//                 textComponent.text = lines[index];
//             }
//         }
//     }

//     void StartDialogue() {
//         index = 0;
//         StartCoroutine(TypeLine());
//     }

//     IEnumerator TypeLine() {
//         foreach (char c in lines[index].ToCharArray()) {
//             textComponent.text += c;
//             yield return new WaitForSeconds(textSpeed);
//         }
//     }

//     void NextLine() {
//         if (index < lines.Length - 1) {
//             index++;
//             textComponent.text = string.Empty;
//             StartCoroutine(TypeLine());
//         } else {
//             gameObject.SetActive(false); // End of dialogue
//         }
//     }

// }

public class Dialogue : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI textComponent;  // Text component to display dialogue
    [SerializeField] private string[] lines;                 // Array of dialogue lines
    [SerializeField] private float textSpeed = 0.05f;        // Speed of the typewriter effect

    private int index = 0;                                   // Keeps track of the current line
    private bool isTyping = false;                           // Tracks if the typewriter effect is active

    private void Start() {
        if (textComponent == null || lines == null || lines.Length == 0) {
            Debug.LogError("Dialogue script setup is incomplete. Ensure textComponent and lines array are properly assigned.");
            return;
        }

        textComponent.text = string.Empty;
        StartDialogue();
    }

    private void Update() {
        if (Input.GetMouseButtonDown(0)) {
            if (isTyping) {
                // Complete the current line immediately
                StopAllCoroutines();
                textComponent.text = lines[index];
                isTyping = false;
            } else {
                // Proceed to the next line
                NextLine();
            }
        }
    }

    private void StartDialogue() {
        index = 0;
        DisplayLine();
    }

    private void DisplayLine() {
        if (index >= 0 && index < lines.Length) {
            StartCoroutine(TypeLine());
        } else {
            Debug.LogError("Index out of range during DisplayLine.");
        }
    }

    private IEnumerator TypeLine() {
        isTyping = true;
        textComponent.text = string.Empty;

        foreach (char c in lines[index].ToCharArray()) {
            textComponent.text += c;
            yield return new WaitForSeconds(textSpeed);
        }

        isTyping = false; // Typing completed
    }

    private void NextLine() {
        if (index < lines.Length - 1) {
            index++;
            DisplayLine();
        } else {
            EndDialogue();
        }
    }

    private void EndDialogue() {
        textComponent.text = string.Empty; // Clear the text (optional)
        gameObject.SetActive(false);       // Disable the dialogue object
        Debug.Log("Dialogue ended.");
    }
}

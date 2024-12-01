using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HideHole : MonoBehaviour
{
    private GameObject player;
    private SpriteRenderer background;
    private Sprite newSprite;
    private Sprite origonalSprite;
    public GameObject promptUI;
    private bool isPlayerInRange = false;
    private bool isHidden = false;
    void Start()
    {
        player = GameObject.FindWithTag("Player");
        background = GameObject.FindWithTag("BackGround")?.GetComponent<SpriteRenderer>();
        newSprite = Resources.Load<Sprite>("Art/Art_Scene/UndergroundPassage_withPlayer");
        origonalSprite = Resources.Load<Sprite>("Art/Art_Scene/UndergroundPassage_new");
        if (background == null)
        {
            Debug.LogWarning("background not found");
        }
        if (newSprite == null)
        {
            Debug.LogWarning("newSprite not found");
        }
        if (promptUI != null)
        {
            promptUI.SetActive(false);
        }
    }

    void Update()
    {
        if(isPlayerInRange && Input.GetKeyDown(KeyCode.E))
        {
            if(isHidden)
            {
                if (background != null && newSprite != null)
                {
                    background.sprite = origonalSprite;
                    player.SetActive(true);
                    isHidden = false;
                }
                else
                {
                    Debug.LogWarning("Sprite not found");
                }
            }
            else
            {
                if (background != null && newSprite != null)
                {
                    background.sprite = newSprite;
                    isHidden = true;
                    player.SetActive(false);
                }
                else
                {
                    Debug.LogWarning("Sprite not found");
                }
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isPlayerInRange = true;
            if (promptUI != null)
            {
                promptUI.SetActive(true);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && !isHidden)
        {
            isPlayerInRange = false;
            if (promptUI != null)
            {
                promptUI.SetActive(false);
            }
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RedGem : MonoBehaviour
{   
    public GameObject redGem; // red gem item
    public Text RedGemText; // used for showing "item collected"
    private bool isNearGem = false; // determine if the player is near the gem
    private bool isCollected = false; // determine if the gem has been collected

    // Start is called before the first frame update
    private void start()
    {
        // hide notification text
        RedGemText.gameObject.SetActive(false);
    }

    private void update()
    {
        // determine if the player is near the gem and the gem has not been collected
        if (isNearGem && !isCollected && Input.GetKeyDown(KeyCode.E))
        {
            CollectGem();
        }
    }

    private void CollectGem()
    {
        // put the red gem into backpack
        isCollected = true;
        redGem.SetActive(false); // hide the gem

        // show notification text
        RedGemText.text = "Item Collected";
        RedGemText.gameObject.SetActive(true);

        // delay hiding prompt for 2s
        Invoke("Item Collected", 2f);
    }

    private void HideNotification()
    {
        RedGemText.gameObject.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        // determine if player is near the gem
        if (other.CompareTag("player"))
        {
            isNearGem = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        // determine if player gets away from the gem
        if (other.CompareTag("player"))
        {
            isNearGem = false;
        }
    }
}

using UnityEngine;
using UnityEngine.UI;

public class AncientKey : MonoBehaviour
{
    private bool isPlayerNearby = false; // Tracks if the player is near the key
    [SerializeField] private Text interactionText; // UI element for hints
    [SerializeField] private Inventory playerInventory; // Reference to player's inventory
    [SerializeField] private GameInput gameInput; // Reference to GameInput

    private void Start()
    {
        if (interactionText != null)
        {
            interactionText.gameObject.SetActive(false); // Hide interaction text initially
        }
    }

    private void Update()
    {
        // Check if player is nearby and presses the interact key
        if (isPlayerNearby && gameInput != null && gameInput.IsInteractPressed())
        {
            PickUpKey();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isPlayerNearby = true;

            if (interactionText != null)
            {
                interactionText.gameObject.SetActive(true);
                interactionText.text = "Press E to pick up the key";
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isPlayerNearby = false;

            if (interactionText != null)
            {
                interactionText.gameObject.SetActive(false);
            }
        }
    }

    private void PickUpKey()
    {
        if (playerInventory != null)
        {
            playerInventory.AddItem(gameObject); // Add the key to inventory
        }

        if (interactionText != null)
        {
            interactionText.gameObject.SetActive(false);
        }

        Debug.Log("Ancient Key Collected");
    }
}



// 使用方法
/*
1. 创建钥匙对象
    - 在场景中放置一个钥匙的 2D 精灵，并为其添加 Collider 2D（建议使用 Box Collider 2D 或 Circle Collider 2D），勾选 "Is Trigger"

2. 添加脚本
    - 将上述脚本命名为 AncientKey_Script.cs，并挂载到钥匙对象上

3. 设置提示UI
    - 在场景中创建一个 Text（UI > Text），用于显示提示文字
    - 将 Text 对象拖到脚本的 interactionText 字段中

4. 设置玩家
    - 确保玩家对象带有 Collider 2D，并设置 Tag 为 "Player"

5. 运行游戏
    - 玩家靠近钥匙时会显示提示文字，按下 E 键后钥匙会消失，并标记为已获得
*/

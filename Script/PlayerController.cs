using UnityEngine;
using UnityEngine.UI; // For UI text display

public class PlayerController : MonoBehaviour
{
    public float speed = 5f;
    public float interactionRange = 1f; // How close the player needs to be to interact with an item or NPC
    public float skinWidth = 0.1f;   // 距离墙体的安全距离，避免嵌入
    public LayerMask obstacleLayer;   // 墙体所在的图层

    private Rigidbody2D rb;
    private Vector2 movement;

    // Reference to the NPC the player is interacting with
    private GameObject npcInRange;
    private Text dialogueText; // Reference to the UI Text for dialogue

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        // Assuming there's a UI Text element in your scene for dialogue display
        dialogueText = GameObject.Find("DialogueText").GetComponent<Text>();
        dialogueText.text = ""; // Ensure no text is showing at the start
    }

    void Update()
    {
        // 获取移动输入
        movement.x = Input.GetAxis("Horizontal");
        movement.y = Input.GetAxis("Vertical");

        // Handle search action when pressing the "F" key for items
        if (Input.GetKeyDown(KeyCode.F) && npcInRange != null)
        {
            SearchItem();
        }

        // Handle talk action when pressing the "T" key for NPCs
        if (Input.GetKeyDown(KeyCode.T) && npcInRange != null)
        {
            TalkToNPC();
        }
    }

    void FixedUpdate()
    {
        // 计算移动向量和目标位置
        Vector2 direction = movement.normalized;
        float distance = movement.magnitude * speed * Time.fixedDeltaTime;
        Vector2 targetPosition = rb.position + direction * distance;

        // 使用 BoxCast 来检测前方是否有障碍物
        RaycastHit2D hit = Physics2D.BoxCast(rb.position, rb.GetComponent<Collider2D>().bounds.size, 0, direction, distance, obstacleLayer);

        if (hit.collider != null)
        {
            // 如果检测到障碍物，则只移动到障碍物前的安全距离
            float adjustedDistance = hit.distance - skinWidth;
            if (adjustedDistance > 0)
            {
                Vector2 newPosition = rb.position + direction * adjustedDistance;
                rb.MovePosition(newPosition);
            }
        }
        else
        {
            // 如果没有障碍物，正常移动
            rb.MovePosition(targetPosition);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        // Detect if the player is near an NPC
        if (other.CompareTag("NPC"))
        {
            npcInRange = other.gameObject;
            Debug.Log("Press T to talk to the NPC!");
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        // Exit detection when the player moves away
        if (other.CompareTag("NPC"))
        {
            npcInRange = null;
            dialogueText.text = ""; // Clear dialogue when leaving
        }
    }

    void TalkToNPC()
    {
        // Here you can display the NPC's dialogue
        Debug.Log("Talking to NPC: " + npcInRange.name);

        // Example: Display a basic dialogue
        dialogueText.text = "Hello! Welcome to the Dute Town.";
    }

    void SearchItem()
    {
        // Handle item interaction (same as before)
        Debug.Log("Searching the item: " + npcInRange.name);
        Destroy(npcInRange); // For example, destroy item after searching
    }
}

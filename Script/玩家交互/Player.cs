using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] public float moveSpeed;
    [SerializeField] private GameInput gameInput;
    [SerializeField] public float interactionRange = 1f; // How close the player needs to be to interact with an item or NPC
    [SerializeField] public float skinWidth = 0.01f;   // 距离墙体的安全距离，避免嵌入
    [SerializeField] public LayerMask obstacleLayer;   // 墙体所在的图层

    private Vector2 movement;
    private Rigidbody2D rb;
    private float mygravity = 0;
    private bool isWalking;
    private bool isFacingRight = false; // Default to false if the sprite initially faces left

    private Animator animator; // Reference to the Animator component

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        mygravity = rb.gravityScale;

        // Get the Animator component attached to the player
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        // Get the normalized movement input from the GameInput script
        Vector2 inputVector = gameInput.GetMovementVectorNormalized();
        if (inputVector.x == 0)
        {
            rb.drag = 10f; // 设置较高的摩擦力以使角色停止
            rb.gravityScale = 0;
        }
        else
        {
            rb.drag = 0f;  // 如果有输入，恢复正常的摩擦力
            rb.gravityScale = mygravity;
        }

        float distance = inputVector.magnitude * moveSpeed * Time.fixedDeltaTime;
        // Create a movement direction vector for 2D, ignoring the z-axis
        Vector2 targetPosition = rb.position + inputVector * distance;

        // 检测前方是否有障碍物
        RaycastHit2D hit = Physics2D.BoxCast(rb.position, rb.GetComponent<Collider2D>().bounds.size, 0, inputVector, distance, obstacleLayer);

        if (hit.collider != null)
        {
            // 如果检测到障碍物，则只移动到障碍物前的安全距离
            float adjustedDistance = hit.distance - skinWidth;
            if (adjustedDistance > 0)
            {
                Vector2 newPosition = rb.position + inputVector * adjustedDistance;
                rb.MovePosition(newPosition);
            }
        }
        else
        {
            // 如果没有障碍物，正常移动
            rb.MovePosition(targetPosition);
        }

        // Update the walking state (only true if moving along the x-axis)
        isWalking = inputVector.x != 0;

        // Update the Animator with the walking state
        if (animator != null)
        {
            animator.SetBool("isWalk", isWalking);
        }

        // Flip the player sprite to face the movement direction
        if ((inputVector.x > 0 && !isFacingRight) ||
            (inputVector.x < 0 && isFacingRight))
        {
            Flip();
        }
    }

    // Flips the player's sprite by adjusting the localScale
    private void Flip()
    {
        isFacingRight = !isFacingRight;
        Vector3 localScale = transform.localScale;
        localScale.x *= -1; // Inverts the x scale
        transform.localScale = localScale;
    }

    // Returns whether the player is currently walking
    public bool IsWalking()
    {
        return isWalking;
    }
}

using UnityEngine;

public class BossController : MonoBehaviour
{
    // References to movement, detection, etc.
    public Transform left;
    public Transform right;
    public Animator animator;
    public Transform OriginalBossTransform;

    public float patrolSpeed = 2f;
    public float chargeSpeed = 4.6f;
    public float detectionRange = 5f;
    public float detectionRangeBack = 3f;
    public LayerMask playerLayer;
    public float detectionTimeThreshold = 1f;

    private Vector3 currentTarget;
    private bool movingToA = true;
    private float detectionTimer = 0f;

    private BossStateMachine stateMachine;
    public Transform player;
    public bool isFaceRight = false;


    private void Start()
    {
        stateMachine = GetComponent<BossStateMachine>();
        player = GameObject.FindWithTag("Player")?.transform;
        Debug.Log(player);
        currentTarget = left.position;
        OriginalBossTransform = GetComponent<Rigidbody2D>().transform;
        // Initialize with patrol state
        stateMachine.ChangeState(new BossPatrolState(this, stateMachine));
    }

    private void Update()
    {
        stateMachine.Update();

        if (IsPlayerInRange())
        {
            detectionTimer += Time.deltaTime;
            if (detectionTimer >= detectionTimeThreshold)
            {
                // Player detected for enough time, initiate Charge state
                stateMachine.ChangeState(new BossChargeState(this, stateMachine));
            }
        }
        else
        {
            detectionTimer = 0f;
        }
    }

    public bool IsPlayerInRange()
    {
        Vector2 frontDirection = isFaceRight ? Vector2.right : Vector2.left;
        Vector2 backDirection = isFaceRight ? Vector2.left : Vector2.right;

        bool frontHit = Physics2D.Raycast(transform.position, frontDirection, detectionRange, playerLayer);
        bool backHit = Physics2D.Raycast(transform.position, backDirection, detectionRangeBack, playerLayer);
        Debug.Log(frontDirection);
        Debug.Log(transform.position);
        return frontHit || backHit;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            VideoManager.instance.ShowVideo_BadEnd(true);
        }
    }

    public void Patrol()
    {
        if(isFaceRight)
        {
            currentTarget = right.position;
        }
        else
        {
            currentTarget = left.position;
        }
        transform.position = Vector2.MoveTowards(transform.position, currentTarget, patrolSpeed * Time.deltaTime);

        if (Vector2.Distance(transform.position, currentTarget) < 0.1f)
        {
            movingToA = !movingToA;
            currentTarget = movingToA ? left.position : right.position;

            stateMachine.ChangeState(new BossIdleState(this, stateMachine));
        }
    }

    public void Charge(Vector3 targetPosition)
    {
        transform.position = Vector2.MoveTowards(transform.position, targetPosition, chargeSpeed * Time.deltaTime);

        if (Vector2.Distance(transform.position, targetPosition) < 0.1f)
        {
            stateMachine.ChangeState(new BossDetectedState(this, stateMachine));
        }
    }

    public void Flip()
    {
        isFaceRight = !isFaceRight;
        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    [SerializeField] protected float speed;
    [SerializeField] protected float direction;
    [SerializeField] protected int lifePoint;
    [SerializeField] protected float rayLenghtFromFeet;
    [SerializeField] protected float radius;
    [SerializeField] protected GameObject eyeSightCenterPoint;

    public enum MovementType {PatrollingCheckingGround, HeroMovement, MoveTowardsTarget, GoBackToInitialPosition };
    [SerializeField] protected MovementType movementType;
    protected MovementType initialMovementType;

    protected Animator animator;
    protected BoxCollider2D boxCollider2D;
    protected Vector2 leftFoot;
    protected Vector2 rightFoot;
    protected bool isOnGround;
    protected bool facingForward;
    protected Vector3 initialPosition;

    private Transform Target;

    protected Rigidbody2D rb;

    int ground = 1 << 6;
    int enemyLayer = 1 << 7;
    int bulletLayer = 1 << 8;

    float rangeRadius;
    Vector3 rangeOrigin;

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    protected void GetAnimator()
    {
        if (GetComponent<Animator>())
        {
            animator = gameObject.GetComponent<Animator>();
        }
    }

    protected void GetBoxCollider()
    {
        if (GetComponent<BoxCollider2D>())
        {
            boxCollider2D = gameObject.GetComponent<BoxCollider2D>();
        }
    }

    public void Move()
    {
        switch (movementType)
        {
            case MovementType.PatrollingCheckingGround:
                PatrollingCheckingGround();
                break;

            case MovementType.HeroMovement:
                HeroMovement();
                break;

            case MovementType.MoveTowardsTarget:
                MoveTowardsTarget();
                break;

            case MovementType.GoBackToInitialPosition:
                GoBackToInitialPosition();
                break;

            default: break;
        }
    }
    public void PatrollingCheckingGround()
    {
        transform.position = new Vector2(transform.position.x + (speed * direction * Time.deltaTime), transform.position.y);
    }
    public void HeroMovement()
    {
        Rigidbody2D rb;

        if(GetComponent<Rigidbody2D>())
        {
            rb = GetComponent<Rigidbody2D>();
            rb.velocity = new Vector2(speed * direction, rb.velocity.y);
        }
    }
    private void MoveTowardsTarget()
    {
        if (Target)
        {
            transform.position =  Vector2.MoveTowards(transform.position, Target.position, speed * Time.deltaTime);
        }
    }

    private void GoBackToInitialPosition()
    {
        //Debug.Log("I am moving back to initialPosition: " + initialPosition);
        transform.position = Vector2.MoveTowards(transform.position, initialPosition, speed * Time.deltaTime);
        if(transform.position == initialPosition)
        {
            movementType = initialMovementType;
        }
    }

    private protected void StopMoving()
    {
        Rigidbody2D rb;

        if (GetComponent<Rigidbody2D>())
        {
            rb = GetComponent<Rigidbody2D>();
            rb.velocity = new Vector2(rb.velocity.x * 0, rb.velocity.y);
        }
    }

    private protected void Jump(int jF)
    {
        int jumpForce = jF;
        Rigidbody2D rb;
        if (GetComponent<Rigidbody2D>())
        {
            rb = GetComponent<Rigidbody2D>();
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        }
    }

    public void TakeDamage(int damage)
    {
        lifePoint -= damage;
        if (lifePoint <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        Destroy(gameObject);
    }

    protected void EmittingRaysFromFeet()
    {
        leftFoot = new Vector2(boxCollider2D.bounds.min.x, boxCollider2D.bounds.min.y);
        rightFoot = new Vector2(boxCollider2D.bounds.max.x, boxCollider2D.bounds.min.y);

        RaycastHit2D leftFootRays = Physics2D.Raycast(leftFoot, Vector2.down, rayLenghtFromFeet, ground);
        RaycastHit2D rightFootRays = Physics2D.Raycast(rightFoot, Vector2.down, rayLenghtFromFeet, ground);

        CheckIfGround(leftFootRays, rightFootRays);
        DrawRaysFromFeet();
    }

    private void CheckIfGround(RaycastHit2D lFRays, RaycastHit2D rFRays)
    {
        if ((!lFRays) || (!rFRays))
        {
            isOnGround = false;
           
            if ((gameObject.name == "Flying_Eye") && (movementType == MovementType.PatrollingCheckingGround))
            {
                //Chiedere al prof come nantenere l'angolo dell z fermo in rotazione
              
                transform.Rotate(0f, 180f, 0f);
           
                direction = direction * -1;
            }
            //direction *= direction - 1;
        }
        else
            isOnGround = true;
        Debug.Log("isOnGround" + isOnGround);

    }

    protected void EmittingEyeSight()
    {
        rangeOrigin = SetOrigin();
        rangeRadius = SetRadius();
        LookingForHeros();
    }

    private Vector2 SetOrigin()
    {
        Vector3 center;

        if (eyeSightCenterPoint)
        {
            center = eyeSightCenterPoint.transform.position;
            Debug.Log("True it has a center now");
            return center;
        }
        else
            Debug.Log("The component has not been found");

        //Is a correct way?
        return Vector2.zero;
    }

    private float SetRadius()
    {
        float r;

        if (eyeSightCenterPoint)
        {
            r = radius;
            Debug.Log("True it has a radius now");
            return r;
        }
        else
        {
            return 0;
        }
    }

    private void LookingForHeros()
    {
        RaycastHit2D range = Physics2D.CircleCast(rangeOrigin, rangeRadius, Vector2.zero, 1, ~(enemyLayer + bulletLayer));

        if (range.collider != null)
        {
            if (range.collider.gameObject.CompareTag("Hero"))
            {
                Debug.Log("From this distance i can hit the Hero");
                RaycastHit2D target = Physics2D.Linecast(transform.position, range.collider.gameObject.transform.position, ~(enemyLayer + bulletLayer));
                Target = range.collider.transform;
                DrawTargetLine(range);
                movementType = MovementType.MoveTowardsTarget;
                //Move();
            }
            else if ((transform.position != initialPosition) && (movementType == MovementType.MoveTowardsTarget || movementType == MovementType.GoBackToInitialPosition))
            {
                Debug.Log("I see something but it not the player is a: " + range.collider.gameObject.tag);
                movementType = MovementType.GoBackToInitialPosition;
            }
        }
    }

    private void DrawRaysFromFeet()
    {
        Debug.DrawRay(leftFoot, Vector2.down * rayLenghtFromFeet, Color.blue);
        Debug.DrawRay(rightFoot, Vector2.down * rayLenghtFromFeet, Color.blue);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(rangeOrigin, rangeRadius);
    }

    private void DrawTargetLine(RaycastHit2D r)
    {
        Debug.DrawLine(transform.position, r.collider.gameObject.transform.position, Color.yellow);
    }

}
